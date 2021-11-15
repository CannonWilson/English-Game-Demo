using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top_Button : MonoBehaviour
{
    public GameObject wordMenu1;
    public GameObject wordMenu2;
    public static int menuToLoad = 1;

    public void Top_ButtonPressed() {
        if (menuToLoad == 1) {
            wordMenu1.SetActive(true);
        }
        if (menuToLoad == 2) {
            wordMenu2.SetActive(true);
        }
    }
}
