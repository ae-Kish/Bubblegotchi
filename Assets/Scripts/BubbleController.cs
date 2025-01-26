using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class BubbleController : MonoBehaviour
{
    public Camera cameraToCastFrom;

    public Rigidbody2D myRigidBody;
    public float verticalPushOnClick = 2;
    public float horizontalPushOnClick = 5;

    public InputActionReference moveInput;

    //Audio
    [SerializeField] private FMODUnity.EventReference _clickAudio;
    [SerializeField] private FMODUnity.EventReference _pushAudio;
    [SerializeField] private FMODUnity.EventReference _collideAudio;
    [SerializeField] private FMODUnity.EventReference _breakAudio;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            BubbleMoveUp();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            BubbleMoveDown();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            BubbleMoveRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            BubbleMoveLeft();
        }

    }

    // looks for a plant and we will pop the bubble on hit
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Debug.Log("I hit something");
            Destroy(gameObject);
        }
    }

    // when the bubble pops we are gonna then let the player know so they can get a new one
    private void OnDisable()
    {
        Debug.Log("PrintOnDisable: script was disabled");
    }

    private void PlayPushSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(_pushAudio.Guid, transform.position);
    }

    private void PlayClickSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(_clickAudio.Guid, transform.position);
    }


    private void BubbleMoveUp()
    {
        myRigidBody.linearVelocity += new Vector2(0, verticalPushOnClick);
        PlayPushSound();
    }

    private void BubbleMoveDown()
    {
        myRigidBody.linearVelocity += new Vector2(0, -verticalPushOnClick);
    }

    private void BubbleMoveRight()
    {
        myRigidBody.linearVelocity = new Vector2(horizontalPushOnClick, 0);
    }

    private void BubbleMoveLeft()
    {
        myRigidBody.linearVelocity = new Vector2(-horizontalPushOnClick, 0);
    }
}
