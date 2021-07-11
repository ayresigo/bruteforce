using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyController : MonoBehaviour
{
    [Header("Session")]
    public GameObject session;
    public GameObject commonWindow;
    public GameObject modularCharacter;

    [Header("Screen Elements")]
    [Header("User Info")]
    public TMP_Text userName;
    public GameObject expBar;
    public TMP_Text userLevel, expBarText;

    [Header("Lower Menu")]
    public GameObject marketplaceButton;
    public GameObject inventoryButton;
    public GameObject battleButton;

    private void Awake()
    {
        Debug.Log("Awakening Lobby Controller...");
        commonWindow.SetActive(true);
        SessionManager sessionComponent = session.GetComponent<SessionManager>();
        commonWindow.GetComponent<CommonWindowController>().session = session;
        userName.text = sessionComponent.login;
        userLevel.text = sessionComponent.level.ToString();
    }

    private void OnBecameVisible()
    {
        commonWindow.SetActive(false);
    }
}
