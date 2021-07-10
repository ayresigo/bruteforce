using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommonWindowController : MonoBehaviour
{
    [ReadOnly] public GameObject session;

    [Header("Status Bar")]
    public GameObject stats;
    public TMP_Text energyText;
    public TMP_Text goldText, diamondText;

   /* private void Awake()
    {
        SessionManager sessionComponent = session.GetComponent<SessionManager>();
        energyText.text = sessionComponent.energy.ToString()+"/20";
        goldText.text = sessionComponent.gold.ToString();
        diamondText.text = sessionComponent.diamond.ToString();
    }*/
}
