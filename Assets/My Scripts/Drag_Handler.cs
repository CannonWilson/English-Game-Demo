using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag_Handler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject correctPanelMatch;
    public static GameObject itemLastDragged;
    public static bool firstScriptLoaded = true;
    static Dictionary<GameObject, GameObject> matches = new Dictionary<GameObject, GameObject>();
    static Dictionary<GameObject, GameObject> startingMatches = new Dictionary<GameObject, GameObject>();
    static Dictionary<GameObject, GameObject> correctMatches = new Dictionary<GameObject, GameObject>();
    GameObject[] panels;
    GameObject foundPanel;
    Vector3 startPosition;
    
    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition; // Set item position to the position of the mouse
    }

    public void OnEndDrag(PointerEventData eventData) {
        itemLastDragged = this.gameObject; // Set itemLastDraggedString
        ItemDropped();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start Called");
        startPosition = this.gameObject.transform.position;
        correctMatches[this.gameObject] = correctPanelMatch;
        panels = GameObject.FindGameObjectsWithTag("Panel"); // Get an array of all the panels in the response menu

        if (firstScriptLoaded) {
            Debug.Log("triggered");
            LoadDictionaries();
            firstScriptLoaded = false;
        }

        // foreach(KeyValuePair<GameObject, GameObject> dict in correctMatches) {
        //     Debug.Log("CorrectMatches. The button is " + dict.Key.name + ". The matching panel is " + dict.Value.name + ".");
        // }
    }

    void LoadDictionaries() {
        // populate the matches dictionary with the starting matches (which button starts where). This only needs to be done once.
        float maxDistance = 5f;
        GameObject[] wordButtons = GameObject.FindGameObjectsWithTag("Word Button");

        foreach(GameObject panel in panels) {
            foreach(GameObject button in wordButtons) {
                if (Vector3.Distance(panel.transform.position, button.transform.position) < maxDistance) {
                    matches[button] = panel;
                    startingMatches[button] = panel;
                    panel.GetComponent<Drop_Handler>().isOccupied = true;
                }
            }
        }
        // Debugging
        // foreach(KeyValuePair<GameObject, GameObject> dict in matches) {
        //     Debug.Log("Matches. The button is " + dict.Key.name + ". The matching panel is " + dict.Value.name + ".");
        // }
        // foreach(KeyValuePair<GameObject, GameObject> dict in startingMatches) {
        //     Debug.Log("StartingMatches. The button is " + dict.Key.name + ". The matching panel is " + dict.Value.name + ".");
        // }
    }

    void ItemDropped() {
        // loop through all the panels and see if any are within the right distance of the item being dropped
        float maxDistance = 100f; // accept drops within this many pixels of panels 
        foreach(GameObject panel in panels) {
            if (Vector3.Distance(Input.mousePosition, panel.transform.position) < maxDistance && !panel.GetComponent<Drop_Handler>().isOccupied) {
                foundPanel = panel;
            }
        }
        if (foundPanel == null) { // if a panel was not found
            transform.position = startPosition; // Set item position back to where it started from
        }
        else { // a panel was found
            transform.position = foundPanel.transform.position;
            matches[itemLastDragged].GetComponent<Drop_Handler>().isOccupied = false;
            matches[itemLastDragged] = foundPanel;
            foundPanel.GetComponent<Drop_Handler>().isOccupied = true;
        }
    }

    public void PlayButtonPressed() {
        bool allCorrect = true;
        foreach(GameObject button in matches.Keys) {
            if(matches[button] != correctMatches[button]) {
                allCorrect = false;
            }
        }
        // TODO:
        // Move all buttons back to original positions if wrong and update dicts
        if (allCorrect) {
            // reset firstScriptLoaded
            // reset dictionaries
            // Use method in First_tree to:
            // Play animation
            // Remove screens
            // Make player move again
            firstScriptLoaded = true;
            matches.Clear();
            startingMatches.Clear();
            correctMatches.Clear();
            
            First_Tree[] trees = Resources.FindObjectsOfTypeAll(typeof(First_Tree)) as First_Tree[];
            trees[0].GetComponent<First_Tree>().EnablePlayer();
        }
        else {
            // Move all buttons back to original positions
            // Update dicts
            foreach(GameObject match in startingMatches.Keys) {
                matches[match].GetComponent<Drop_Handler>().isOccupied = false;
                matches[match] = startingMatches[match];
                matches[match].GetComponent<Drop_Handler>().isOccupied = true;
                match.transform.position = matches[match].transform.position;
            }
        }

        // foreach(KeyValuePair<GameObject, GameObject> dict in matches) {
        //     Debug.Log("The button is " + dict.Key.name + ". The matching panel is " + dict.Value.name + ".");
        // }
    }


}
