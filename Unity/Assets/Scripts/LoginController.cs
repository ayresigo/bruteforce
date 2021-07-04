using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoginController : MonoBehaviour
{
    public Text serverStatusText;
    public InputField loginInput;
    public InputField passwordInput;
    public Button submitButton;

    [Serializable]
    public class AccountData
    {
        public int account_id;
        public string login;
        public string creation_date;
    }

    public void callLogin()
    {
        StartCoroutine(login());
    }

    IEnumerator login()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", loginInput.text);
        form.AddField("pw", passwordInput.text);

        UnityWebRequest www = UnityWebRequest.Post("https://bruteforcegame.000webhostapp.com/index.php", form);
        yield return www.SendWebRequest();

        if (www.isDone)
        {
            Debug.Log("Enviando o formulário para o servidor.");
            string account_info_json;
            yield return account_info_json = www.downloadHandler.text;
            if (www.isDone)
            {
                if (account_info_json == "0")                
                    Debug.LogWarning("Usuário ou senha não existem!");
                
                else
                {
                    Debug.Log("Usuário conectado! Iniciando sessão...");
                    StartCoroutine(startSession(account_info_json));
                }
            }
            else
                Debug.LogError(www.error);
        }
        else
            Debug.LogError(www.error);
    }
    
    IEnumerator startSession(string account_info_json)
    {
        GameObject session = new GameObject();
        session.name = "Player Session";
        session.AddComponent<SessionManager>();
        Debug.Log("Response: " + account_info_json);
        AccountData account_info = JsonUtility.FromJson<AccountData>(account_info_json);
        session.GetComponent<SessionManager>().accountId = account_info.account_id;
        session.GetComponent<SessionManager>().login = account_info.login;
        session.GetComponent<SessionManager>().creationDate = account_info.creation_date;
        StartCoroutine(session.GetComponent<SessionManager>().getInventory(session.GetComponent<SessionManager>().accountId, session));
        yield return new WaitForSeconds(1f);
    }

    private void Update()
    {
        VerifyInputs();
    }

    public void VerifyInputs()
    {
        if (loginInput.text.Length >= 5 && passwordInput.text.Length >= 6)
        {
            submitButton.interactable = true;
        }
    }
}


