using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchBar : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Dropdown dropDown;
    public List<string> searchLibrary = new();
    public List<string> searchResult = new();


    // Start is called before the first frame update
    void Start()
    {
        InitSearchLibrary();
        InitDropDown();
        dropDown.onValueChanged.AddListener(
            delegate {
                string selectedPlayer = dropDown.transform.Find("Label").GetComponent<TMP_Text>().text;
                Debug.Log(selectedPlayer);
                inputField.text = selectedPlayer;
                HideInputField();
            }
        );

        inputField.onEndEdit.AddListener(
            delegate {
                FilterSearchResult();
                ShowResult();
            }
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (inputField.isFocused && inputField.placeholder.gameObject.activeSelf == false)
        {
            ShowInputField();
        }  
    }

    private void InitSearchLibrary()
    {
        // Get player list from GameManager
        List<Player> players = GameManager.Instance.GetPlayerList();
        foreach (Player player in players)
        {
            searchLibrary.Add(player.playerName);
        }
    }

    private void InitDropDown()
    {
        dropDown.ClearOptions();
        dropDown.options.Add(new TMP_Dropdown.OptionData());
        foreach (string playerName in searchLibrary)
        {
            TMP_Dropdown.OptionData option = new()
            {
                text = playerName
            };
            dropDown.options.Add(option);
        }
    }

    private void HideInputField()
    {
        inputField.GetComponent<Image>().color = new(1, 1, 1, 0);
        inputField.placeholder.gameObject.SetActive(false);
        inputField.textComponent.gameObject.SetActive(false);
    }

    private void ShowInputField()
    {
        inputField.GetComponent<Image>().color = new(1, 1, 1, 1);
        inputField.placeholder.gameObject.SetActive(true);
        inputField.textComponent.gameObject.SetActive(true);
    }

    // Set the text of the input field placeholder
    public void SetPlaceHolderText(string text)
    {
        inputField.placeholder.GetComponent<TMP_Text>().text = text;
    }

    private void FilterSearchResult()
    {
        searchResult = TextMatch(inputField.textComponent.text, searchLibrary);
    }

    // Find the player with the same part of the name as the input field text
    public List<string> TextMatch(string inputText, List<string> matchList)
    {
        List<string> result = new();
        foreach (string playerName in matchList)
        {
            if (playerName.Contains(inputText))
            {
                result.Add(playerName);
            }
        }

        return result;
    }

    private void ShowResult()
    {
        dropDown.ClearOptions();
        dropDown.AddOptions(searchResult);
        if (searchResult.Count != 0)
        {
            dropDown.Show();
        }
    }
}
