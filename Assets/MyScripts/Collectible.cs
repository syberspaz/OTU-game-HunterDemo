using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Collectible : MonoBehaviour
{
    public bool isOn;
    public UnityAction<Collectible> OnPickup;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        // animate
        if (isOn)
        {
            LeanTween.moveY(gameObject, gameObject.transform.position.y + 2.0f, 1.0f)
                .setEaseOutBounce()
                .setOnComplete(AnimationComplete);
            //LeanTween.scale(gameObject, gameObject.transform.localScale * 1.5f, 0.5f)
              //  .setEaseOutBounce()
                

        }
        //yield return new WaitUntil(()=>!LeanTween.isTweening(gameObject));
        
    }
    private void AnimationComplete()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.5f)
               .setEaseOutBounce();
        OnPickup?.Invoke(this);
        gameObject.SetActive(false);
    }
}
