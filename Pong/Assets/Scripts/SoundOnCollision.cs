using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour
{

    // Add the sound clip to this script
    public AudioClip sound;

    // Start is called before the first frame update
    void Start()
    {
        // Sets playonawake to false so the game doesnt accidentally play the sound
        GetComponent<AudioSource>().playOnAwake = false;
        // Sets the audio clip on the Audio Source component to the clip that was added to this script
        GetComponent<AudioSource>().clip = sound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Plays sound on collision
        GetComponent<AudioSource>().Play();
    }
}
