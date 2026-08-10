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

    // Armazena quem venceu a RODADA ATUAL
    public int jogadorVencedorDaRodada = 0;
    public string nomeBolinhaVencedoraRodada = "";

    // Armazena quem venceu o JOGO (0 se a partida ainda estiver em andamento)
    public int jogadorVencedorDaPartida = 0;
    public string nomeBolinhaVencedoraPartida = "";

    public bool partidaFinalizada = false;

    private bool roundFinalizado = false;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
        // Se a CenaUI estiver aberta e formos sair da gameplay, descarrega a UI
        if (sceneName != "CenaGameplay")
        {
            Scene sceneUI = SceneManager.GetSceneByName("CenaUI");
            if (sceneUI.isLoaded)
            {
                SceneManager.UnloadSceneAsync("CenaUI");
            }
        }

        // Carrega a cena desejada em modo Single (substitui a cena atual completamente)
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CenaGameplay")
        {
            roundFinalizado = false;
            ChangeState(GameState.Gameplay);

            if (!SceneManager.GetSceneByName("CenaUI").isLoaded)
            {
                SceneManager.LoadScene("CenaUI", LoadSceneMode.Additive);
            }
        }
    }

    // Chamado pelo DetectorDeQueda na Arena
    public void RegistrarQuedaJogador(int idJogadorQueCaiu)
    {
        if (roundFinalizado) return;
        roundFinalizado = true;

        // Se o J1 caiu, o J2 vence a rodada. Se o J2 caiu, o J1 vence.
        if (idJogadorQueCaiu == 1)
        {
            vitoriasJ2++;
            jogadorVencedorDaRodada = 2;
            nomeBolinhaVencedoraRodada = dadosEscolhidoJ2 != null ? dadosEscolhidoJ2.nomeDaBolinha : "Jogador 2";
        }
        else
        {
            vitoriasJ1++;
            jogadorVencedorDaRodada = 1;
            nomeBolinhaVencedoraRodada = dadosEscolhidoJ1 != null ? dadosEscolhidoJ1.nomeDaBolinha : "Jogador 1";
        }

        Debug.Log($"Placar Atual: J1 [{vitoriasJ1}] vs J2 [{vitoriasJ2}]");

        // Verifica se alguém atingiu a quantidade de vitórias necessária (3)
        if (vitoriasJ1 >= 3)
        {
            FinalizarPartida(1, dadosEscolhidoJ1 != null ? dadosEscolhidoJ1.nomeDaBolinha : "Jogador 1");
        }
        else if (vitoriasJ2 >= 3)
        {
            FinalizarPartida(2, dadosEscolhidoJ2 != null ? dadosEscolhidoJ2.nomeDaBolinha : "Jogador 2");
        }
        else
        {
            // Apenas a rodada terminou (partida continua)
            partidaFinalizada = false;
            ChangeState(GameState.Vitoria);
            RequestSceneLoad("CenaVitoria");
        }
    }

    private void FinalizarPartida(int idGanhador, string nomeBolinha)
    {
        partidaFinalizada = true;
        jogadorVencedorDaPartida = idGanhador;
        nomeBolinhaVencedoraPartida = nomeBolinha;

        ChangeState(GameState.Vitoria);
        RequestSceneLoad("CenaVitoria");
    }

    // Método chamado para reiniciar o placar após o fim da partida completa
    public void ResetarPartida()
    {
        vitoriasJ1 = 0;
        vitoriasJ2 = 0;
        jogadorVencedorDaPartida = 0;
        nomeBolinhaVencedoraPartida = "";
        partidaFinalizada = false;
    }
}