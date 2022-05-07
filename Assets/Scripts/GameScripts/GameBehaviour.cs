using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class GameBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject chipBundle;
    private Probability probability;
    public PlayerMovement playerMovement;
    public LensBehaviour correctLens;
    public ProbabilityType myProbability;

    public int betValue;

    private void Awake()
    {
        probability = GetComponent<Probability>();

        myProbability = probability.probablities[Random.Range(0, probability.probablities.Count)];
    }
    public void GetBetValue()
    {
        Debug.Log(correctLens);
        betValue = correctLens.bettedValue;
    }

    public abstract void MakeMovement();

    public void SpawnEarning()
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
    }
}
