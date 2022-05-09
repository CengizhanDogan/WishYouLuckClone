using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRaySystem : MonoBehaviour
{
    [SerializeField] private Dice dice;
    [SerializeField] private LayerMask layer;

    private List<DiceSides> diceSides = new List<DiceSides>();

    public bool stoppedOnce;

    private void Start()
    {
        GetDiceSides();
    }

    private void GetDiceSides()
    {
        DiceSides[] diceSidesArray = GetComponentsInChildren<DiceSides>();

        foreach (DiceSides item in diceSidesArray)
        {
            diceSides.Add(item);
        }
    }

    public void CastRay()
    {
        foreach (DiceSides diceSide in diceSides)
        {
            if (Physics.Raycast(diceSide.transform.position, diceSide.transform.forward * 5f, out var hit, 5f, layer))
            {
                dice.results.Add(diceSide.sideValue);
                dice.CheckResults();
            }
        }
    }
}
