using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // Prompt UI
    public GameObject devicePromptCanvas;
    public TMP_Text uiText;
    public float timeBetweenPrompts = 5f;

    // Prefabs that can be spawned by the prompts.
    public GameObject plantPrefab; public Vector3 plantLocation;
    public GameObject fanPrefab; public Vector3 fanLocation;

    // The bubble itself.
    public GameObject bubblePrefab;
    private GameObject bubbleInstance;
    private BubbleController bubbleController;

    private float _timer = 0;
    private bool _promptIsOpen = false;
    private int selectedPromptIndex = 0;
    private string selectedPromptText;

    // Set prompt values.
    const string _prompt1 = "Bubble is sad.";
    const string _prompt2 = "Bubble is hot.";
    const string _prompt3 = "Bubble has a green thumb.";
    
    private List<string> prompts = new List<string>() {_prompt1, _prompt2, _prompt3};

    Camera m_Camera;

    private void Awake()
    {
        m_Camera = Camera.main;
    }

    private void Start()
    {
        bubbleInstance = Instantiate(bubblePrefab, new Vector3(-19.5f, 0, 0), Quaternion.identity);
        bubbleController = bubbleInstance.GetComponentInChildren<BubbleController>();
    }

    void Update()
    {
        CheckAndDisplayPrompt();
        ProcessDeviceClicks();
    }


    // Called by the BubbleController script when the bubble touches something.
    public void BubbleCollided()
    {
        if (bubbleInstance != null)
        {
            Destroy(bubbleInstance); // "Pop" the bubble on collision.
            devicePromptCanvas.SetActive(false);
        }
    }

    // Display interactive prompts if conditions are met. 
    void CheckAndDisplayPrompt()
    {
        if (!_promptIsOpen && bubbleInstance != null)
        {
            _timer += Time.deltaTime;

            if (_timer >= timeBetweenPrompts)
            {
                _timer = 0;

                if (prompts.Count > 0)
                {
                    selectedPromptIndex = Random.Range(0, prompts.Count - 1); // Pick a random prompt from the list of remaining prompts.
                    selectedPromptText = prompts[selectedPromptIndex];
                    uiText.text = selectedPromptText;

                    devicePromptCanvas.SetActive(true);

                    _promptIsOpen = true;


                    switch (selectedPromptText)
                    {
                        case _prompt1: // Bubble is sad - increase mass
                            Shader.SetGlobalFloat("_Sad_Prompt", 0.75f);
                            break;
                        case _prompt2: // Bubble is hot - spawn fan
                            Shader.SetGlobalFloat("_Hot_Prompt", 0.75f);
                            break;
                        case _prompt3: // Bubble wants a plant - spawn cactus
                            Shader.SetGlobalFloat("_Plant_Prompt", 0.75f);
                            break;
                        default:
                            Debug.Log($"No action found for prompt {selectedPromptText}");
                            break;
                    }
                }
            }
        }
    }

    public void RespondToPrompt(bool confirmed)
    {
        //Debug.Log($"Prompt responded to! Selected prompt: {selectedPromptText}.  Confirmed: {confirmed}");
        _promptIsOpen = false;

        // Disable canvas.
        devicePromptCanvas.SetActive(false);

        // Do something depending on confirmed prompt...
        if (confirmed)
        {
            switch (selectedPromptText)
            {
                case _prompt1: // Bubble is sad - increase mass
                    bubbleInstance.GetComponentInChildren<Rigidbody2D>().gravityScale += 1;
                    break;
                case _prompt2: // Bubble is hot - spawn fan
                    Instantiate(fanPrefab, fanLocation, Quaternion.identity);
                    break;
                case _prompt3: // Bubble wants a plant - spawn cactus
                    Instantiate(plantPrefab, plantLocation, Quaternion.identity);
                    break;
                default:
                    Debug.Log($"No action found for prompt {selectedPromptText}");
                    break;
            }
        }

        switch (selectedPromptText)
        {
            case _prompt1: // Bubble is sad - increase mass
                Shader.SetGlobalFloat("_Sad_Prompt", 0f);
                break;
            case _prompt2: // Bubble is hot - spawn fan
                Shader.SetGlobalFloat("_Hot_Prompt", 0f);
                break;
            case _prompt3: // Bubble wants a plant - spawn cactus
                Shader.SetGlobalFloat("_Plant_Prompt", 0f);
                break;
            default:
                Debug.Log($"No action found for prompt {selectedPromptText}");
                break;
        }

        // Remove the processed prompt from the prompts list.
        prompts.RemoveAt(selectedPromptIndex);

    }

    private void ProcessDeviceClicks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Use the hit variable to determine what was clicked on.
                string clickedButton = hit.collider.gameObject.name;

                switch (clickedButton)
                {
                    case "polySurface1":
                        RespondToPrompt(true);
                        break;
                    case "polySurface2":
                        RespondToPrompt(false);
                        break;
                    case "polySurface3":
                        bubbleController.BubbleMoveRight();
                        break;
                    case "polySurface4":
                        bubbleController.BubbleMoveUp();
                        break;
                    case "polySurface5":
                        bubbleController.BubbleMoveLeft();
                        break;
                }
            }
        }
    }
}
