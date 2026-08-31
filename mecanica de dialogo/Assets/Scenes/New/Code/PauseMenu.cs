using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject slotsPanel;

    private bool isSavingMode = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        bool state = !pausePanel.activeSelf;
        pausePanel.SetActive(state);
        Time.timeScale = state ? 0f : 1f;
        if (!state) slotsPanel.SetActive(false);
    }

    public void OpenSaveSlots()
    {
        isSavingMode = true;
        slotsPanel.SetActive(true);
    }

    public void OpenLoadSlots()
    {
        isSavingMode = false;
        slotsPanel.SetActive(true);
    }

    public void OnCloseSlots()
    {
        if (slotsPanel != null)
            slotsPanel.SetActive(false);
    }


    public void SelectSlot(int slot)
    {
        if (isSavingMode)
        {
            SaveManager.Instance.SaveGame(slot);
            slotsPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            SaveManager.Instance.LoadGame(slot);
            SceneManager.LoadScene(SaveManager.Instance.CurrentData.currentLevelIndex);
        }
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}