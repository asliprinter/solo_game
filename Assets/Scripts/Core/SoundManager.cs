using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource souce;

    private void Awake()
    {
        instance = this;
        souce = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        souce.PlayOneShot(clip);
    }
}
