using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    [Header("Games to Load")]
    private string GameToLoad;
    [SerializeField] private GameObject NoSaveGame = null;
    public Astronaut player;

    public void NewGameYes()
    {
        SceneManager.LoadScene("SampleScene_1");
        player = (Astronaut)FindAnyObjectByType(typeof(Astronaut));
    }

    public void LoadCutScene()
    {
        /*SceneManager.LoadScene("SampleScene_1");
        player = (Astronaut)FindAnyObjectByType(typeof(Astronaut));
        Debug.Log(Astronaut.CurHealth);*/
        SceneManager.LoadScene("OpeningScene");
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
