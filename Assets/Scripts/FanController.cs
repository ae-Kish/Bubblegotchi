using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField] private GameObject bubble;
    [SerializeField] private float fanForce = 0.5f;
    [SerializeField] private float maxDistanceToBubble = 5.0f;

    private BubbleMovement bubblePhysics;

    private Vector2 direction;

    void Start()
    {
        bubble = GameObject.FindGameObjectWithTag("Bubble");
        bubblePhysics = bubble.GetComponentInChildren<BubbleMovement>();
    }

    void Update()
    {
        LookAtBubble();
        AddForceToBubble();
    }

    // Fan will always be looking in the direction of the bubble
    private void LookAtBubble()
    {
        if (bubble != null)
        {
            direction = bubble.transform.GetChild(0).position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    // On left click the fan will push the bubble in the direction it is facing
    private void AddForceToBubble()
    {
        if (bubble != null)
        {
            bool isKeyDown = (
                Input.GetKeyDown(KeyCode.LeftArrow)
                || Input.GetKeyDown(KeyCode.RightArrow)
                || Input.GetKeyDown(KeyCode.DownArrow)
                || Input.GetKeyDown(KeyCode.UpArrow)
                );

            if (isKeyDown && GetDistanceToBubble() < maxDistanceToBubble)
            {
                float inverseDistance = 1 / GetDistanceToBubble();

                Vector2 fanDirection = direction.normalized;

                // Calculation so that more force is applied when the fan is closer to the bubble
                float forceToApply = fanForce * inverseDistance;

                bubblePhysics.ApplyFanForce(fanDirection, forceToApply);
            }
        }
    }

    private float GetDistanceToBubble()
    {
        Vector2 fanPos = transform.position;
        Vector2 bubblePos = bubble.transform.GetChild(0).position;

        return Vector2.Distance(bubblePos, fanPos);
    }
}
