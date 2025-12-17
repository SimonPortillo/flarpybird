using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public Text scoreText;
    public GameObject gameOverScreen;
    public GameObject introScreen;
    public AudioSource ding;

    public int score = 0;
    public bool isGameOver = false;
    public bool isGameStarted = false;
    public float pipeMoveSpeed = 1f;
    public float pipeSpeedIncreaseRate = 0.05f;
    public float pipeSpacingUnits = 2.5f;

    private void Start()
    {
        introScreen.SetActive(true);
    }

    void Update()
    {
        if (!isGameStarted)
        {
            if ((UnityEngine.InputSystem.Keyboard.current != null && UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame) ||
                (UnityEngine.InputSystem.Mouse.current != null && UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame))
            {
                isGameStarted = true;
                introScreen.SetActive(false); 
            }
            return; // Don't update speed or other logic until started
        }
        if (!isGameOver)
        {
            pipeMoveSpeed += pipeSpeedIncreaseRate * Time.deltaTime;
        }
        if (isGameOver)
        {
            pipeMoveSpeed = 0f;
        }
    }

    public float CurrentSpawnInterval
    {
        get
        {
            var speed = Mathf.Max(pipeMoveSpeed, 0.001f);
            return pipeSpacingUnits / speed;
        }
    }
    


    [ContextMenu("Increase Score")]
    public void addScore()
    {
        if (!isGameOver)
        {
            score++;
            scoreText.text = score.ToString();
            ding.Play();
        }
        else return;
    }
    public void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        isGameOver = true;
    }
}
