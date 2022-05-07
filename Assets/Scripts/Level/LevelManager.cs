using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public LevelHolder levelHolder;

    private GameObject currentLevel;
    void Start()
    {
        LoadNextLevel(false);
    }

    public void LoadNextLevel(bool button)
    {
        if (button) levelHolder.levelCount++;

        if (currentLevel)
        {
            Destroy(currentLevel);
        }
        if (levelHolder.levelCount - 1 <= levelHolder.levels.Count - 1)
        {
            currentLevel = Instantiate(levelHolder.levels[levelHolder.levelCount - 1]);
        }
        else
        {
            currentLevel = Instantiate(levelHolder.levels[Random.Range(0, levelHolder.levels.Count)]);
        }
    }
}
