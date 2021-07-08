using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCard : MonoBehaviour
{
    [Header("Frames")]
    public Sprite defaultFrame;
    public Sprite dimFrame, commonFrame, rareFrame, epicFrame, legendaryFrame;
    public GameObject commonLabelPrefab, rareLabelPrefab, epicLabelPrefab, legendaryLabelPrefab;
    public GameObject addIcon, addText;

    [Header("Screen Elements")]
    public Sprite frame;
    public Image thisFrame;
    public GameObject label, stars, characterName;
}
