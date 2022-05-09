using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectorAnimate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(gameObject, gameObject.transform.localScale*0.75f, 2.0f).setLoopPingPong();
        //LeanTween.rotateAround(gameObject, Vector3.forward, 360, 2.5f).setLoopClamp();
        LeanTween.rotateAround(gameObject, new Vector3(1,1,0), 360, 5.0f).setLoopClamp();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
