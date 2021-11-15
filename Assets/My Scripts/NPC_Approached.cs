using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Approached : MonoBehaviour
{
    // This script will be attached to trigger objects that will cause certain behavior when the character approaches.
    public GameObject NPC;
    public GameObject emote; // before being approached, the NPC is usually emoting (ex: exclamation or happy)
    public GameObject dialogue;
    public GameObject responseMenu;
    public GameObject pulseEffect;
    public GameObject player;
    public GameObject treeColliderBox;
    bool isFirstTime = true;

    public static float previousSpeed;
    public static float previousRotation;

    void OnTriggerEnter() {
        // TODO:
        // enable dialogue above the trigger/character it's around
        // make it so the player cannot move
        // bring up response menu

        // The trigger is being entered by the NPC in the environment, so use isFirstTime to control for this
        if (!isFirstTime) {
            emote.SetActive(false);
            pulseEffect.SetActive(false);
            NPC.transform.LookAt(player.transform); // face the NPC towards the player when the player enters the NPC's collider
            player.transform.LookAt(NPC.transform); // face the player towards the NPC too
            dialogue.SetActive(true);
            responseMenu.SetActive(true);
            SlowAndStopPlayer();
        }
        else {
            isFirstTime = false;
        }
    }

    void OnDisable() { // Called when this collider object is disable by Button_Script 
        // Let the player move again
        player.GetComponent<Animator>().enabled = true; // enable the script that allows the player to move 
        player.GetComponent<SimpleSampleCharacterControl>().m_moveSpeed = previousSpeed;
        player.GetComponent<SimpleSampleCharacterControl>().m_turnSpeed = previousRotation;

        pulseEffect.SetActive(true);
        pulseEffect.transform.position = new Vector3(180.214f, 50.92802f, 235.752f); // This is the position of the tree the player should go towards (divided by 10)
        pulseEffect.transform.eulerAngles = new Vector3(97.659f, 21.04201f, -46.327f); // This is the rotation of the tree
        treeColliderBox.SetActive(true);
    }

    void SlowAndStopPlayer() {
        player.GetComponent<Animator>().enabled = false; // disable the script that allows the player to move 

        previousSpeed = player.GetComponent<SimpleSampleCharacterControl>().m_moveSpeed;
        previousRotation = player.GetComponent<SimpleSampleCharacterControl>().m_turnSpeed;

        player.GetComponent<SimpleSampleCharacterControl>().m_moveSpeed = 0;
        player.GetComponent<SimpleSampleCharacterControl>().m_turnSpeed = 0;
    }
}
