using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public float timer = 0.0f;
    public bool end = false;
    public TextMeshProUGUI countdownText;  // Add a reference to the Text component
    public KillCount kc;
    public Astronaut player;

    // Start is called before the first frame update

    private void Awake()
    {
        kc = GameObject.Find("KillCount").GetComponent<KillCount>();
        kc.ResetKills();
        UpdateCountdownText();
    }
    void Start()
    {
        UpdateCountdownText();
        player = (Astronaut)FindAnyObjectByType(typeof(Astronaut));
    }

    // Update is called once per frame
    void Update()
    {
        if (!end)
        {
            timer += Time.deltaTime;

            if (timer >= 300.0f)
            {
                end = true;
                TimerEnded();
            }
            else
            {
                UpdateCountdownText(); // Update the countdown text each frame
                if (GlobalVariables.dead == 1){
                    Debug.Log("Player die");
                    Astronaut.CurHealth = 100;
                    GlobalVariables.dead = 0;
                    SceneManager.LoadScene("GameLost");
                }
                
        
            }
        }
    }

    // Method to update the countdown text
    void UpdateCountdownText()
    {
        float remainingTime = 300.0f - timer;
        int minutes = (int)remainingTime / 60;
        int seconds = (int)remainingTime % 60;
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Displaythe remaining time in mm:ss format
    }

    void TimerEnded()
    {
        
        GlobalVariables.timeEnd = 1;
        if (GlobalVariables.timeEnd == 1){
            // Debug.Log("Timer has ended!");
            SceneManager.LoadScene("GameWin");
        }
        // Add any additional actions you want to perform when the timer ends
    }
}
