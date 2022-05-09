using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Collectible : MonoBehaviour
{
    public bool isOn;
    public UnityAction<Collectible> OnPickup;
    public GameObject energyPrefab;
    private Material energyMat;
    float life;
    public void Start()
    {
        energyPrefab = Instantiate(energyPrefab, gameObject.transform, false);
        energyPrefab.SetActive(true);
        MeshRenderer m = energyPrefab.GetComponentInChildren<MeshRenderer>();
       // Debug.Log(m);
        energyMat = m.material;
        //Debug.Log(energyMat);
        energyMat.SetFloat("LIFE", 0.0f);
        energyPrefab.transform.SetParent(gameObject.transform);
        energyPrefab.transform.localScale = Vector3.zero;
        life = 0.0f;
    }
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
            life = 1.0f;
            energyMat.SetFloat("LIFE", 1.0f);
            LeanTween.value(life, 0.0f, 1.2f).setEaseOutQuart().setOnUpdate(updateMaterial);
            LeanTween.scale(energyPrefab, Vector3.one*5.0f, 0.5f);
        }
        //yield return new WaitUntil(()=>!LeanTween.isTweening(gameObject));
        
    }
    private void updateMaterial(float val)
    {
        energyMat.SetFloat("LIFE", val);
        //Debug.Log(val);
    }
    private void AnimationComplete()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.5f)
               .setEaseOutBounce();
        OnPickup?.Invoke(this);
        gameObject.SetActive(false);
    }
}
