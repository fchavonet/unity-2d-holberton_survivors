using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    public AudioSource[] bgm;

    private void Awake()
    {
        instance = this;
    }

    public void StopBGM(int bgmToStop)
    {
        bgm[bgmToStop].Stop();
    }
}
