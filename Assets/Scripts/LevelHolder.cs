using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelHolder", menuName = "LevelHolder")]
public class LevelHolder : ScriptableObject
{
    public int levelCount;
    public List<GameObject> levels = new List<GameObject>();
}
