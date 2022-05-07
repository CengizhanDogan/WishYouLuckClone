using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Roulette : GameBehaviour
{
    [SerializeField] private Transform rouletteModel;
    [SerializeField] private Transform ball;

    public override void MakeMovement()
    {
        int rotateValue = 0;

        if (myProbability == ProbabilityType.Red) rotateValue = 1753;
        if (myProbability == ProbabilityType.Black) rotateValue = 1820;

        rouletteModel.DORotate(Vector3.up * rotateValue, 2.5f).SetRelative(true).SetEase(Ease.InOutExpo);
        ball.DORotate(Vector3.up * -1800, 2.5f).SetRelative(true).SetEase(Ease.InOutExpo).OnComplete(() =>
        {
            ball.rotation = Quaternion.Euler(Vector3.zero);
            Vector3 ballPos = new Vector3(-1, -2.6f, 4.75f);
            ball.DOLocalMove(ballPos, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                transform.DOMoveY(-10, 1f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    SpawnEarning();
                });
            });
        });
    }
}
