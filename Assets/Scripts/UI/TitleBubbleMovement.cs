using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleBubbleMovement : MonoBehaviour
{
    private float moveSpeed;

    void Start()
    {
        moveSpeed = Random.Range(20, 40);
    }

    void Update()
    {
        float titleBubblePrefabX = transform.localPosition.x;
        float titleBubblePrefabY = transform.localPosition.y;
        float titleBubblePrefabZ = transform.localPosition.z;

        Vector3 newPosition = new Vector3(titleBubblePrefabX, titleBubblePrefabY + moveSpeed, titleBubblePrefabZ);

        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, Time.deltaTime);

        if (titleBubblePrefabY > 265)
        {
            Vector3 newPositionY = new Vector3(titleBubblePrefabX, -252, titleBubblePrefabZ);

            transform.localPosition = newPositionY;
        }
    }
}
