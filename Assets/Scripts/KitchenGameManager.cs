using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public Action<int, int> OnGameOver;

    enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    State state;
    bool gamePaused = false;
    public float CountdownToStartTimer { get; private set; } = 3f;
    float gamePlayingTimer;
    float gamePlayingTimerMax = 60f; 
    bool firstTimeMoneyEnrollment = true;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        PauseGameButtonUI.Instance.OnPause += PauseGameButtonUI_OnPause;
        PauseGameUI.Instance.OnResume += PauseGameUI_OnUnpause;
        TutorialUI.OnResume += PauseGameUI_OnUnpause;
    }

    private void PauseGameButtonUI_OnPause(object sender, EventArgs e)
    {
        Time.timeScale = 0f;
        gamePaused = true;
    }
    private void PauseGameUI_OnUnpause(object sender, EventArgs e)
    {
        Time.timeScale = 1f;
        gamePaused = false;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                Time.timeScale = 0f;
                if (!SaveLoadManager.GetIsFirstTimePlayInfo())
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountdownToStart:
                if (!gamePaused && !YandexGame.nowFullAd)
                {
                    Time.timeScale = 1f;
                }
                CountdownToStartTimer -= Time.deltaTime;
                if (CountdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                if (firstTimeMoneyEnrollment)
                {
                    SaveLoadManager.SaveMoneyData(DeliveryManager.Instance.MoneyEarned);
                    OnGameOver?.Invoke(DeliveryManager.Instance.SuccessfulRecipesAmount, DeliveryManager.Instance.MoneyEarned);
                    firstTimeMoneyEnrollment = false;
                }
                break;
        }
    }

    public bool IsGamePlaying() => state == State.GamePlaying;

    public bool IsCountdownToStartActive() => state == State.CountdownToStart;

    public bool IsGameOver() => state == State.GameOver;

    public float GetPlayingTimerNormalized() => 1 - (gamePlayingTimer / gamePlayingTimerMax);
}
