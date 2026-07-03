using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Estado do Jogo")]
    [SerializeField] private GameState currentState;

    [Header("Dados de Seleção Temporários")]
    public BolinhaData dadosEscolhidoJ1;
    public BolinhaData dadosEscolhidoJ2;

    [Header("Controle de Rounds")]
    public int vitoriasJ1 = 0;
    public int vitoriasJ2 = 0;
    public int jogadorVencedorDaPartida = 0;
    public string nomeBolinhaVencedora = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeState(GameState.Iniciando);
        RequestSceneLoad("Splash");
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log($"<color=cyan>Estado do Jogo alterado para: {currentState}</color>");
    }

    public void RequestSceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Chamado pelo sistema de verificação de queda na Arena
    public void RegistrarQuedaJogador(int idJogadorQueCaiu)
    {
        // Se o J1 caiu, ponto do J2. Se o J2 caiu, ponto do J1.
        if (idJogadorQueCaiu == 1) vitoriasJ2++;
        else vitoriasJ1++;

        Debug.Log($"Placar Atual: J1 [{vitoriasJ1}] vs J2 [{vitoriasJ2}]");

        if (vitoriasJ1 >= 2)
        {
            FinalizarPartida(1, dadosEscolhidoJ1.nomeDaBolinha);
        }
        else if (vitoriasJ2 >= 2)
        {
            FinalizarPartida(2, dadosEscolhidoJ2.nomeDaBolinha);
        }
        else
        {
            // Reinicia o Round atual relendo a cena de Gameplay
            RequestSceneLoad(SceneManager.GetActiveScene().name);
        }
    }

    private void FinalizarPartida(int idGanhador, string nomeBolinha)
    {
        jogadorVencedorDaPartida = idGanhador;
        nomeBolinhaVencedora = nomeBolinha;

        // Reseta placar para a próxima partida futura
        vitoriasJ1 = 0;
        vitoriasJ2 = 0;

        ChangeState(GameState.Vitoria);
        RequestSceneLoad("CenaVitoria");
    }
}