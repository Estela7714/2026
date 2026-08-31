using UnityEngine;
using TMPro; // Usado caso esteja usando TextMeshPro (se usar a UI antiga do Unity, troque por UnityEngine.UI)
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("HUD")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI notificationText;

    [Header("Painel de Vitória")]
    public GameObject victoryPanel;
    public TextMeshProUGUI victoryCoinText;
    public TextMeshProUGUI pressKeyText;

    private bool waitingForNextLevelInput = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        // Ao vencer a fase, detecta qualquer tecla para avançar
        if (waitingForNextLevelInput && Input.anyKeyDown)
        {
            waitingForNextLevelInput = false;
            LoadNextLevel();
        }
    }

    public void UpdateCoinCount(int currentCoins)
    {
        if (coinText != null)
        {
            coinText.text = "Moedas: " + currentCoins;
        }
    }

    public void ShowMessage(string message)
    {
        if (notificationText != null)
        {
            StopAllCoroutines();
            StartCoroutine(MessageRoutine(message));
        }
    }

    private IEnumerator MessageRoutine(string message)
    {
        notificationText.text = message;
        notificationText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        notificationText.gameObject.SetActive(false);
    }

    public void ShowVictoryPanel(int coinsCollected, int totalCoins)
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);

            if (victoryCoinText != null)
                victoryCoinText.text = $"Moedas: {coinsCollected} / {totalCoins}";

            if (pressKeyText != null)
                pressKeyText.text = "Pressione qualquer tecla para continuar...";

            waitingForNextLevelInput = true;
        }
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Se houver uma próxima fase na Build Settings, carrega ela; senão volta pro Menu
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(0); // Assumindo que 0 é o Menu Inicial
        }
    }
}