using UnityEngine;
using DG.Tweening;

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
        #region Commneted Area
        /* This commented code can be used if not bundle but 1 chips is collected

        if (other.TryGetComponent(out Chip chip))
        {
            if (chipManager.chips.Contains(chip)) return;

            CollectChip(chip);

            playerScore.GetScore(chip.chipValue);
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

            playerScore.GetScore(bundle.totalValue);
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
