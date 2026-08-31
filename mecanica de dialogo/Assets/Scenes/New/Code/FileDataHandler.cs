using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dirPath;
    private string fileNameCustom;
    private string encryptionKey = "SuaChaveSecretaAqui";

    public FileDataHandler(string dirPath, string fileNameCustom)
    {
        this.dirPath = dirPath;
        this.fileNameCustom = fileNameCustom;
    }

    public SaveData Load(int slot)
    {
        string fullPath = Path.Combine(dirPath, $"save_slot_{slot}.dat");
        if (!File.Exists(fullPath)) return null;

        try
        {
            string dataToLoad = "";
            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }
            dataToLoad = EncryptDecrypt(dataToLoad);
            return JsonUtility.FromJson<SaveData>(dataToLoad);
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao carregar slot {slot}: " + e.Message);
            return null;
        }
    }

    public void Save(SaveData data, int slot)
    {
        string fullPath = Path.Combine(dirPath, $"save_slot_{slot}.dat");
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataToStore = JsonUtility.ToJson(data, true);
            dataToStore = EncryptDecrypt(dataToStore);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Erro ao salvar slot {slot}: " + e.Message);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string result = "";
        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ encryptionKey[i % encryptionKey.Length]);
        }
        return result;
    }
}