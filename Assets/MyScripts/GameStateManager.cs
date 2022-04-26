using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using TMPro;
public class GameStateManager : MonoBehaviour
{
    // this is the event that is called when everything is collected.

    [SerializeField] public UnityEvent OnCompleteEvent;
    [SerializeField] public TextMeshProUGUI textfield;

    // what to collect
    [SerializeField] List<Collectible> gatherables;

    // copy of list when it starts of all the things
    List<Collectible> collectiblesRemaining;

    private void OnEnable()
    {
        collectiblesRemaining = new List<Collectible>(gatherables);
        textfield.text = collectiblesRemaining.Count.ToString();
        // register event handlers
        foreach (var collectible in collectiblesRemaining)
        {
            collectible.OnPickup += HandlePickup;
        }
    }
    void HandlePickup(Collectible collectible)
    {
        // we have been picked up, what to do?
        // remove ourselves from the list
        collectiblesRemaining.Remove(collectible);
        textfield.text = collectiblesRemaining.Count.ToString();
        if(collectiblesRemaining.Count == 0)
        {
            // Game is complete! You picked them all up
            OnCompleteEvent?.Invoke();
        }
    }

    [ContextMenu("AutoFill Collectibles")]
    void AutoFillCollectibles()
    {
        gatherables = FindObjectsOfType<Collectible>().ToList();
    }
}
