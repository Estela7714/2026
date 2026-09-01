using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Configurações da Fase")]
    public Transform playerSpawnPoint;
    public Checkpoint levelCheckpoint;
    public Transform playerTransform;
    public int totalCoinsInLevel;

    public int CurrentCoinsSession { get; private set; }
    private HashSet<string> collectedCoinsSession = new HashSet<string>();

    private void Awake() => Instance = this;

    private void Start()
    {
        // Garante que o SaveData guarde o índice correto da fase atual
        SaveManager.Instance.CurrentData.currentLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        totalCoinsInLevel = FindObjectsOfType<Coin>().Length;
        SetupPhase();
    }

    private void SetupPhase()
    {
        SaveData data = SaveManager.Instance.CurrentData;

        if (data.reachedCheckpoint)
        {
            // Teleporta para o centro do checkpoint
            playerTransform.position = levelCheckpoint.centerPoint.position;
            CurrentCoinsSession = data.coinsAtCheckpoint;

            // Esconde moedas já pegas antes do checkpoint
            foreach (var coin in FindObjectsOfType<Coin>())
            {
                if (data.collectedCoinIDsAtCheckpoint.Contains(coin.coinID))
                    coin.gameObject.SetActive(false);
            }
        }
        else
        {
            playerTransform.position = playerSpawnPoint.position;
            CurrentCoinsSession = 0;
        }

        UIManager.Instance.UpdateCoinCount(CurrentCoinsSession);
    }

    public void CollectCoin(string coinID)
    {
        CurrentCoinsSession++;
        collectedCoinsSession.Add(coinID);
        UIManager.Instance.UpdateCoinCount(CurrentCoinsSession);
    }

    public void ActivateCheckpoint()
    {
        SaveData data = SaveManager.Instance.CurrentData;
        data.reachedCheckpoint = true;
        data.coinsAtCheckpoint = CurrentCoinsSession;
        data.collectedCoinIDsAtCheckpoint = new List<string>(collectedCoinsSession);

        SaveManager.Instance.SaveGame(0); // AutoSave no slot 0
        UIManager.Instance.ShowMessage("Checkpoint Ativado!");
    }

    public void TriggerVictory()
{
    // 1. Atualiza os dados no SaveManager para a PRÓXIMA FASE
    SaveData data = SaveManager.Instance.CurrentData;
    
    data.currentLevelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1; // Avança o índice da fase
    data.reachedCheckpoint = false; // RESETA o checkpoint para a nova fase
    data.coinsAtCheckpoint = 0;     // RESETA o contador de moedas no checkpoint
    data.collectedCoinIDsAtCheckpoint.Clear(); // LIMPA as moedas coletadas salvas

    // 2. Executa o AutoSave no Slot 0 com os dados limpos da Fase 2
    SaveManager.Instance.SaveGame(0);

    // 3. Exibe o painel de vitória na tela
    UIManager.Instance.ShowVictoryPanel(CurrentCoinsSession, totalCoinsInLevel);
}
}