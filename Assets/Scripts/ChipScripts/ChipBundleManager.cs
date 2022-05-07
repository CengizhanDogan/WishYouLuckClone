using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChipBundleManager : MonoBehaviour
{
    [RangeExtension(2, 1000, 2)]
    public int totalValue;
    [HideInInspector] public int spawnValue;

    [SerializeField] private GameObject chipPrefab;
    [SerializeField] private GameObject placeHolder;
    [SerializeField] private Transform positionParent;
    [SerializeField] private List<Transform> chipPositions = new List<Transform>();

    [SerializeField] private TextMeshProUGUI textMesh;
    [HideInInspector] public GameObject textParent;
     public List<Chip> bundleChips = new List<Chip>();

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
        spawnValue = totalValue / 2;
    }
    void Start()
    {
        Destroy(placeHolder);
        SpawnChips();
        SetText();
    }

    private void SpawnChips()
    {
        for (int i = 0; i < spawnValue; i++)
        {
            Vector3 spawnPos = Vector3.zero;

            if (i < 3) spawnPos = chipPositions[0].position;
            else if (i > 2 && i < 8) spawnPos = chipPositions[1].position;
            else if (i > 7 && i < 13) spawnPos = chipPositions[2].position;
            else if (i > 12 && i < 16) spawnPos = chipPositions[0].position;
            else
            {
                spawnValue -= 16;
                positionParent.position += Vector3.up * 1.25f;
                SpawnChips();
                return;
            }

            spawnPos += (Vector3.up * SpawnOffset(i)) + (Vector3.right * RandomValue) + (Vector3.forward * RandomValue);

            GameObject clonedChip = Instantiate(chipPrefab, spawnPos, Quaternion.identity, transform);
            bundleChips.Add(clonedChip.GetComponent<Chip>());
        }
    }

    private void SetText()
    {
        textMesh.text = $"$ {totalValue}";
    }

}
