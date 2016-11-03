using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }else
        {
            audioSource.Stop();
        }*/
	}

    public void play(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
    }
}
