using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_3 : MonoBehaviour
{
    public static int valid = 0;
    public GameObject pulseEffect;
    public GameObject buttonPulseEffect;
    public GameObject dialogue;
    public GameObject responseMenu;
    public GameObject player;

    void OnTriggerEnter() {
        if (valid == 4) { // This is inefficient, it should be if the player enters, rather than troubleshooting the amount of objects around a collider each time one's created.
            pulseEffect.SetActive(false);
            buttonPulseEffect.SetActive(false);
            DisablePlayer();
            dialogue.SetActive(true);
            responseMenu.SetActive(true);
            Button_Scripts.isComingFromNPC3 = true;
        }
        else {
            valid += 1;
        }
    }

    void DisablePlayer() {
        player.GetComponent<Animator>().enabled = false; // disable the script that allows the player to move 
        player.GetComponent<SimpleSampleCharacterControl>().m_moveSpeed = 0;
        player.GetComponent<SimpleSampleCharacterControl>().m_turnSpeed = 0;
    }
}
