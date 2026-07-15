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

    private void OnEnable()
    {
        // Se inscreve no evento de carregamento de cena da Unity
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Remove a inscrição para evitar vazamento de memória
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
        // Se estiver trocando para uma cena que NÃO é a gameplay, garante que a UI seja removida
        if (sceneName != "CenaGameplay" && SceneManager.GetSceneByName("CenaUI").isLoaded)
        {
            SceneManager.UnloadSceneAsync("CenaUI");
        }

        SceneManager.LoadScene(sceneName);
    }

    // Método chamado automaticamente sempre que qualquer cena termina de carregar
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "CenaGameplay")
        {
            ChangeState(GameState.Gameplay);

            // Carrega a interface gráfica em modo aditivo por cima da cena do jogo
            if (!SceneManager.GetSceneByName("CenaUI").isLoaded)
            {
                SceneManager.LoadScene("CenaUI", LoadSceneMode.Additive);
            }
        }
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
            FinalizarPartida(1, dadosEscolhidoJ1 != null ? dadosEscolhidoJ1.nomeDaBolinha : "Jogador 1");
        }
        else if (vitoriasJ2 >= 2)
        {
            FinalizarPartida(2, dadosEscolhidoJ2 != null ? dadosEscolhidoJ2.nomeDaBolinha : "Jogador 2");
        }
        else
        {
            // Recarrega a arena para o novo round
            RequestSceneLoad("CenaGameplay");
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