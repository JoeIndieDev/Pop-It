using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawningManager;
    public Text scoreText;
    public Text highScoreText;
    public GameObject playerController;
    public GameObject spawnmanager;
    public GameObject homePanel;
    public GameObject settingsPanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject UI;
    InputSystem_Actions playerInputSystem;
    public bool isGameStarted;
    public bool isGameEnded;
    public int scoreCount;
    public int highScoreCount;
    public Slider modeSlider;

    private string highScoreFilePath;

    private void Awake()
    {
        playerInputSystem = new InputSystem_Actions();
        isGameStarted = false;
        isGameEnded = false;
        highScoreFilePath = Application.persistentDataPath + "/highscore.json";

        LoadHighScore(); // Load the saved high score when the game starts
    }

    private void Start()
    {
        if (modeSlider != null)
        {
            modeSlider.minValue = 0.2f;
            modeSlider.maxValue = 1f;
            modeSlider.value = spawningManager.spawnTime;
        }
    }

    public void PLay()
    {
        isGameStarted = true;
    }

    void Update()
    {
        modeSlider.onValueChanged.AddListener(UpdateMode);

        if (isGameStarted)
        {
            spawnmanager.SetActive(true);
            playerController.SetActive(true);
            homePanel.SetActive(false);
            UI.SetActive(true);
        }

        if (isGameEnded)
        {
            spawnmanager.SetActive(false);
            playerController.SetActive(false);
            isGameStarted = false;

            if (scoreCount > highScoreCount)
            {
                highScoreCount = scoreCount;
                SaveHighScore(); // Save the new high score
            }
        }
    }

    public void UpdateMode(float newValue)
    {
        spawningManager.spawnTime = newValue;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void Pause()
    {
        spawnmanager.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        spawnmanager.SetActive(true);
        pausePanel.SetActive(false);
        StartCoroutine(spawningManager.SpawnBallons());
    }

    public void IncreamentScore()
    {
        scoreCount++;
        scoreText.text = scoreCount.ToString();
    }

    private void OnEnable()
    {
        playerInputSystem.Enable();
    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
    }

    // Save high score as JSON
    private void SaveHighScore()
    {
        HighScoreData data = new HighScoreData { highScore = highScoreCount };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(highScoreFilePath, json);
        highScoreText.text = highScoreCount.ToString();
    }

    // Load high score from JSON
    private void LoadHighScore()
    {
        if (File.Exists(highScoreFilePath))
        {
            string json = File.ReadAllText(highScoreFilePath);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
            highScoreCount = data.highScore;
            highScoreText.text = highScoreCount.ToString();
        }
        else
        {
            highScoreCount = 0;
            highScoreText.text = "0";
        }
    }
}

// High score data class
[System.Serializable]
public class HighScoreData
{
    public int highScore;
}
