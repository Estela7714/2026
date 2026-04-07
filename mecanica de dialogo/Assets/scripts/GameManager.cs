using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para trocar de cena
using UnityEngine.InputSystem;    // Necessário para o novo Input System
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameState currentState;

    private void Awake()
    {
        // Setup do Singleton
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
        // Adicione esta linha abaixo se ela não existir:
        RequestSceneLoad("Splash"); 
    }

    // Função única para mudar o estado e avisar no Console
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        Debug.Log($"<color=cyan>Estado do Jogo alterado para: {currentState}</color>");
    }

    // O "Segurança": Só ele troca de cena
    public void RequestSceneLoad(string sceneName)
    {
        // Exemplo de regra: Só muda para Gameplay se vier do Menu
        SceneManager.LoadScene(sceneName);
    }

    // Alocação de Input (Simplificada para Single Player)
    public void AssignPlayerInput(PlayerInput playerInput)
    {
        // Aqui você poderia forçar o esquema de controle (Teclado ou Controle)
        Debug.Log("Input alocado para o jogador.");
    }
}