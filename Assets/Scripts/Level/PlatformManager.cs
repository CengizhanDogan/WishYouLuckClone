using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private GameObject gFX;
    [SerializeField] private GameObject ending;
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
        newScale.x = 12;
        newScale.z = 1f / (float)platformScale;
        ending.transform.localScale = newScale;
    }

    private void SetMatTexture()
    {
        platformMat = gFX.GetComponent<Renderer>().material;

        platformMat.mainTextureScale = new Vector2(1, platformScale / 2);
    }
}
