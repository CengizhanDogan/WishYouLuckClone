using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private GameObject gFX;
    private Material platformMat;

    [RangeExtension(100, 10000, 10)]
    [SerializeField] private int platformScale;
    void Update()
    {
        SetScale();
        SetMatTexture();
    }

    private void SetScale()
    {
        Vector3 newScale = transform.localScale;
        newScale.z = platformScale;
        transform.localScale = newScale;
    }

    private void SetMatTexture()
    {
        platformMat = gFX.GetComponent<Renderer>().material;

        platformMat.mainTextureScale = new Vector2(1, platformScale / 2);

    }
}
