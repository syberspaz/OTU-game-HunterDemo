using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimingTargeting : MonoBehaviour
{
    public GameObject gameObjectToUse;
    public bool displayObject;
    public float distanceFromCamera;
    public float scrollSpeed;
    [Range(0.0f, 10.0f)]
    public float scrollRangeMin;
    [Range(0.0f, 30.0f)]
    public float scrollRangeMax;

    private GameObject highlightedObject;
    // Start is called before the first frame update
    void Awake()
    {
        highlightedObject = null;
        //gameObjectToUse?.SetActive(false);
    }
    public bool GetAimTarget(out GameObject g)
    {
        g = gameObjectToUse;
        if (g) return true;
        else return false;
    }
    public bool GetHighlightedObject(out GameObject g)
    {
        g= highlightedObject;
        if (g) return true;
        else return false;
    }
    void Highlight(GameObject obj, bool state)
    {
        if (obj.tag == "Interactable")
        {
            // check distance from camera
            float dist = Vector3.Distance(Camera.main.transform.position, obj.transform.position);
            //if (dist > distanceFromCamera) return;
            var outline = obj.GetComponent<Outline>();
            if (outline)
            {
                outline.enabled = state;
            }
        }
    }
    public void FixTargetToHighlightedObject()
    {
        if(highlightedObject)
            distanceFromCamera = Vector3.Distance(Camera.main.transform.position, highlightedObject.transform.position);
    }
    
    private void FixedUpdate()
    {

        RaycastHit hit;
        Vector3 direction = Camera.main.transform.forward;
        if (displayObject)
        {
            
            distanceFromCamera += Mouse.current.scroll.ReadValue().y * scrollSpeed;
            distanceFromCamera = Mathf.Clamp(distanceFromCamera, scrollRangeMin, scrollRangeMax);
            gameObjectToUse.transform.position = Camera.main.transform.position + direction * distanceFromCamera;
            gameObjectToUse.SetActive(true);
        }
        

        if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
        {
            Transform tHit = hit.transform;
            GameObject objectHit = tHit.transform.gameObject;
            if (objectHit.tag != "Interactable") return;
            Debug.Log("tag:" + objectHit.tag);
            float dist = Vector3.Distance(Camera.main.transform.position, objectHit.transform.position);
            if (highlightedObject)
            {
                // we had previously selected an object, is this hit the same one?
                if(objectHit != highlightedObject )
                {
                    // different one
                    // deslect previous one and set this one up
                    Highlight(highlightedObject, false);
                    // select new one
                    if (dist <= distanceFromCamera)
                    {
                        highlightedObject = tHit.transform.gameObject;
                        Highlight(highlightedObject, true);
                    }
                }
                
            }
            else
            {
                if (dist <= distanceFromCamera)
                {
                    // nothing highlighted at the moment and we hit an object, we should do something
                    highlightedObject = tHit.transform.gameObject;
                    Highlight(highlightedObject, true);
                }
            }
        }
        else
        {
            // no hit, deselect object if we had previously selected it
            if (highlightedObject)
            {
                Highlight(highlightedObject, false);
                highlightedObject = null;
            }
        }

    }
   
}
