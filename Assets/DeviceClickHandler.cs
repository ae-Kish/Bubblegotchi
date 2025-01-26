using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceClickHandler : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject bubble;
    private BubbleController bubbleController;

    Camera m_Camera;
    void Awake()
    {
        m_Camera = Camera.main;
    }

    void Start()
    {
        gameManager = GetComponent<GameManager>();
        //bubble = GameObject.FindGameObjectWithTag("Bubble");
    }

    void Update()
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
                        // Yes
                        break;
                    case "polySurface2":
                        // No
                        break;
                    case "polySurface3":
                        // Move right
                        break;
                    case "polySurface4":
                        // Move Up
                        break;
                    case "polySurface5":
                        // Move left
                        break;
                }
                Debug.Log($"Raycast hit an object:{hit.collider.gameObject.name}");
            }
        }
    }
}
