using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipManager : MonoBehaviour
{
    public List<Chip> chips = new List<Chip>();

    [SerializeField] private float followValue;
    [SerializeField] private float frontValue;
    [SerializeField] private float upValue;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public IEnumerator ChipMovement(Chip _chip, int _listOrder)
    {
        while (chips.Contains(_chip))
        {
            Vector3 followPos = Vector3.zero;

            if (_listOrder > 0)
            {
                followPos = chips[_listOrder - 1].chipRb.transform.position;
                followPos.z = rb.transform.position.z + frontValue * 2 + (frontValue * _listOrder);
                followPos.x = Mathf.Lerp(_chip.chipRb.transform.position.x, chips[_listOrder - 1].chipRb.transform.position.x, followValue * Time.deltaTime);
            }
            else
            {
                followPos = rb.transform.position;
                followPos.z += frontValue * 2;
                followPos.y += upValue;
                followPos.x = Mathf.Lerp(_chip.chipRb.transform.position.x, rb.transform.position.x, followValue * Time.deltaTime);
            }

            _chip.chipRb.transform.position = followPos;

            yield return null;
        }
    }
}
