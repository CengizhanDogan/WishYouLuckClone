using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    [SerializeField] private float throwPower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Chip chip))
        {
            if (chip.chipManager.chips.Contains(chip))
            {
                chip.thrown = true;
                Destroy(other);

                CheckIfLastMember(chip, chip.chipManager.chips.IndexOf(chip));
            }
        }
    }

    private void CheckIfLastMember(Chip _chip, int chipIndex)
    {
        if (_chip.chipManager.chips[_chip.chipManager.chips.Count - 1] != _chip)
        {
            for (int i = _chip.chipManager.chips.Count - 1; i >= chipIndex; i--)
            {
                Destroy(_chip.chipManager.chips[i].GetComponent<Collider>());

                _chip.playerScore.LoseScore(2);

                Destroy(_chip.chipManager.chips[i].gameObject, 2f);

                ThrowChip(_chip.chipManager.chips[i]);
            }
        }
        else
        {
            _chip.playerScore.LoseScore(2);

            Destroy(_chip.gameObject, 2f);

            ThrowChip(_chip);
        }
    }

    private void ThrowChip(Chip _chip)
    {
        _chip.chipManager.chips.Remove(_chip);
        _chip.chipRb.constraints = RigidbodyConstraints.None;

        Vector3 rightThrow = new Vector3(Random.Range(3f, 5f), Random.Range(5f, 10f), 0);
        Vector3 leftThrow = new Vector3(Random.Range(-5f, -3f), Random.Range(5f, 10f), 0);

        Vector3[] directions = new Vector3[2];
        directions[0] = rightThrow;
        directions[1] = leftThrow;

        Vector3 rotateDirection = new Vector3(Random.Range(2, 5), Random.Range(2, 5), Random.Range(2, 5));

        _chip.chipRb.AddForce(directions[Random.Range(0, directions.Length)] * throwPower * Time.deltaTime, ForceMode.Impulse);
        _chip.chipRb.AddTorque(rotateDirection * throwPower * 2 * Time.deltaTime, ForceMode.Impulse);
    }
}
