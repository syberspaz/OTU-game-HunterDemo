using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class winmenu : MonoBehaviour
{
   public void AnimateMenuOn()
    {
        gameObject.SetActive(true);
        LeanTween.scale(gameObject, Vector3.one, 1.0f).setEaseOutBounce();
    }
}
