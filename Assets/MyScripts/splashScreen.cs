using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splashScreen : MonoBehaviour
{
    public GameObject nextCanvas;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 1.0f, 2.0f).setOnComplete(WaitAnim);
    }
    void FadeInComplete()
    {
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0.0f, 2.0f).setOnComplete(AnimationComplete);
    }
    void WaitAnim() {
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 2.0f, 2.0f).setOnComplete(FadeInComplete);
    }
    void AnimationComplete()
    {
        LeanTween.alphaCanvas(nextCanvas.GetComponent<CanvasGroup>(), 1.0f, 2.0f);
        gameObject.SetActive(false);
    }
}
