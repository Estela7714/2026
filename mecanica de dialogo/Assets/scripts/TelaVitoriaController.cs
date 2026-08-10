using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelaVitoriaController : MonoBehaviour
{
    [Header("Componentes de UI")]
    public TextMeshProUGUI textoVencedor;
    public TextMeshProUGUI textoDetalhesBolinha;
    public TextMeshProUGUI textoPlacar;

    [Tooltip("Arraste o botão de avançar")]
    public Button btnAvancar;

    [Tooltip("Texto dentro do botão")]
    public TextMeshProUGUI textoBotao;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeState(GameState.Vitoria);

            bool jogoAcabou = GameManager.Instance.partidaFinalizada;

            if (jogoAcabou)
            {
                int ganhadorJogo = GameManager.Instance.jogadorVencedorDaPartida;
                string nomeBolinha = GameManager.Instance.nomeBolinhaVencedoraPartida;

                if (textoVencedor != null) textoVencedor.text = $"PARABÉNS!\nJOGADOR {ganhadorJogo} VENCEU O JOGO!";
                if (textoDetalhesBolinha != null) textoDetalhesBolinha.text = $"Bolinha Campeã: {nomeBolinha}";
                if (textoBotao != null) textoBotao.text = "NOVO JOGO";
            }
            else
            {
                int ganhadorRodada = GameManager.Instance.jogadorVencedorDaRodada;
                string nomeBolinha = GameManager.Instance.nomeBolinhaVencedoraRodada;

                if (textoVencedor != null) textoVencedor.text = $"JOGADOR {ganhadorRodada} VENCEU A RODADA!";
                if (textoDetalhesBolinha != null) textoDetalhesBolinha.text = $"Bolinha: {nomeBolinha}";
                if (textoBotao != null) textoBotao.text = "PRÓXIMA RODADA";
            }

            if (textoPlacar != null)
            {
                textoPlacar.text = $"PLACAR: J1 [{GameManager.Instance.vitoriasJ1}]  X  [{GameManager.Instance.vitoriasJ2}] J2";
            }
        }

        if (btnAvancar != null)
        {
            // Remove ouvintes anteriores para garantir que não haja chamadas duplicadas/bloqueadas
            btnAvancar.onClick.RemoveAllListeners();
            btnAvancar.onClick.AddListener(AcaoBotao);
        }
    }

    public void AcaoBotao()
    {
        Debug.Log("<color=yellow>Botão Avançar Clicado!</color>");

        if (GameManager.Instance == null) return;

        if (GameManager.Instance.partidaFinalizada)
        {
            GameManager.Instance.ResetarPartida();
            GameManager.Instance.ChangeState(GameState.SelecaoBolinhas);
            GameManager.Instance.RequestSceneLoad("CenaSelecao");
        }
        else
        {
            GameManager.Instance.ChangeState(GameState.SelecaoBolinhas);
            GameManager.Instance.RequestSceneLoad("CenaSelecao");
        }
    }
}