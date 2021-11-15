using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop_Handler : MonoBehaviour
{
    [Tooltip("Used to determine if words are placed correctly. Leave at 0 if panel is in word menu, assign a value starting at 1 if panel is in phrase menu.")]
    [SerializeField] int panelIndex;

    [Tooltip("Shows if a word is on the panel.")]
    [SerializeField] public bool isOccupied;
}
