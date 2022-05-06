using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(ChipManager))]
[RequireComponent(typeof(PlayerScore))]
public class PlayerInteractions : MonoBehaviour
{
    private ChipManager chipManager;
    private PlayerScore playerScore;

    private void Start()
    {
        chipManager = GetComponent<ChipManager>();
        playerScore = GetComponent<PlayerScore>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Chip chip))
        {
            if (chipManager.chips.Contains(chip)) return;
            
            CollectChip(chip);

            playerScore.GetScore(chip.chipValue);
        }
    }

    private void CollectChip(Chip _chip)
    {
        chipManager.chips.Add(_chip);
        _chip.chipManager = chipManager;
        _chip.playerScore = playerScore;
        _chip.transform.DORotate(Vector3.right * 90, 0.2f);
        StartCoroutine(chipManager.ChipMovement(_chip, chipManager.chips.IndexOf(_chip)));
    }
}
