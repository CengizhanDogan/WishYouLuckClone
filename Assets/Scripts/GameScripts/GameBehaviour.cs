using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof (Probability))]
public abstract class GameBehaviour : MonoBehaviour
{
    [Header("Game Components")]
    [SerializeField] private GameObject chipBundle;

    [HideInInspector] public Probability probability;
    [HideInInspector] public PlayerMovement playerMovement;

    [HideInInspector] public List<LensBehaviour> lensBehaviours = new List<LensBehaviour>();
    [HideInInspector] public LensBehaviour correctLens;
    [HideInInspector] public ProbabilityType myProbability;

    [HideInInspector] public int betValue;

    public void Awake()
    {
        probability = GetComponent<Probability>();
    }
    private void GetBetValue()
    {
        betValue = correctLens.bettedValue;
    }

    public abstract void MakeMovement();

    public void CheckLenses()
    {
        foreach (LensBehaviour lens in lensBehaviours)
        {
            lens.SetCorrentLens();
        }
    }
    public void SpawnEarning()
    {
        GetBetValue();

        transform.DOMoveY(-10, 1f).SetEase(Ease.InBack).OnComplete(() =>
        {
            if (betValue > 0)
            {
                ChipBundleManager chipsBundleClone = Instantiate(chipBundle, transform.position,
                    Quaternion.identity, transform).GetComponent<ChipBundleManager>();

                chipsBundleClone.totalValue = betValue * 2;
                chipsBundleClone.spawnValue = betValue;

                chipsBundleClone.transform.DOMoveY(0, 1f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    playerMovement.SetSpeed(true);
                });
            }
            else
            {
                playerMovement.SetSpeed(true);
            }
        });
    }
}
