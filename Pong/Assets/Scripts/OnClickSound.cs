using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickSound : MonoBehaviour
{

    // Add the sound clip to this script
    public AudioClip ClickSound;

    // Start is called before the first frame update
    void Start()
    {
        // Sets playonawake to false so the game doesnt accidentally play the sound
        GetComponent<AudioSource>().playOnAwake = false;
        // Sets the audio clip on the Audio Source component to the clip that was added to this script
        GetComponent<AudioSource>().clip = ClickSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        // Plays sound when function is called
        GetComponent<AudioSource>().Play();
    }
}
