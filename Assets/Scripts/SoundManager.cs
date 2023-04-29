using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
   
    private bool alphaPlayed = false;
    private bool betaPlayed = false;
    private bool brainMolePlayed = false;
    private bool minotaurPlayed = false;

    private bool allPlayed = false;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        OnAudioClipPlayed();
    }

    public void AlphaPlay(AudioClip clip)
    {
        // Check if the clip was played
        if (alphaPlayed)
        {
            return;
        }

        // Play the sound effect
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

        // // Set the checking bool to true
        alphaPlayed = true;
        CheckAllBool();
    }

    public void BetaPlay(AudioClip clip)
    {
        // Check if the clip was played
        if (betaPlayed)
        {
            return;
        }

        // Play the sound effect
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

        // // Set the checking bool to true
        betaPlayed = true;
        CheckAllBool();
    }

    public void BrainMolePlay(AudioClip clip)
    {
        // Check if the clip was played
        if (brainMolePlayed)
        {
            return;
        }

        // Play the sound effect
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

        // // Set the checking bool to true
        brainMolePlayed = true;
        CheckAllBool();
    }

    public void MinotaurPlay(AudioClip clip)
    {
        // Check if the clip was played
        if (minotaurPlayed)
        {
            return;
        }

        // Play the sound effect
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

        // // Set the checking bool to true
        minotaurPlayed = true;
        CheckAllBool();
    }

    private void CheckAllBool()
    {
        allPlayed = (alphaPlayed && betaPlayed && brainMolePlayed && minotaurPlayed);
        //Debug.Log(allPlayed);
    }

    public void OnAudioClipPlayed()
    {
        if (allPlayed)
        {
            // reset the bool of sounds playing
            alphaPlayed = false;
            betaPlayed = false;
            brainMolePlayed = false;
            minotaurPlayed = false;

            allPlayed = false;;
        }
    }
}
