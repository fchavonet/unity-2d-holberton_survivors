using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    public AudioSource[] soundEffects;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySFX(int sfxToPlay)
    {
        soundEffects[sfxToPlay].Stop();
        soundEffects[sfxToPlay].Play();
    }

    public void StopSFX(int sfxToPlay)
    {
        soundEffects[sfxToPlay].Stop();
    }

    public void PlaySFXPitched(int sfxToPlay)
    {
        soundEffects[sfxToPlay].pitch = Random.Range(0.8f, 1.2f);

        PlaySFX(sfxToPlay);
    }
}
