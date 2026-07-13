using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class BolinhaController : MonoBehaviour
{
    [Header("Configura��es de Dados")]
    public BolinhaData dadosBolinha;
    public int idJogador; // 1 para Jogador 1, 2 para Jogador 2
    public BolinhaController inimigo;

    [Header("Mapeamento de Inputs")]
    public InputActionReference moveAction;
    public InputActionReference empurrarAction;

    [Header("Status em Tempo de Execu��o")]
    public int moedasColetadas = 0;

    private float tempoUltimoEmpurrao;
    private float tempoCooldown = 2f; // Tempo em segundos para poder empurrar de novo
    private Rigidbody rb;

    // Evento (Observer) para a UI escutar quando o cooldown iniciar
    public static event Action<int, float> OnEmpurraoUsado; // idJogador, tempoCooldown

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Inicializa a bolinha com os dados escolhidos na tela de sele��o
    public void Inicializar(BolinhaData dados, int id, BolinhaController oInimigo)
    {
        dadosBolinha = dados;
        idJogador = id;
        inimigo = oInimigo;

        // Aplica modifica��es visuais e f�sicas do ScriptableObject
        transform.localScale = Vector3.one * dadosBolinha.tamanhoEscala;
        rb.mass = dadosBolinha.massaRigidbody;

        GetComponent<Renderer>().material = (idJogador == 1) ? dadosBolinha.corJogador1 : dadosBolinha.corJogador2;
    }

    void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
        if (empurrarAction != null)
        {
            empurrarAction.action.Enable();
            empurrarAction.action.performed += OnEmpurrarPerformado;
        }
    }

    void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
        if (empurrarAction != null)
        {
            empurrarAction.action.performed -= OnEmpurrarPerformado;
            empurrarAction.action.Disable();
        }
    }

    void FixedUpdate()
    {
        if (moveAction == null) return;

        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 direcaoMovimento = new Vector3(input.x, 0f, input.y);

        // Modificadores de moeda: Mais moedas = Mais lento
        float modificadorVelocidade = Mathf.Max(0.3f, 1f - (moedasColetadas * 0.08f));
        float velocidadeAtual = dadosBolinha.velocidadeInicial * modificadorVelocidade;

        rb.AddForce(direcaoMovimento * velocidadeAtual, ForceMode.Force);

        // Limita a velocidade m�xima
        if (rb.linearVelocity.magnitude > velocidadeAtual)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * velocidadeAtual;
        }
    }

    private void OnEmpurrarPerformado(InputAction.CallbackContext context)
    {
        if (Time.time < tempoUltimoEmpurrao + tempoCooldown) return; // Em Cooldown
        if (inimigo == null) return;

        tempoUltimoEmpurrao = Time.time;
        OnEmpurraoUsado?.Invoke(idJogador, tempoCooldown);

        // C�lculos de dist�ncia e dire��o oposta
        float distancia = Vector3.Distance(transform.position, inimigo.transform.position);
        if (distancia < 0.1f) distancia = 0.1f; // Evita divis�o por zero

        Vector3 direcaoInimigo = (inimigo.transform.position - transform.position).normalized;
        direcaoInimigo.y = 0f; // Garante for�a apenas no plano horizontal

        // Modificador de moedas: Mais moedas = Mais for�a de empurr�o aplicada
        float modificadorForcaMoeda = 1f + (moedasColetadas * 0.15f);

        // F�rmula pedida: Mais perto = Mais forte. Multiplicado pela for�a base e moedas
        float forcaFinal = (dadosBolinha.forcaEmpurraoBase * modificadorForcaMoeda) / distancia;

        // Aplica a for�a empurrando o inimigo para longe
        inimigo.GetComponent<Rigidbody>().AddForce(direcaoInimigo * forcaFinal, ForceMode.Impulse);
    }
    
}