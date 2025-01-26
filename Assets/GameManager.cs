using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    private float _timeBetweenPrompts = 2f;
    private float _timer = 0;

    public TMP_Text uiText;

    private List<string> _prompts = new List<string>() {"Bubble is sad.", "Bubble is hot.", "Bubble wants his favorite plant..."};
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiText.text = "Hello world.";
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeBetweenPrompts)
        {
            _timer = 0;

            //Debug.Log($"Hello? {uiText.text}");
            //Debug.Log($"Time for a new prompt! Current prompt: {_textMeshPro.text

            if (_prompts.Count > 0)
            {
                int randomIndex = Random.Range(0, _prompts.Count - 1);

                string selectedPrompt = _prompts[randomIndex];

                //Debug.Log($"Selected prompt: {selectedPrompt}");
                uiText.text = selectedPrompt;

                _prompts.Remove(selectedPrompt);
            }
        }
    }
}
