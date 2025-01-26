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

    private float _timer = 0;
    private bool _promptIsOpen = false;
    private int selectedPromptIndex = 0;
    private string selectedPromptText;

    // Set prompt values.
    const string _prompt1 = "Bubble is sad.";
    const string _prompt2 = "Bubble is hot.";
    const string _prompt3 = "Bubble has a green thumb.";
    
    private List<string> prompts = new List<string>() {_prompt1, _prompt2, _prompt3};



    private void Start()
    {
        bubbleInstance = Instantiate(bubblePrefab, new Vector3(-19.5f, 0, 0), Quaternion.identity);
    }

    void Update()
    {
        CheckAndDisplayPrompt();
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

        // Remove the processed prompt from the prompts list.
        prompts.RemoveAt(selectedPromptIndex);

    }

    public void SpawnPrefab(GameObject prefab)
    {

        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
