using System.Collections.Generic;
using UnityEngine;
public class Probability : MonoBehaviour
{
    public List<ProbabilityType> probablities = new List<ProbabilityType>();
}
public enum ProbabilityType
{
    Red,
    Black,
    Odd,
    Even
}
