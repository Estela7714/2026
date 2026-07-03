using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelaSelecaoManager : MonoBehaviour
{
    [Header("Banco de Dados de Bolinhas")]
    [Tooltip("Arraste os 5 Scriptable Objects (BolinhaData) criados aqui no Inspector")]
    public BolinhaData[] todasAsBolinhas;

    [Header("UI - Jogador 1")]
    public TextMeshProUGUI textoNomeJ1;
    public Image imagemBolinhaJ1;
    public Button btnVoltarJ1;
    public Button btnAvancarJ1;
    public Toggle toggleProntoJ1;

    [Header("UI - Jogador 2")]
    public TextMeshProUGUI textoNomeJ2;
    public Image imagemBolinhaJ2;
    public Button btnVoltarJ2;
    public Button btnAvancarJ2;
    public Toggle toggleProntoJ2;

    [Header("UI - Geral")]
    public Button btnIniciarPartida;

    private int indiceJ1 = 0;
    private int indiceJ2 = 0;

    void Start()
    {
        // Força o estado correto no GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeState(GameState.SelecaoBolinhas);
        }

        // Configura os botões do Jogador 1
        btnVoltarJ1.onClick.AddListener(() => MudarSelecaoJ1(-1));
        btnAvancarJ1.onClick.AddListener(() => MudarSelecaoJ1(1));
        toggleProntoJ1.onValueChanged.AddListener((valor) => ChecarProntos());

        // Configura os botões do Jogador 2
        btnVoltarJ2.onClick.AddListener(() => MudarSelecaoJ2(-1));
        btnAvancarJ2.onClick.AddListener(() => MudarSelecaoJ2(1));
        toggleProntoJ2.onValueChanged.AddListener((valor) => ChecarProntos());

        // Botão de Iniciar Partida (começa desativado)
        btnIniciarPartida.onClick.AddListener(ConfirmarEIniciar);
        btnIniciarPartida.interactable = false;

        // Atualiza a interface visual inicial
        AtualizarVisualJ1();
        AtualizarVisualJ2();
    }

    void MudarSelecaoJ1(int direcao)
    {
        if (toggleProntoJ1.isOn) return; // Bloqueia mudança se já estiver pronto

        indiceJ1 += direcao;
        if (indiceJ1 < 0) indiceJ1 = todasAsBolinhas.Length - 1;
        if (indiceJ1 >= todasAsBolinhas.Length) indiceJ1 = 0;

        AtualizarVisualJ1();
    }

    void MudarSelecaoJ2(int direcao)
    {
        if (toggleProntoJ2.isOn) return; // Bloqueia mudança se já estiver pronto

        indiceJ2 += direcao;
        if (indiceJ2 < 0) indiceJ2 = todasAsBolinhas.Length - 1;
        if (indiceJ2 >= todasAsBolinhas.Length) indiceJ2 = 0;

        AtualizarVisualJ2();
    }

    void AtualizarVisualJ1()
    {
        if (todasAsBolinhas.Length == 0) return;
        BolinhaData dados = todasAsBolinhas[indiceJ1];
        textoNomeJ1.text = dados.nomeDaBolinha;
        imagemBolinhaJ1.sprite = dados.fotoMenu;
    }

    void AtualizarVisualJ2()
    {
        if (todasAsBolinhas.Length == 0) return;
        BolinhaData dados = todasAsBolinhas[indiceJ2];
        textoNomeJ2.text = dados.nomeDaBolinha;
        imagemBolinhaJ2.sprite = dados.fotoMenu;
    }

    void ChecarProntos()
    {
        // O botão de iniciar só fica ativo se ambos os jogadores marcarem "Pronto"
        btnIniciarPartida.interactable = toggleProntoJ1.isOn && toggleProntoJ2.isOn;

        // Desativa ou ativa os botões de seta dependendo do status de "Pronto"
        btnVoltarJ1.interactable = !toggleProntoJ1.isOn;
        btnAvancarJ1.interactable = !toggleProntoJ1.isOn;

        btnVoltarJ2.interactable = !toggleProntoJ2.isOn;
        btnAvancarJ2.interactable = !toggleProntoJ2.isOn;
    }

    void ConfirmarEIniciar()
    {
        if (GameManager.Instance == null) return;

        // Salva os Scriptable Objects escolhidos dentro do seu novo GameManager
        GameManager.Instance.dadosEscolhidoJ1 = todasAsBolinhas[indiceJ1];
        GameManager.Instance.dadosEscolhidoJ2 = todasAsBolinhas[indiceJ2];

        // Altera o estado e carrega a cena de Gameplay
        GameManager.Instance.ChangeState(GameState.Gameplay);
        GameManager.Instance.RequestSceneLoad("CenaGameplay"); // Certifique-se de usar o nome exato da sua cena
    }
}