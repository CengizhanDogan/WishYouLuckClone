using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : GameBehaviour
{
    [Header("Dice Components")]
    [SerializeField] private List<DiceRaySystem> dices = new List<DiceRaySystem>();
    public List<int> results = new List<int>();
    [SerializeField] private float forceValue = 1000;

    [SerializeField] private bool work;
    private bool stopped;

    private int stoppedCount = 0;
    private float RandomTorqueValue
    {
        get { return Random.Range(-10f, 10f); }
    }
    private void Update()
    {
        if (!work) return;
        MakeMovement();
        work = false;
    }
    public override void MakeMovement()
    {
        StartCoroutine(StartCheckingStop());
        foreach (var dice in dices)
        {
            Rigidbody diceRb = dice.GetComponent<Rigidbody>();

            diceRb.AddForce(Vector3.up * forceValue * Time.deltaTime, ForceMode.Impulse);

            Vector3 randomTorque = new Vector3(RandomTorqueValue, RandomTorqueValue / 5f, RandomTorqueValue);

            diceRb.AddTorque(randomTorque * forceValue * 2f * Time.deltaTime, ForceMode.Impulse);
        }
    }

    public void CheckResults()
    {
        if (results.Count == 2)
        {
            if ((results[0] + results[1]) % 2 == 0)
            {
                myProbability = ProbabilityType.Even;
            }
            else
            {
                myProbability = ProbabilityType.Odd;
            }

            CheckLenses();
            SpawnEarning();
        }
    }

    private IEnumerator StartCheckingStop()
    {
        yield return new WaitForSeconds(1f);

        foreach (var dice in dices)
        {
            Rigidbody diceRb = dice.GetComponent<Rigidbody>();
            StartCoroutine(CheckForStop(diceRb, dice));
        }
    }
    private IEnumerator CheckForStop(Rigidbody _diceRb, DiceRaySystem _dice)
    {
        while (!stopped)
        {
            if (_diceRb.velocity == Vector3.zero && !_dice.stoppedOnce)
            {
                stoppedCount++;
                _dice.stoppedOnce = true;
            }
            if (stoppedCount >= 2)
            {
                stopped = true;
                foreach (var dice in dices)
                {
                    dice.CastRay();
                }
            }
            yield return null;
        }
    }
}
