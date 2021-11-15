using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Scripts : MonoBehaviour
{
    public GameObject dialogue;
    static int dialogueIndex = 1; // Keeps track of what the character is saying and has said, beginning at 1
    int dialogues;
    public GameObject NPCQuestionMarks;
    public GameObject responseMenu;
    public GameObject dialogue3;
    public GameObject responseMenu3;
    public GameObject FadeOutBlackScreen;
    public static bool isComingFromNPC3 = false;

    void OnEnable() {
        dialogues = dialogue.transform.childCount; // Get the number of different dialogue children objects
        HandleSpeech();
    }

    public void ResponseButtonPressed() {
        if (EventSystem.current.currentSelectedGameObject.name == "Correct") { //If the currently pressed button's name is "Correct"
            if (isComingFromNPC3) {
                FadeOutBlackScreen.SetActive(true);
            }
            Debug.Log(dialogueIndex);
            dialogueIndex += 1;
            Debug.Log(dialogueIndex);
            HandleSpeech();
        }
        else {
            if (isComingFromNPC3) {
                dialogue3.transform.localScale = Vector3.zero;
                responseMenu3.transform.localScale = Vector3.zero;
            }
            // Remove speech bubble and current dialogue
            dialogue.transform.localScale = Vector3.zero;
            // Show question marks around character
            NPCQuestionMarks.SetActive(true);
            // Disable response menu. This is shrunk like this because setting it to inactive will not work. A Coroutine can only run on an active object.
            responseMenu.transform.localScale = Vector3.zero;
            // Show speech bubble and same dialogue after a wait time.
            StartCoroutine(RepopulateResponseScreen());
        }
    }

    IEnumerator RepopulateResponseScreen() {
        yield return new WaitForSeconds(1.5f);
        NPCQuestionMarks.SetActive(false);
        if (isComingFromNPC3) {
            dialogue3.transform.localScale = Vector3.one;
            responseMenu3.transform.localScale = Vector3.one;
        }
        else {
            dialogue.transform.localScale = Vector3.one;
            responseMenu.transform.localScale = Vector3.one;
        }
    }

    void DepopulateResponseScreen() {
        if (isComingFromNPC3) {
            FadeOutBlackScreen.SetActive(true);
        }
        else {
            dialogue.transform.localScale = Vector3.zero;
            responseMenu.transform.localScale = Vector3.zero;
            // TODO:
            // Add logic so that the correct GameObject is found and disabled when this script is applied to more scenarios
            GameObject mycollider = GameObject.Find("NPC 1 Collider");
            mycollider.SetActive(false);
        }
    }

    void HandleSpeech() {
        // go through the dialogue options and response options and show the correct ones depending on where the player is in the required response flow
        // using tranform.localScale to show/hide because SetAtive only works once at runtime. Ugh.

        if (dialogueIndex == dialogues) { //if the dialogueIndex has been increased past the valid number of dialogues, aka the dialogue has ended
            DepopulateResponseScreen();
        }

        for(int i=1; i<dialogues; i++) { // loop over all children excluding the first, which is the speech bubble image
            if (i == dialogueIndex) {
                // Show the dialogue and reponse options corresponding to the current dialogue shown by the dialogueIndex
                dialogue.transform.GetChild(i).transform.localScale = Vector3.one;
                responseMenu.transform.GetChild(i).transform.localScale = Vector3.one;
            }
            else {
                // Hide the dialogue and response options if they do not correspond to the current dialogueIndex
                dialogue.transform.GetChild(i).transform.localScale = Vector3.zero;
                responseMenu.transform.GetChild(i).transform.localScale = Vector3.zero;
            }
        }
    }
}
