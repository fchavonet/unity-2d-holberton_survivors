using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    public static LevelManagement instance;
    private bool gameActive;
    public float timer;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameActive = true;
    }

    void Update()
    {
        if(gameActive == true)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }
    }

}
