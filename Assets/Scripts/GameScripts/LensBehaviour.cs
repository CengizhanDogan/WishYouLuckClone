using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LensBehaviour : MonoBehaviour
{
    [SerializeField] private GameBehaviour myGame;
    [SerializeField] private ProbabilityType lensProbability;

    public int bettedValue;
    [SerializeField] private TextMeshPro textMesh;

    private void Awake()
    {
        myGame.lensBehaviours.Add(this);
    }
    public void SetCorrentLens()
    {
        if (lensProbability == myGame.myProbability)
        {
            myGame.correctLens = this;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Chip chip))
        {
            if (!chip.chipManager.chips.Contains(chip)) return;

            chip.chipManager.chips.Remove(chip);
            chip.playerScore.LoseScore(2);
            Destroy(chip.gameObject);
            bettedValue += 2;
            SetText();
        }
        if (other.TryGetComponent(out PlayerMovement player))
        {
            myGame.MakeMovement();
            myGame.playerMovement = player;
        }
    }
    private void SetText()
    {
        textMesh.text = $"$ {bettedValue}";
    }
}
