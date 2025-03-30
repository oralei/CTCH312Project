using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------Audio Clip---------")]
    public AudioClip background;
    public AudioClip microwaveBeep;
    public AudioClip microwaveHeat;
    public AudioClip door;
    public AudioClip jumpscare;
    public AudioClip ghostJumpscare;
    public AudioClip walking;
    public AudioClip telephone;
    public AudioClip doorbell;
    public AudioClip stringsHit;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
