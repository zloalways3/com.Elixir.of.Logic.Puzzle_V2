using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElixirPuzzleController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject policyMenu;
    [SerializeField] private GameObject rulesMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject tutorMenu;
    [SerializeField] private ElixirPuzzleBoard elixirPuzzleboard;

    private int pointsCount;
    [SerializeField] private TextMeshProUGUI pointsText1;
    [SerializeField] private ElixirPuzzleTimer elixirPuzzletimer;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    private float musicVolume;
    private float soundVolume;

    [SerializeField] private Button[] levelsButton;
    [SerializeField] private TextMeshProUGUI[] levelsText;
    [SerializeField] private Color enableColor;
    [SerializeField] private Color disableColor;
    [SerializeField] private AudioSource audioSourceMusic;
    [SerializeField] private AudioSource audioSourceSound;
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip matchClip;
    [SerializeField] private AudioClip backgroundClip;

    [SerializeField] private TextMeshProUGUI finalHeaderText;
    [SerializeField] private TextMeshProUGUI finalScoreText1;
    [SerializeField] private TextMeshProUGUI finalButtonText;

    [SerializeField] private TextMeshProUGUI levelText;

    private bool isPolicyFromTutor;
    private bool isRulesFromGame;

    private int currentLevel;
    private int maxLevel;

    private void Start()
    {
        var tutorInfo = PlayerPrefs.GetInt("tutorInfo", 0);
        Application.targetFrameRate = 60;
        maxLevel = PlayerPrefs.GetInt("maxLevel", 1);
        ElixirPuzzleRefreshLevelsButton();
        if (tutorInfo == 0)
        {
            tutorMenu.SetActive(true);
        }
        else
        {
            tutorMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        gameMenu.SetActive(false);
        optionsMenu.SetActive(false);
        policyMenu.SetActive(false);
        winMenu.SetActive(false);
        levelMenu.SetActive(false);

        musicVolume = PlayerPrefs.GetFloat("musicVolu", 1);
        soundVolume = PlayerPrefs.GetFloat("soundVolu", 1);
        musicSlider.value = musicVolume;
        audioSourceMusic.volume = musicVolume;
        soundSlider.value = soundVolume;

        audioSourceMusic.clip = backgroundClip;
        audioSourceMusic.Play();
    }

    public void ElixirPuzzleTutorMainMenu()
    {
        ElixirPuzzleBoard.TestB111();
        tutorMenu.SetActive(false);
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        mainMenu.SetActive(true);
        PlayerPrefs.SetInt("tutorInfo", 1);
        PlayerPrefs.Save();
        ElixirPuzzleClickSound();
    }

    public void ElixirPuzzleSwitchSound()
    {
        soundVolume = soundSlider.value;
        PlayerPrefs.SetFloat("soundVolu", soundVolume);
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        PlayerPrefs.Save();
    }

    public void ElixirPuzzleSwitchMusic()
    {
        musicVolume = musicSlider.value;
        audioSourceMusic.volume = musicVolume;
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        PlayerPrefs.SetFloat("musicVolu", musicVolume);
        PlayerPrefs.Save();
    }

    public void ElixirPuzzleRefreshLevelsButton()
    {
        for (int i1111 = 0; i1111 < levelsButton.Length; i1111++)
        {
            if (i1111 < maxLevel)
            {
                levelsButton[i1111].interactable = true;
                levelsText[i1111].color = enableColor;
            }
            else
            {
                levelsButton[i1111].interactable = false;
                levelsText[i1111].color = disableColor;
            }
        }
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
    }

    public void ElixirPuzzleUpdatePoints(int x)
    {
        if (elixirPuzzletimer.ElixirPuzzleGetTimerStatus())
        {
            pointsCount += x * 13;
            pointsText1.text = $"Points:\n{pointsCount}/{350 + 100 * currentLevel}";
            if (pointsCount >= (350 + 100 * currentLevel))
            {
                elixirPuzzletimer.ElixirPuzzlePauseTimer();
                ElixirPuzzleOpenWinMenu();
            }
        }
    }

    public void ElixirPuzzleOpenGameMenu(int num)
    {
        currentLevel = num;
        levelText.text = $"Level {currentLevel}";
        levelMenu.SetActive(false);
        mainMenu.SetActive(false);
        pointsCount = 0;
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        pointsText1.gameObject.SetActive(true);
        pointsText1.text = $"Points:\n{pointsCount}/{350 + 100 * currentLevel}";
        elixirPuzzletimer.gameObject.SetActive(true);
        elixirPuzzletimer.ElixirPuzzleRestartTimer();
        elixirPuzzleboard.gameObject.SetActive(true);
        levelMenu.SetActive(false);
        gameMenu.SetActive(true);
        elixirPuzzleboard.ElixirPuzzleUpdateBoard();
        ElixirPuzzleClickSound();
    }

    public void ElixirPuzzleMainMenuLevelMenu()
    {
        ElixirPuzzleBoard.TestB111();
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        ElixirPuzzleClickSound();
    }

    public void ElixirPuzzleLevelMenuMainMenu()
    {
        ElixirPuzzleBoard.TestB111();
        levelMenu.SetActive(false);
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        mainMenu.SetActive(true);
        ElixirPuzzleClickSound();
    }

    public void ElixirPuzzleGameLevelMenu()
    {
        ElixirPuzzleBoard.TestB111();
        gameMenu.SetActive(false);
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        elixirPuzzletimer.ElixirPuzzlePauseTimer();
        winMenu.SetActive(false);
        levelMenu.SetActive(true);
        elixirPuzzleboard.gameObject.SetActive(true);
        ElixirPuzzleClickSound();
    }

    public void ElixirPuzzleOpenWinMenu()
    {
        gameMenu.SetActive(false);
        winMenu.SetActive(true);
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        if (pointsCount >= (350 + 100 * currentLevel))
        {
            finalHeaderText.text = "Level complete";
            finalScoreText1.text = $"Points:\n{pointsCount}/{350 + 100 * currentLevel}";
            if (maxLevel == currentLevel)
            {
                maxLevel++;
                PlayerPrefs.SetInt("maxLevel", maxLevel);
                PlayerPrefs.Save();
                ElixirPuzzleRefreshLevelsButton();
            }
        }
        else
        {
            finalHeaderText.text = "Level failed";
            finalScoreText1.text = $"Points:\n{pointsCount}/{350 + 100 * currentLevel}";
        }
    }

    public void ElixirPuzzleAppExit()
    {
        ElixirPuzzleBoard.TestB111();
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        Application.Quit();
    }

    private void ElixirPuzzleClickSound()
    {
        ElixirPuzzleBoard.TestB111();
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        audioSourceSound.PlayOneShot(clickClip, soundVolume);
    }

    public void ElixirPuzzleCollectSound()
    {
        ElixirPuzzleBoard.TestB111();
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        audioSourceSound.PlayOneShot(matchClip, soundVolume);
    }

    public void ElixirPuzzleMainMenuOptionsMenu()
    {
        ElixirPuzzleBoard.TestB111();
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        ElixirPuzzleClickSound();
    }

    public void ElixirPuzzleCloseOptionsMenu()
    {
        ElixirPuzzleBoard.TestB111();
        optionsMenu.SetActive(false);
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        mainMenu.SetActive(true);
        ElixirPuzzleClickSound();
    }

    public void ElixirPuzzleOpenPolicyMenu()
    {
        ElixirPuzzleBoard.TestB111();
        mainMenu.SetActive(false);
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        policyMenu.SetActive(true);
        ElixirPuzzleClickSound();
    }

    public void ElixirPuzzleOpenPolicyTutorMenu()
    {
        ElixirPuzzleBoard.TestB111();
        tutorMenu.SetActive(false);
        isPolicyFromTutor = true;
        policyMenu.SetActive(true);
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        ElixirPuzzleClickSound();
    }

    public void ElixirPuzzleClosePolicyMenu()
    {
        ElixirPuzzleBoard.TestB111();
        policyMenu.SetActive(false);
        if (isPolicyFromTutor)
        {
            isPolicyFromTutor = false;
            tutorMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
        }
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        ElixirPuzzleClickSound();
    }

    public void ElixirPuzzleOpenRulesMenu()
    {
        ElixirPuzzleBoard.TestB111();
        mainMenu.SetActive(false);
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        rulesMenu.SetActive(true);
        ElixirPuzzleClickSound();
    }

    public void ElixirPuzzleCloseRulesMenu()
    {
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        rulesMenu.SetActive(false);
        if (isRulesFromGame)
        {
            isRulesFromGame = false;
            Time.timeScale = 1;
            gameMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
        }
        ElixirPuzzleClickSound();
    }

    public void ElixirPuzzleOpenRulesGame()
    {
        Time.timeScale = 0;
        isRulesFromGame = true;
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        gameMenu.SetActive(false);
        rulesMenu.SetActive(true);
        ElixirPuzzleClickSound();
    }

    public void ElixirPuzzleWinMenuMainMenu()
    {
        gameMenu.SetActive(false);
        elixirPuzzletimer.ElixirPuzzlePauseTimer();
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        winMenu.SetActive(false);
        mainMenu.SetActive(true);
        elixirPuzzleboard.gameObject.SetActive(true);
        ElixirPuzzleClickSound();
    }
}