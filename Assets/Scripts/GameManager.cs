using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    // Player class definition


    // Save a player's data
    [Serializable]
    class SaveData
    {
        public Player playerSaving;
    }

    public void SaveProfile(Player player)
    {
        SaveData saveData = new();
        saveData.playerSaving = player;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/playerInfo.json", json);
    }
}

[Serializable]
public class Player
{
    public string playerName;
    public int playerHighestScore;
    public int playerCurrentScore;
    public int[] playerRecentScores;
}