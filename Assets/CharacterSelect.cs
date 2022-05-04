using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System.Linq;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    // what to collect
    public GameObject[] mixamoCharacters;

    //[SerializeField] List<GameObject> taggedPrefabs;

    private Animator theAnimator;
    private GameObject theCurrentCharacter;
    private int currentCharacter;

    void SetCurrentCharacter()
    {
        foreach (GameObject obj in mixamoCharacters)
         {
             obj.SetActive(false);
         }
        theCurrentCharacter = mixamoCharacters[currentCharacter];
        theCurrentCharacter.SetActive(true);
    }
    void SetAnimation()
    {
        theAnimator.avatar = theCurrentCharacter.GetComponent<Animator>().avatar;
        theAnimator.Rebind();
    }
    public void ChangeCharacter()
    {
        int length = mixamoCharacters.Length;
        currentCharacter++;
        if (currentCharacter >= length) currentCharacter = 0;
        SetCurrentCharacter();
        SetAnimation();
    }
    
    public void Start()
    {
        theAnimator = GetComponent<Animator>();
        SetCurrentCharacter();
        // set up the avatar
        SetAnimation();
        
    }


    [ContextMenu("AutoFill Active Characters")]
    void AutoFillActiveCharacters()
    {
        mixamoCharacters = GameObject.FindGameObjectsWithTag("Character");
    }
}
