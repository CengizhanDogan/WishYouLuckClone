using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Chip))]
public class ChipInteractions : MonoBehaviour
{
    private Chip myChip;

    private void Awake()
    {
        myChip = GetComponent<Chip>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!myChip.chipManager) return;

        #region Commneted Area
        /* This commented code can be used if not bundle but 1 chips is collected

        if (other.TryGetComponent(out Chip chip))
        {
            if (myChip.chipManager.chips.Contains(chip)) return;

            CollectChip(chip);

            myChip.playerScore.GetScore(chip.chipValue);
        }

        */
        #endregion

        if (other.TryGetComponent(out ChipBundleManager bundle))
        {
            if (bundle.collected) return;

            bundle.collected = true;

            Destroy(other);
            Destroy(bundle.textParent);

            foreach (var chip in bundle.bundleChips)
            {
                CollectChip(chip);
            }

            myChip.playerScore.GetScore(bundle.totalValue);
        }
    }
    private void CollectChip(Chip _chip)
    {
        myChip.chipManager.chips.Add(_chip);
        _chip.chipManager = myChip.chipManager;
        _chip.playerScore = myChip.playerScore;
        _chip.transform.DORotate(Vector3.right * 90, 0.2f);
        StartCoroutine(myChip.chipManager.ChipMovement(_chip, myChip.chipManager.chips.IndexOf(_chip)));
    }
}