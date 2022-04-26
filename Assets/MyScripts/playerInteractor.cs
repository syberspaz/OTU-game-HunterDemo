using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerInteractor : MonoBehaviour
{
    public bool isInRange = false;
    public GameObject objectInRange;
    public Transform aimingTransform;
    public GameObject aimingTargeting;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            Debug.Log("collision ENTERED::");
            Debug.Log(collision.gameObject.tag);
            isInRange = true;
            objectInRange = collision.gameObject;
            var outline = objectInRange.GetComponent<Outline>();
            if (outline)
            {
                outline.enabled = true;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            Debug.Log("collision EXIT::");
            Debug.Log(collision.gameObject.tag);
            isInRange = false;
            objectInRange = null;
            var outline = objectInRange.GetComponent<Outline>();
            if (outline)
            {
                outline.enabled = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            //Debug.Log("collision ENTERED::");
            //Debug.Log(collision.gameObject.tag);
            isInRange = true;
            objectInRange = other.gameObject;
            var outline = objectInRange.GetComponent<Outline>();
            if (outline)
            {
                outline.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            //Debug.Log("collision ENTERED::");
            //Debug.Log(collision.gameObject.tag);
            isInRange = true;
            objectInRange = other.gameObject;
            var outline = objectInRange.GetComponent<Outline>();
            if (outline)
            {
                outline.enabled = true;
            }
        }
    }
    private void OnChangeAimDistance()
    {
        Debug.Log("change aim distance");

    }

}
