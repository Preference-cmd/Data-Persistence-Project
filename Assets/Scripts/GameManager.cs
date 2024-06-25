using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Player currentPlayer;
    private List<Player> currentPlayerList = new();

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Setup singleton and don't destroy on load
        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    // Save playergroup data
    [Serializable]
    class SaveData
    {
        public List<Player> players;
    }

    public void SavePlayer()
    {

    }

    public void SaveProfile()
    {
        // Initialize save data
        SaveData saveData = new()
        {
            players = currentPlayerList
        };

        // Serialize save data to JSON and save to file
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadProfile()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        SaveData data = new();

        // Deserialize JSON file to save data
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
        }

        // Set current player list
        currentPlayerList = data.players;
    }

    public List<Player> GetPlayerList()
    {
        // Init the current player list if it's null
        if (currentPlayerList == null)
        {
            LoadProfile();
        }

        return currentPlayerList;
    }
}

[Serializable]
public class Player
{
    public string playerName;
    public int playerHighestScore;
    public int playerCurrentScore;
}
