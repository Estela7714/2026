using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public SaveData CurrentData { get; private set; } = new SaveData();
    private FileDataHandler fileHandler;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        fileHandler = new FileDataHandler(Application.persistentDataPath, "");
    }

    public bool HasAutoSave() => fileHandler.Load(0) != null;

    public void SaveGame(int slot)
    {
        fileHandler.Save(CurrentData, slot);
        if (slot != 0) fileHandler.Save(CurrentData, 0); // Replica no slot 0 (AutoSave)
    }

    public void LoadGame(int slot)
    {
        SaveData loaded = fileHandler.Load(slot);
        if (loaded != null)
        {
            CurrentData = loaded;
            if (slot != 0) fileHandler.Save(CurrentData, 0); // Replica slot carregado no slot 0
        }
    }

    public void NewGame()
    {
        CurrentData = new SaveData { currentLevelIndex = 1, reachedCheckpoint = false, coinsAtCheckpoint = 0 };
    }
}