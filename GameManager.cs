using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public static int level = 1;
    public static bool isGameOver = false;
    public static int currentMoney;
    public static GameManager instance;
    // Panels
    public GameObject[] platforms;
    public GameObject mainmenuPanel;
    public GameObject losePanel;
    public TextMeshProUGUI moneyText;
    public GameObject gamePanel;
    public GameObject winPanel;
    public GameObject pausePanel;
    public GameObject settingsPanel;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Application.targetFrameRate = 60;
        currentMoney = 0;
        level = PlayerPrefs.GetInt("level");
        if (level > 2)
        {
            level = 0;
            PlayerPrefs.SetInt("level", level);
        }

         //Spawning Level
        Instantiate(platforms[PlayerPrefs.GetInt("level")], Vector3.zero, Quaternion.identity);

        Time.timeScale = 0;       
        isGameOver = true;
        

    }


    void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("level"));
        moneyText.text = currentMoney.ToString();

    }
    public void Win()
    {
        isGameOver = true;
        Time.timeScale = 0;
        MMVibrationManager.Haptic(HapticTypes.Success);
        level++;
        gamePanel.SetActive(false);
        winPanel.SetActive(true);
    }
    public void Lose()
    {
        Time.timeScale = 0;
        isGameOver = true;
        MMVibrationManager.Haptic(HapticTypes.Failure);
        gamePanel.SetActive(false);
        losePanel.SetActive(true);
    }
    public void StartButton()
    {

        Time.timeScale = 1;
        isGameOver = false;
        mainmenuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(0);

    }
    public void SettingsButton()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseButton()
    {
        settingsPanel.SetActive(false);
    }
    public void PauseButton()
    {
        isGameOver = true;
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void ContinueButton()
    {
        isGameOver = false;
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevelButton()
    {
        PlayerPrefs.SetInt("level", level++);
        if (PlayerPrefs.GetInt("level") > 2)
        {
            PlayerPrefs.SetInt("level", 0);
        }
        SceneManager.LoadScene(0);
    }

}
