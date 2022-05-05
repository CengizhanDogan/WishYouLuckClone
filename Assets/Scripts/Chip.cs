using UnityEngine;

public class Chip : MonoBehaviour
{
    [RangeExtension(2, 100, 2)]
    public int chipValue;

    [HideInInspector] public Rigidbody chipRb;
    [HideInInspector] public ChipManager chipManager;
    [HideInInspector] public PlayerScore playerScore;

    void Start()
    {
        SetRigidBody();
        SetColor();
    }

    public void SetRigidBody()
    {
        chipRb = GetComponent<Rigidbody>();
    }

    private void SetColor()
    {
        Transform[] chips = GetComponentsInChildren<Transform>();

        foreach (Transform chip in chips)
        {
            if (chip != transform)
            {
                chip.gameObject.SetActive(false);
            }
        }

        int randomValue = Random.Range(1, chips.Length);

        chips[randomValue].gameObject.SetActive(true);
    }
}
