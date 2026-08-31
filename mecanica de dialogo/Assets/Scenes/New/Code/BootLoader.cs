using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    [Header("Configuração de Inicialização")]
    [Tooltip("Nome exato da cena do Menu Inicial na Build Settings")]
    public string mainMenuSceneName = "MainMenu";

    private void Start()
    {
        // Garante que o jogo comece com o tempo rodando normalmente
        Time.timeScale = 1f;

        // Troca para o Menu Inicial imediatamente
        SceneManager.LoadScene(mainMenuSceneName);
    }
}