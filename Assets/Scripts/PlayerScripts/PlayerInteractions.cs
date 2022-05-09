using UnityEngine;
using DG.Tweening;
using System;
using Cinemachine;

[RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(ChipManager))]
[RequireComponent(typeof(PlayerScore), typeof(AnimationManager), typeof(PlayerMovement))]
public class PlayerInteractions : MonoBehaviour
{
    private ChipManager chipManager;
    private PlayerScore playerScore;
    private AnimationManager animationManager;
    private PlayerMovement playerMovement;

    [SerializeField] private GameObject endCam;
    [SerializeField] private CinemachineVirtualCamera vCam;
    private void Awake()
    {
        chipManager = GetComponent<ChipManager>();
        playerScore = GetComponent<PlayerScore>();
        animationManager = GetComponent<AnimationManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        #region Commneted Area
        if (other.TryGetComponent(out Chip chip))
        {
            if (chipManager.chips.Contains(chip) || chip.bundleChip || chip.thrown) return;

            CollectChip(chip);

            playerScore.GetScore(chip.chipValue);
        }
        #endregion

        if (other.TryGetComponent(out ChipBundleManager bundle))
        {
            if (bundle.collected) return;

            bundle.collected = true;

            Destroy(other);
            Destroy(bundle.textParent);

            foreach (var bundleChip in bundle.bundleChips)
            {
                CollectChip(bundleChip);
            }

            playerScore.GetScore(bundle.totalValue);
        }

        if (other.TryGetComponent(out EndingBehaviour end))
        {
            SetScoreUI();
            SetCam();
            StartCoroutine(end.EnableButton());
            SetPosition();
            SetAnims();
        }

        if (other.TryGetComponent(out LensBehaviour lens))
        {
            playerMovement.SetSpeed(false);
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

    #region Ending Hethods
    private void SetScoreUI()
    {
        Transform playerCanvas = playerScore.textMesh.transform.parent.parent;
        playerCanvas.SetParent(transform.parent);
        Vector3 canvasPos = playerCanvas.position;
        canvasPos.x = 0;
        playerCanvas.position = canvasPos;
    }

    private void SetCam()
    {
        vCam.transform.parent = null;
        vCam.Follow = null;
        Vector3 camPos = endCam.transform.position;
        camPos.x = 0;

        endCam.transform.position = camPos;
        endCam.transform.parent = transform.parent;
        endCam.SetActive(true);
    }

    private void SetPosition()
    {
        playerMovement.forwardSpeed = 0;
        playerMovement.swerveSpeed = 0;

        transform.DORotate(Vector3.up * 180, 0.25f);

        Vector3 centerPos = transform.position;
        centerPos.x = 0;

        transform.DOMove(centerPos, 0.25f);
    }
    private void SetAnims()
    {
        animationManager.SetRunAnimation(false);
        animationManager.EnableWinAnim();
    }
    #endregion
}
