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
}
