using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipesDeliveredText;
    [SerializeField] TextMeshProUGUI moneyEarnedText;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button playAgainButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => Loader.Load(Loader.Scene.MainMenuScene));
        playAgainButton.onClick.AddListener(() => Loader.Load(Loader.Scene.GameScene));
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGameOver += GameOver;

        gameObject.SetActive(false);
    }

    public void GameOver(int deliveredRecipesCount, int moneyEarnedCount)
    {
        gameObject.SetActive(true);
        recipesDeliveredText.text = deliveredRecipesCount.ToString();
        moneyEarnedText.text = moneyEarnedCount.ToString();
    }
}
