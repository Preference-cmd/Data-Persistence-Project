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
    public class Player
    {
        public string playerName;
        private int playerHighestScore;
        public int playerCurrentScore;
        private int[] playerRecentScores;
    }



}
