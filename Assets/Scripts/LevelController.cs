using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    [Space(10)]
    public ExperiencePickup pickup;
    [Space(10)]
    public int currentExperience;
    public List<int> expLevels;
    public int currentLevel = 1;
    public int levelCount = 100;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;

        if (currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }

        UIController.instance.UpdateExperience(currentExperience, expLevels[currentLevel], currentLevel);
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(pickup, position, Quaternion.identity).experienValue = expValue;
    }

    void LevelUp()
    {
        currentExperience -= expLevels[currentLevel];

        currentLevel++;

        if (currentLevel >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;
        }

        PlayerController.instance.acticeWeapon.LevelUp();
    }
}
