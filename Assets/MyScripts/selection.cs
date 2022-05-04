using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class selection : MonoBehaviour
{
    PlayerInput playerControls;
    public GameObject rootOfAllInteractables;
    public GameObject prefabMagicIndicator;
    private GameObject magicIndicator;

    public playerInteractor m_interact;
    private GameObject selectedObjectParent;
    public GameObject environmentObject;
    private GameObject selectedObject;
    public Vector3 offset;
    private bool objectSelected = false;

    private GameObject prefabShootDisplay;
    public GameObject prefabShootDisplayUI;
    public Transform prefabShootConnection;

    private GameObject bottomRayTarget;
    public GameObject prefabBottomRayTarget;

    private AimingTargeting aimingTargeting;
    public bool isPressed = false;

    private InputActionAsset inputActions;
    private InputAction aimAxis;
    Vector2 distance;
    public List<GameObject> prefabShootObjectsList;
    int currentShootObjectIndex;

    public GameObject controlsMenu;

    void SetPrefabShootDisplay()
    {
        prefabShootDisplayUI.GetComponent<imageList>().SetCurrentImageIndex(currentShootObjectIndex);
    }
    private void Awake()
    {
        // get connection to aiming targeting instance
        aimingTargeting = Camera.main.GetComponentInChildren<AimingTargeting>();
        bottomRayTarget = Instantiate(prefabBottomRayTarget, gameObject.transform);
        bottomRayTarget.SetActive(false);
        magicIndicator = Instantiate(prefabMagicIndicator, gameObject.transform);
        magicIndicator.SetActive(false);
        currentShootObjectIndex = 0;
        SetPrefabShootDisplay();
        playerControls = GetComponent<PlayerInput>();
        inputActions = playerControls.actions;
        aimAxis = inputActions.FindAction("Player/ChangeAimDistance");
        Debug.Log(aimAxis);
        
        //StarterAssets.StarterAssetsInputs playerInputActions = new StarterAssets.StarterAssetsInputs();
        //playerInputActions.
    }

    void SelectObject(GameObject obj)
    {// not used yet
        selectedObject = obj;
        selectedObjectParent = obj.transform.parent.gameObject;
        selectedObject.transform.SetParent(Camera.main.transform);
        Rigidbody r = selectedObject.GetComponent<Rigidbody>();
        r.isKinematic = true;
        objectSelected = true;

        var pos = selectedObject.transform.position;
        pos.y = pos.y + 0.5f;
        selectedObject.transform.position = pos;

        // setup the prefab and particles for the downward indicator
        if (bottomRayTarget)
        {
            bottomRayTarget.transform.SetParent(selectedObject.transform);
        }
    }

    public void OnCharacterSelect()
    {
        gameObject.GetComponent<CharacterSelect>().ChangeCharacter();
    }

    public void OnMagnesis()
    {
        GameObject highlighted;
        
        if (isPressed)
        {
            // already pressed, this must be letting go of the button
            isPressed = false;
            if (objectSelected)
            {
              
                selectedObject.transform.parent = selectedObjectParent.transform;
                //selectedObject.GetComponent<Outline>().enabled = false;
                Rigidbody r = selectedObject.GetComponent<Rigidbody>();
                r.isKinematic = false;
                objectSelected = false;
                bottomRayTarget?.SetActive(false);
                magicIndicator?.SetActive(false);
            }
        }
        else
        {
            // not pressed yet, so this is the start of the button click
            if (aimingTargeting.GetHighlightedObject(out highlighted))
            {
                aimingTargeting.FixTargetToHighlightedObject();
                // now disconnect the rigid body
                selectedObject = highlighted;
                selectedObjectParent = highlighted.transform.parent.gameObject;
                selectedObject.transform.parent = Camera.main.transform;
                Rigidbody r = selectedObject.GetComponent<Rigidbody>();
                r.isKinematic = true;
                objectSelected = true;

                var pos = selectedObject.transform.position;
                pos.y = pos.y + 0.5f;
                selectedObject.transform.position = pos;

                // setup magic
                magicIndicator?.SetActive(true);
                bottomRayTarget?.SetActive(true);
            }
            isPressed = true;
        }
        
    }
    public void Update()
    {
        if (objectSelected)
        {
            GameObject target;
            if(aimingTargeting.GetAimTarget(out target))
            {
                selectedObject.transform.position = target.transform.position;
            }
            if (magicIndicator)
            {
                magicIndicator.transform.position = selectedObject.transform.position;
            }
            // setup bottom target
            if (bottomRayTarget)
            {
                RaycastHit hit;
                if (Physics.Raycast(selectedObject.transform.position, -Vector3.up, out hit))
                {
                    // bottomRayTarget.transform.SetParent(selectedObject.transform);

                    Transform tHit = hit.transform;
                    float dist= hit.distance;
                    var pos = hit.point;
                    pos.y += 0.02f;
                    bottomRayTarget.transform.position = pos;// selectedObject.transform.position - pos;
                    
                    
                }
            }

        }
    }
    public void OnSelect()
    {

        if (objectSelected)
        {
            Debug.Log("deselect");
            selectedObject.transform.parent = environmentObject.transform;
            selectedObject.GetComponent<Outline>().enabled = false;

            Rigidbody r = selectedObject.GetComponent<Rigidbody>();
            r.isKinematic = false;
            objectSelected = false;
        }
        else
        {
            if (m_interact.isInRange)
            {
                Debug.Log("select");

                // get the parent of the object for later
                //selectedObjectParent = m_interact.objectInRange.transform.parent.gameObject;
                selectedObject = m_interact.objectInRange.gameObject;
                // make object a child of our object
                selectedObject.transform.parent = gameObject.transform;
                Rigidbody r = selectedObject.GetComponent<Rigidbody>();
                r.isKinematic = true;
                var pos = selectedObject.transform.position;
                pos.y = pos.y + 0.5f;
                selectedObject.transform.position = pos;
                selectedObject.GetComponent<Outline>().enabled = true;
                objectSelected = true;
                
            }
        }
    }
    public void OnToggleShootObject()
    {
        currentShootObjectIndex = (currentShootObjectIndex + 1) % prefabShootObjectsList.Count;
        SetPrefabShootDisplay();

    }
    public void OnShootObject()
    {
        GameObject prefabToShoot = prefabShootObjectsList[currentShootObjectIndex];
        Vector3 pos = gameObject.transform.localPosition + offset;
        GameObject g = Instantiate(prefabToShoot, prefabShootConnection.position, prefabShootConnection.rotation, rootOfAllInteractables.transform);
        Rigidbody r = g.GetComponent<Rigidbody>();
        r.velocity = gameObject.transform.forward * 2;
        
    }
    public void OnChangeAimDistance()
    {

        float f = aimAxis.ReadValue<float>();
        aimingTargeting.ChangeAimDistance(f);

    }
    public void OnToggleControlsMenu()
    {
        controlsMenu?.SetActive(!controlsMenu.activeSelf);
    }
}
