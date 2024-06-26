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

    public void RegisterNewPlayer(string playerName)
    {
        // See if player already exists
        foreach (Player player in currentPlayerList)
        {
            if (player.playerName == playerName)
            {
                Debug.Log("Player already exists");
                currentPlayer = player;
                return;
            }
        }

        // Create new player if player doesn't exist
        currentPlayer = new()
        {
            playerName = playerName,
        };
        Debug.Log("New Player Created");
    }

    public void UpdateCurrentPlayer(int playerScore)
    {
        // Update current player score and check if it is a new high score
        currentPlayer.playerCurrentScore = playerScore;

        if (currentPlayer.playerHighestScore < playerScore)
        {
            currentPlayer.playerHighestScore = playerScore;
        }

        currentPlayerList.Add(currentPlayer);
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
