using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelaVitoriaController : MonoBehaviour
{
    [Header("Componentes de UI")]
    [Tooltip("Arraste o texto que vai mostrar quem ganhou (Ex: JOGADOR 1 VENCEU!)")]
    public TextMeshProUGUI textoVencedor;

    [Tooltip("Arraste o texto que vai mostrar os detalhes da bolinha utilizada")]
    public TextMeshProUGUI textoDetalhesBolinha;

    [Tooltip("Arraste o botão de voltar para a seleção")]
    public Button btnVoltarSelecao;

    void Start()
    {
        // Força o estado do jogo para Vitória no GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeState(GameState.Vitoria);

            // 1. Lê os dados diretamente do GameManager e atualiza os textos
            int ganhador = GameManager.Instance.jogadorVencedorDaPartida;
            string nomeBolinha = GameManager.Instance.nomeBolinhaVencedora;

            textoVencedor.text = $"PARABÉNS!\nJOGADOR {ganhador} VENCEU A PARTIDA!";
            textoDetalhesBolinha.text = $"Bolinha Campeã: {nomeBolinha}";
        }
        else
        {
            textoVencedor.text = "Fim de Jogo!";
            textoDetalhesBolinha.text = "Não foi possível carregar os dados do GameManager.";
        }

        // 2. Configura o botão para retornar à tela de seleção de bolinhas
        if (btnVoltarSelecao != null)
        {
            btnVoltarSelecao.onClick.AddListener(VoltarParaSelecao);
        }
    }

    void VoltarParaSelecao()
    {
        if (GameManager.Instance != null)
        {
            // Muda o estado e pede para o GameManager carregar a cena de seleção
            GameManager.Instance.ChangeState(GameState.SelecaoBolinhas);
            GameManager.Instance.RequestSceneLoad("CenaSelecao"); // Certifique-se de usar o nome exato da sua cena
        }
    }
}