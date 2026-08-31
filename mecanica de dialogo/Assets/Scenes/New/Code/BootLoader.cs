using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
        // Certifique-se de carregar APENAS a cena do MainMenu
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}