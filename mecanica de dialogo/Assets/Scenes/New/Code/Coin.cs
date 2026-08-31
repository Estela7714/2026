using UnityEngine;

public class Coin : MonoBehaviour
{
    public string coinID;

    private void Awake()
    {
        // Gera ID único baseado na posição se não for definido manualmente
        if (string.IsNullOrEmpty(coinID))
            coinID = transform.position.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.CollectCoin(coinID);
            gameObject.SetActive(false);
        }
    }
}