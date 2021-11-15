using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class First_Tree : MonoBehaviour
{
    public GameObject pulseEffect;
    public GameObject buttonPulseEffect;
    public GameObject player;
    public GameObject topButton;
    public GameObject wordMenu;
    public GameObject wordMenu2;
    public GameObject treeInteractionColl;
    public GameObject bridgeInteractionColl;
    public GameObject bridge;

    static float playerSpeed;
    static float playerRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnablePlayer() {
        wordMenu.SetActive(false);
        wordMenu2.SetActive(false);
        buttonPulseEffect.SetActive(false);
        pulseEffect.SetActive(true);
        pulseEffect.transform.position = new Vector3(198.51f, 50.01f, 242.87f); // Move pulse effect to in front of the water and set its rotation (position divided by 10)
        pulseEffect.transform.eulerAngles = new Vector3(90.55499f, 47.694f, -19.88098f);

        if (Build_Bridge.isValid) {
            pulseEffect.transform.position = new Vector3(238.18f, 49.87f, 257.41f);
            pulseEffect.transform.eulerAngles = new Vector3(94.11798f, 269.893f, 201.146f);
            bridge.SetActive(true);
            bridgeInteractionColl.SetActive(true);
        }

        player.GetComponent<Animator>().enabled = true; // enable the script that allows the player to move 

        player.GetComponent<SimpleSampleCharacterControl>().m_moveSpeed = playerSpeed;
        player.GetComponent<SimpleSampleCharacterControl>().m_turnSpeed = playerRotation;
        treeInteractionColl.SetActive(false);
        bridgeInteractionColl.SetActive(true);
    }

    void OnTriggerEnter() {
        pulseEffect.SetActive(false);
        buttonPulseEffect.SetActive(true);
        topButton.SetActive(true);
        DisablePlayer();
    }

    void DisablePlayer() {
        player.GetComponent<Animator>().enabled = false; // disable the script that allows the player to move 

        playerSpeed = player.GetComponent<SimpleSampleCharacterControl>().m_moveSpeed;
        playerRotation = player.GetComponent<SimpleSampleCharacterControl>().m_turnSpeed;

        player.GetComponent<SimpleSampleCharacterControl>().m_moveSpeed = 0;
        player.GetComponent<SimpleSampleCharacterControl>().m_turnSpeed = 0;
    }
}
