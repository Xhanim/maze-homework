using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour
{
    public AudioClip clip;
    public float volume = 1;
    public bool once;
    public bool ambient;
    // if set it will change to this one after the first clip is played when the collision happens again
    public AudioClip alternate;
    private bool playFirstClip = true;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Avatar")
        {
            AudioClip toPlay = alternate != null ? (playFirstClip ? clip: alternate) : clip;
            if (ambient)
            {
                AudioSource source = GameObject.FindWithTag("Ambient Audio").GetComponent<AudioSource>();
                source.clip = toPlay;
                source.volume = volume;
                source.Play();
            }
            else
            {
                other.gameObject.GetComponent<AudioController>().play(toPlay, volume);
                if (once) Destroy(gameObject);
            }
            playFirstClip = !playFirstClip;
        }
    }
}
