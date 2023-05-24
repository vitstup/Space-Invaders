using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource blasterSource;

    public bool muted {  get; private set; }
    
    public void PlayBlasterSound()
    {
        blasterSource.Play();
    }

    public void Mute()
    {
        muted = !muted;
        musicSource.mute = muted;
        blasterSource.mute = muted;
    }
}