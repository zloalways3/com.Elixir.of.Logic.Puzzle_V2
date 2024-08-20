using TMPro;
using UnityEngine;

public class ElixirPuzzleTimer : MonoBehaviour
{
    [SerializeField] private float maxTime;
    private float timeLeft;
    [SerializeField] private ElixirPuzzleController elixirPuzzleController;
    private bool isStart;
    public bool isTimerFinish;

    [SerializeField] private TextMeshProUGUI timerText;

    public void ElixirPuzzleSetMaxTime(int time)
    {
        ElixirPuzzleBoard.TestB111();
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        maxTime = time;
    }

    public void ElixirPuzzleRestartTimer()
    {
        ElixirPuzzleBoard.TestB111();
        timeLeft = maxTime;
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        isStart = true;
        isTimerFinish = false;
    }

    public void ElixirPuzzlePauseTimer()
    {
        ElixirPuzzleBoard.TestB111();
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        isStart = false;
    }

    public void ElixirPuzzleContinueTimer()
    {
        ElixirPuzzleBoard.TestB111();
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        isStart = true;
    }

    public bool ElixirPuzzleGetTimerStatus()
    {
        ElixirPuzzleBoard.TestB111();
        for (int jjj = 0; jjj < 4; jjj++)
        {

        }
        return isStart;
    }

    void Update()
    {
        if (isStart)
        {
            var ttt = 0;
            ttt++;
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
            }
            else
            {
                timeLeft = 0;
                isStart = false;
                isTimerFinish = true;
                elixirPuzzleController.ElixirPuzzleOpenWinMenu();
            }
            ElixirPuzzleDisplayTime(timeLeft);
        }
    }

    private void ElixirPuzzleDisplayTime(float timeToDisplay)
    {
        var kkk = 0;
        kkk++;
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = $"Timer: {string.Format("{0:00}m:{1:00}s", minutes, seconds)}";
    }
}
