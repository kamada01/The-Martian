using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [Header("Games to Load")]
    public string _newGame;
    private string GameToLoad;
    [SerializeField] private GameObject NoSaveGame = null;

    public void NewGameYes()
    {
        SceneManager.LoadScene(_newGame);
    }

    public void LoadGameYes()
    {
        if (PlayerPrefs.HasKey("SavedGame"))
        {
            GameToLoad = PlayerPrefs.GetString("SavedGame");
            SceneManager.LoadScene(GameToLoad);
        }
        else
        {
            NoSaveGame.SetActive(true);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
