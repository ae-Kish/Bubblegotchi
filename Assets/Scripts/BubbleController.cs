using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;

public class BubbleController : MonoBehaviour
{
    public Camera cameraToCastFrom;

    public Rigidbody2D myRigidBody;
    public float verticalPushOnClick = 2;
    public float horizontalPushOnClick = 5;

    public InputActionReference moveInput;

    public GameManager gameManager;

    //Audio
    [SerializeField] private FMODUnity.EventReference _clickAudio;
    [SerializeField] private FMODUnity.EventReference _sidePushAudio;
    [SerializeField] private FMODUnity.EventReference _upPushAudio;
    [SerializeField] private FMODUnity.EventReference _downPushAudio;
    [SerializeField] private FMODUnity.EventReference _collideAudio;
    [SerializeField] private FMODUnity.EventReference _breakAudio;
    [SerializeField] private FMODUnity.EventReference _createAudio;

    private void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(_createAudio.Guid, gameObject);

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

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
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("I hit something");
            FMODUnity.RuntimeManager.PlayOneShotAttached(_breakAudio.Guid, gameObject);

            gameManager.BubbleCollided();
        }
    }

    // when the bubble pops we are gonna then let the player know so they can get a new one
    private void OnDisable()
    {
        Debug.Log("PrintOnDisable: script was disabled");
    }
    //Commenting out since I'm not really using them.

    //private void PlayPushSound()
    //{
    //    FMODUnity.RuntimeManager.PlayOneShot(_sidePushAudio.Guid, transform.position);
    //}

    //private void PlayClickSound()
    //{
    //    FMODUnity.RuntimeManager.PlayOneShotAttached(_clickAudio.Guid, gameObject);
    //}


    public void BubbleMoveUp()
    {
        myRigidBody.linearVelocity += new Vector2(0, verticalPushOnClick);
        FMODUnity.RuntimeManager.PlayOneShotAttached(_upPushAudio.Guid, gameObject);
    }

    public void BubbleMoveDown()
    {
        myRigidBody.linearVelocity += new Vector2(0, -verticalPushOnClick);
        FMODUnity.RuntimeManager.PlayOneShotAttached(_downPushAudio.Guid, gameObject);
    }

    public void BubbleMoveRight()
    {
        myRigidBody.linearVelocity = new Vector2(horizontalPushOnClick, 0);
        FMODUnity.RuntimeManager.PlayOneShotAttached(_sidePushAudio, gameObject);
    }

    public void BubbleMoveLeft()
    {
        myRigidBody.linearVelocity = new Vector2(-horizontalPushOnClick, 0);
        FMODUnity.RuntimeManager.PlayOneShotAttached(_sidePushAudio, gameObject);
    }
}
