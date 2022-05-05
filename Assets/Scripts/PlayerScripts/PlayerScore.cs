using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerScore : MonoBehaviour
{
    private int score;

    [SerializeField] TextMeshProUGUI textMesh;

    private void Start()
    {
        SetText();
    }
    public void GetScore(int _earnedScore)
    {
        score += _earnedScore;

        SetText();
    }

    private void SetText()
    {
        textMesh.text = $"$ {score}";
    }
}
