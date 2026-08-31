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
        SaveManager.Instance.LoadGame(1);
        SceneManager.LoadScene(SaveManager.Instance.CurrentData.currentLevelIndex);
    }

    public void OnNewGame()
    {
        SaveManager.Instance.NewGame();
        // Use o nome exato da sua primeira cena de fase entre aspas
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
    }
}