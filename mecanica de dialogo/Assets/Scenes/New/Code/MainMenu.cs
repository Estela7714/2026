using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Paineis e Botões")]
    public Button continueButton;
    public GameObject slotsPanel;

    private void Start()
    {
        // Exibe o botão Continuar apenas se o AutoSave (Slot 0) existir
        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(SaveManager.Instance.HasAutoSave());
        }
    }

    public void OnContinue()
    {
        // CORRIGIDO: O AutoSave deve carregar o slot 0 e não o slot 1
        SaveManager.Instance.LoadGame(0);
        SceneManager.LoadScene(SaveManager.Instance.CurrentData.currentLevelIndex);
    }

    public void OnNewGame()
    {
        SaveManager.Instance.NewGame();
        SceneManager.LoadScene("Fase1");
    }

    public void OnOpenLoadSlots()
    {
        if (slotsPanel != null)
            slotsPanel.SetActive(true);
    }

    public void OnCloseSlots()
    {
        if (slotsPanel != null)
            slotsPanel.SetActive(false);
    }

    public void SelectSlotToLoad(int slot)
    {
        SaveManager.Instance.LoadGame(slot);
        SceneManager.LoadScene(SaveManager.Instance.CurrentData.currentLevelIndex);
    }

    public void OnQuit()
    {
        Application.Quit();

        // Para parar o Play Mode caso esteja testando dentro da Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}