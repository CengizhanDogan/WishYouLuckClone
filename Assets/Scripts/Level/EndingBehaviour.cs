using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndingBehaviour : MonoBehaviour
{
    [SerializeField] private Transform nextButton;
    [SerializeField] private List<Transform> positions = new List<Transform>();

    private float yPlus = 0.25f;
    private int column;
    private int line = -1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Chip chip))
        {
            if (!chip.chipManager) return;

            chip.thrown = true;

            Destroy(other);
            PlaceTheChip(chip.transform);
            chip.chipManager.chips.Remove(chip);
        }
    }

    private void PlaceTheChip(Transform _chip)
    {
        line++;

        Vector3 pos = positions[line].position;
        pos.y += (column - 2) * yPlus;

        _chip.DOMove(pos, 0.5f);
        _chip.DORotate(Vector3.zero, 0.2f);

        if (line % 4 == 0 && line != 0)
        {
            line = -1;
            column++;
        }
    }

    public IEnumerator EnableButton()
    {
        yield return new WaitForSeconds(3f);

        nextButton.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
    }

    public void NextButton()
    {
        LevelManager.Instance.LoadNextLevel(true);
    }
}
