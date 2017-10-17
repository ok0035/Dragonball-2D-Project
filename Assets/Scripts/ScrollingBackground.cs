using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour
{
    public const float scrollSpeed = 0.2f;
    private Material thisMaterial;

    void Start()
    {
        thisMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        Vector2 newOffset = thisMaterial.mainTextureOffset;
        newOffset.Set(newOffset.x + (scrollSpeed * Time.deltaTime), 0);
        thisMaterial.mainTextureOffset = newOffset;
    }
}