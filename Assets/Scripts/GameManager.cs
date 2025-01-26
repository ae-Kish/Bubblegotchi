using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // Prompt UI
    public GameObject deviceCanvas;
    public TMP_Text uiText;
    public float timeBetweenPrompts = 5f;

    // Prefabs that can be spawned by the prompts.
    public GameObject plantPrefab; public Vector3 plantLocation;
    public GameObject fanPrefab; public Vector3 fanLocation;

    // The bubble itself.
    public GameObject bubblePrefab;
    private GameObject _bubbleInstance;

    private float _timer = 0;
    private bool _promptIsOpen = false;
    private int selectedPromptIndex = 0;
    private string selectedPromptText;

    // Set prompt values.
    const string _prompt1 = "Bubble is sad.";
    const string _prompt2 = "Bubble is hot.";
    const string _prompt3 = "Bubble has a green thumb.";
    
    private List<string> _prompts = new List<string>() {_prompt1, _prompt2, _prompt3};



    private void Start()
    {
        _bubbleInstance = Instantiate(bubblePrefab, new Vector3(-19.5f, 0, 0), Quaternion.identity);
        Debug.Log($"Bubble loaded successfully? {_bubbleInstance}");
        
    }

    void Update()
    {
        CheckAndDisplayPrompt();
    }



    public void BubbleCollided()
    {

        if (_bubbleInstance != null)
        {
            Destroy(_bubbleInstance);
            Debug.Log($"Game manager destroyed bubble because it popped!");

            deviceCanvas.SetActive(false);
        }
    }

    void CheckAndDisplayPrompt()
    {
        if (!_promptIsOpen && _bubbleInstance != null)
        {
            _timer += Time.deltaTime;

            if (_timer >= timeBetweenPrompts)
            {
                _timer = 0;

                if (_prompts.Count > 0)
                {
                    selectedPromptIndex = Random.Range(0, _prompts.Count - 1); // Pick a random prompt from the list of remaining prompts.
                    selectedPromptText = _prompts[selectedPromptIndex];
                    uiText.text = selectedPromptText;

                    deviceCanvas.SetActive(true);
                    _promptIsOpen = true;
                }
            }
        }
    }

    public void RespondToPrompt(bool confirmed)
    {
        Debug.Log($"Prompt responded to! Selected prompt: {selectedPromptText}.  Confirmed: {confirmed}");
        _promptIsOpen = false;

        // Disable canvas.
        deviceCanvas.SetActive(false);

        // Do something depending on confirmed prompt...
        if (confirmed)
        {
            switch (selectedPromptText)
            {
                case _prompt1:
                    _bubbleInstance.GetComponentInChildren<Rigidbody2D>().mass *= 2;
                    break;
                case _prompt2:

                    break;
                case _prompt3:
                    Instantiate(plantPrefab, new Vector3(-17, -2.5f, 0), Quaternion.identity);
                    break;
                default:
                    Debug.Log($"No action found for prompt {selectedPromptText}");
                    break;
            }
        }

        // Remove the processed prompt from the prompts list.
        _prompts.RemoveAt(selectedPromptIndex);

    }

    public void SpawnPrefab(GameObject prefab)
    {

        Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
