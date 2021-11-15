using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Bridge : MonoBehaviour
{
    //TODO:
    // There is a lot of repeated functionality between the colliders. It would be best to combine these two scripts

    public GameObject pulseEffect;
    public GameObject buttonPulseEffect;
    public GameObject player;
    public GameObject wordMenu;
    static float playerSpeed;
    static float playerRotation;
    public static bool isValid = false;

    void OnTriggerEnter() {
        if (isValid) {
            pulseEffect.SetActive(false);
            buttonPulseEffect.SetActive(true);
            Top_Button.menuToLoad = 2;
            DisablePlayer();
        }
        else {
            isValid = true;
        }
    }

    void DisablePlayer() {
        player.GetComponent<Animator>().enabled = false; // disable the script that allows the player to move 

        playerSpeed = player.GetComponent<SimpleSampleCharacterControl>().m_moveSpeed;
        playerRotation = player.GetComponent<SimpleSampleCharacterControl>().m_turnSpeed;

        player.GetComponent<SimpleSampleCharacterControl>().m_moveSpeed = 0;
        player.GetComponent<SimpleSampleCharacterControl>().m_turnSpeed = 0;
    }
}
