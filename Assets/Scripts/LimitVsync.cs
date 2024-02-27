using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitVsync : MonoBehaviour
{
    void Start()
    {
        // Sync the frame rate to the screen's refresh rate
        QualitySettings.vSyncCount = 1;
    }
}