using UnityEngine;

public class Coin : MonoBehaviour
{
    public string coinID;
    private bool isCollected = false; // Impede dupla coleta no mesmo frame

    private void Awake()
    {
        // Gera ID único baseado na posição se não for definido manualmente
        if (string.IsNullOrEmpty(coinID))
            coinID = transform.position.ToString();
    }

    private void OnEnable()
    {
        // Reseta o estado caso a moeda seja reativada
        isCollected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Garante que só coleta se for o Player e se AINDA NÃO tiver sido coletada
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true; // Trava para não executar de novo

            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.CollectCoin(coinID);
            }

            gameObject.SetActive(false);
        }
    }
}