using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUI : MonoBehaviour
{
    private LevelManager levelManager;

    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject tapToPlay;
    void Start()
    {
        levelManager = LevelManager.Instance;

        SetLevelText();
    }

    private void SetLevelText()
    {
        textMesh.text = $"Level {levelManager.levelHolder.levelCount}";
    }

    void Update()
    {
        if (!tapToPlay) return;

        if (Input.GetMouseButtonDown(0))
        {
            Destroy(tapToPlay);
        }
    }
}
