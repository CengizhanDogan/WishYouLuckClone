using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChipBundleManager : MonoBehaviour
{
    [RangeExtension(2, 30, 2)]
    public int totalValue;

    [SerializeField] private GameObject chipPrefab;
    [SerializeField] private GameObject placeHolder;
    [SerializeField] private List<Transform> chipPositions = new List<Transform>();

    [SerializeField] private TextMeshProUGUI textMesh;
    [HideInInspector] public GameObject textParent;
    [HideInInspector] public List<Chip> bundleChips = new List<Chip>();

    [HideInInspector] public bool collected;

    private float RandomValue
    {
        get
        {
            return Random.Range(0, 0.1f);
        }
    }

    private float SpawnOffset(int _spawnCount)
    {
        float upValue = 0.25f;

        if (_spawnCount < 3)
        {
            return _spawnCount * upValue;
        }
        else if (_spawnCount > 2 && _spawnCount < 8)
        {
            return (_spawnCount - 3) * upValue;
        }
        else if (_spawnCount > 7 && _spawnCount < 13)
        {
            return (_spawnCount - 8) * upValue;
        }
        else
        {
            return (_spawnCount - 10) * upValue;
        }
    }

    private void Awake()
    {
        textParent = textMesh.transform.parent.gameObject;
    }
    void Start()
    {
        Destroy(placeHolder);
        SpawnChips();
        SetText();
    }

    private void SpawnChips()
    {
        for (int i = 0; i < totalValue / 2; i++)
        {
            Vector3 spawnPos = Vector3.zero;
            GameObject clonedChip = null;

            if (i < 3) spawnPos = chipPositions[0].position;
            else if (i > 2 && i < 8) spawnPos = chipPositions[1].position;
            else if (i > 7 && i < 13) spawnPos = chipPositions[2].position;
            else spawnPos = chipPositions[0].position;

            spawnPos += (Vector3.up * SpawnOffset(i)) + (Vector3.right * RandomValue) + (Vector3.forward * RandomValue);

            clonedChip = Instantiate(chipPrefab, spawnPos, Quaternion.identity, transform);
            bundleChips.Add(clonedChip.GetComponent<Chip>());
        }
    }

    private void SetText()
    {
        textMesh.text = $"$ {totalValue}";
    }

}
