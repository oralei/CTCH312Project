using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Source---------")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    [Header("---------Audio Clip---------")]
    public AudioClip background;
    public AudioClip creditMusic;
    public AudioClip microwaveBeep;
    public AudioClip microwaveHeat;
    public AudioClip door;
    public AudioClip vase;
    public AudioClip ghostJumpscare;
    public AudioClip walking;
    public AudioClip telephone;
    public AudioClip doorbell;
    public AudioClip stringsHit;
    public AudioClip notify;
    public AudioClip lockedDoor;
    public AudioClip buttonPress;
    public AudioClip eatSound;
    public AudioClip pickUpPhone;
    public AudioClip meow;

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
