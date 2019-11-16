using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeLaunchSound : MonoBehaviour
{
    // Add the sound clip to this script
    public AudioClip LaunchSound;

    // Start is called before the first frame update
    void Start()
    {
        // Sets playonawake to false so the game doesnt accidentally play the sound
        GetComponent<AudioSource>().playOnAwake = false;
        // Sets the audio clip on the Audio Source component to the clip that was added to this script
        GetComponent<AudioSource>().clip = LaunchSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Play Sound (D)");
            GetComponent<AudioSource>().Play();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Play Sound (Arrow)");
            GetComponent<AudioSource>().Play();
        }
    }
}
