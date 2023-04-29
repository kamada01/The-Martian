using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector director;

    void Start()
    {
        // Start playing the timeline
        director.Play();
    }

    void Update()
    {
        // Check if the timeline has finished playing
        if (director.state != PlayState.Playing)
        {
            // Load the next scene after the timeline finishes
            SceneManager.LoadScene("SampleScene_1");
        }

        // Check if the player has pressed the spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Load the next scene if the spacebar is pressed
            SceneManager.LoadScene("SampleScene_1");
        }

    }
}
