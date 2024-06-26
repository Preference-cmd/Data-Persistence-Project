using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject searchBar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        // Get the name from the search bar
        string name = searchBar.GetComponent<TMP_InputField>().text;
        GameManager.Instance.RegisterNewPlayer(name);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {

        GameManager.Instance.SaveProfile();


    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit();
    #endif
    }
}
