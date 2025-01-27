using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] private Button startButton, quitButton;
    [SerializeField] private Image cursor, blackFade;

    private Animator blackFadeAnimator;

    private AnimatorStateInfo animStateInfo;

    private bool checkIfFading = false;

    AsyncOperation loadingSceneAsync;

    private void Start()
    {
        SetCursorToStartButton();

        blackFadeAnimator = blackFade.GetComponent<Animator>();
    }

    private void Update()
    {
        if (checkIfFading)
        {
            animStateInfo = blackFadeAnimator.GetCurrentAnimatorStateInfo(0);

            if (animStateInfo.IsName("BlackFadeDone"))
            {
                loadingSceneAsync.allowSceneActivation = true;
            }
        }
    }

    public void StartGame()
    {
        loadingSceneAsync = SceneManager.LoadSceneAsync("MainScene");
        loadingSceneAsync.allowSceneActivation = false;

        blackFadeAnimator.Play("FadeToBlack");

        checkIfFading = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetCursorToStartButton()
    {
        cursor.transform.SetParent(startButton.transform);
        cursor.transform.position = startButton.transform.position;
    }

    public void SetCursorToQuitButton()
    {
        cursor.transform.SetParent(quitButton.transform);
        cursor.transform.position = quitButton.transform.position;
 
    }

}
