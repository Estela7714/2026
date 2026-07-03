using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header("Referências das Bolinhas na Cena")]
    [Tooltip("Arraste o objeto da Bolinha do Jogador 1 que já está na cena")]
    public BolinhaController jogador1;

    [Tooltip("Arraste o objeto da Bolinha do Jogador 2 que já está na cena")]
    public BolinhaController jogador2;

    [Header("Pontos de Renascimento (Spawn Points)")]
    [Tooltip("Arraste um objeto vazio para ser a posição inicial do J1")]
    public Transform spawnPointJ1;

    [Tooltip("Arraste um objeto vazio para ser a posição inicial do J2")]
    public Transform spawnPointJ2;

    void Start()
    {
        // 1. Garante que o GameManager mudou o estado para Gameplay
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeState(GameState.Gameplay);
        }

        // 2. Reposiciona as bolinhas nos seus respectivos lugares de início
        if (spawnPointJ1 != null && jogador1 != null)
            jogador1.transform.position = spawnPointJ1.position;

        if (spawnPointJ2 != null && jogador2 != null)
            jogador2.transform.position = spawnPointJ2.position;

        // 3. Inicializa as bolinhas injetando os dados guardados do GameManager
        if (GameManager.Instance != null)
        {
            if (jogador1 != null)
            {
                jogador1.Inicializar(GameManager.Instance.dadosEscolhidoJ1, 1, jogador2);
            }

            if (jogador2 != null)
            {
                jogador2.Inicializar(GameManager.Instance.dadosEscolhidoJ2, 2, jogador1);
            }
        }
        else
        {
            Debug.LogError("GameplayManager: GameManager não foi encontrado! As bolinhas usarão dados padrão.");
        }
    }
}
