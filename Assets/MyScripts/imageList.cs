using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imageList : MonoBehaviour
{
    public Image currentImage;
    public List<Sprite> spriteList;
    int currentImageIndex;

    private void Awake()
    {
        currentImage = GetComponent<Image>();
        currentImage.sprite = spriteList[currentImageIndex];
    }
    public void SetCurrentImageIndex(int ind)
    {
        currentImageIndex = ind;
        currentImage.sprite = spriteList[currentImageIndex];
    }
}
