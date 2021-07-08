using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class LoginController : MonoBehaviour
{
    [Header("Common")]
    public GameObject loginPopup, lobby, common;
    public GameObject signUpScreen, loginScreen;
    public TMP_Text windowTitle;

    [Header("Login")]
    public TMP_InputField login_username;
    public TMP_InputField login_password;
    public Toggle login_rememberMe;
    public GameObject login_submitButton, login_fakeSubmitButton;
    public TMP_Text login_feedback;
    
    [Header("Sign Up")]
    public TMP_InputField signup_username;
    public TMP_InputField signup_password, signup_c_password, signup_email;
    public GameObject signup_submitButton, signup_fakeSubmitButton;
    public TMP_Text signup_feedback;

    [Serializable]
    public class AccountData
    {
        public int id;
        public string username;
        public string creation_date;
        public int privilege_level;
        public int level;
        public int energy;
        public int gold;
        public int diamond;
        public int fk_char_id1, fk_char_id2, fk_char_id3;
    }
    
    public void callSignUp()
    {
        StartCoroutine(signUp());
    }

    IEnumerator signUp()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", signup_username.text);
        form.AddField("pw", signup_password.text);
        form.AddField("c_pw", signup_c_password.text);
        form.AddField("email", signup_email.text);
        

        UnityWebRequest www = UnityWebRequest.Post("https://bruteforcegame.000webhostapp.com/register.php", form);
        yield return www.SendWebRequest();

        switch(www.downloadHandler.text)
        {
            case "success":
                signUpScreen.SetActive(false);
                loginScreen.SetActive(true);
                login_feedback.color = Color.green;
                login_feedback.text = "Usuario cadastrado com sucesso!";
                break;
            default:
                signup_feedback.color = Color.red;
                signup_feedback.text = www.downloadHandler.text;
                break;
        }
    }
    public void callLogin()
    {
        StartCoroutine(login(login_username.text, login_password.text));
    }

    IEnumerator login(string login, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("pw", password);

        UnityWebRequest www = UnityWebRequest.Post("https://bruteforcegame.000webhostapp.com/index.php", form);
        yield return www.SendWebRequest();

        if (www.isDone)
        {
            login_feedback.color = Color.white;
            login_feedback.text = "Enviando dados para o servidor...";
            string account_info_json;
            yield return account_info_json = www.downloadHandler.text;
            if (www.isDone)
            {   
                switch(account_info_json)
                {
                    case "0":
                        login_feedback.color = Color.red;
                        login_feedback.text = "Usuario ou senha invalidos!";
                        break;
                    case null:
                        login_feedback.color = Color.red;
                        login_feedback.text = "Algo aconteceu. Tente novamente mais tarde.";
                        break;
                    default:
                        login_feedback.color = Color.white;
                        login_feedback.text = "Iniciando sessao...";
                        StartCoroutine(startSession(account_info_json));
                        break;
                }
            }
            else
            {
                login_feedback.color = Color.red;
                login_feedback.text = www.error;
            }
                
        }
        else
        {
            login_feedback.color = Color.red;
            login_feedback.text = www.error;
        }
    }
    
    IEnumerator startSession(string account_info_json)
    {
        GameObject session = new GameObject();
        session.name = "PlayerSession";
        session.AddComponent<SessionManager>();
        SessionManager sessionComponent = session.GetComponent<SessionManager>();
        Debug.Log("Response: " + account_info_json);
        AccountData account_info = JsonUtility.FromJson<AccountData>(account_info_json);
        sessionComponent.accountId = account_info.id;
        sessionComponent.login = account_info.username;
        sessionComponent.creationDate = account_info.creation_date;
        sessionComponent.privilegeLevel = account_info.privilege_level;
        sessionComponent.level = account_info.level;
        sessionComponent.energy = account_info.energy;
        sessionComponent.diamond = account_info.gold;
        sessionComponent.loginPopup = loginPopup;
        sessionComponent.lobby = lobby;
        sessionComponent.common = common;
        lobby.GetComponent<LobbyController>().session = session;
        StartCoroutine(sessionComponent.getInventory(sessionComponent.accountId, session));
        yield return new WaitForSeconds(1f);
    }

    private void Update()
    {
        VerifyInputs();

        if (loginScreen.active)
            windowTitle.text = "LOGIN";
        else
            windowTitle.text = "SIGN UP";
    }

    public void VerifyInputs()
    {
        bool signup_usernameInputisOk = false;
        bool signup_passwordInputisOk = false;
        bool signup_c_passwordInputisOk = false;
        bool signup_emailInputisOk = false;

        if (login_username.text.Length >= 3 && login_password.text.Length >= 6)
        {
            login_submitButton.SetActive(true);
            login_fakeSubmitButton.SetActive(false);
        }
        else
        {
            login_submitButton.SetActive(false);
            login_fakeSubmitButton.SetActive(true);
        }

        if (signup_username.text.Length >= 3)
        {
            signup_usernameInputisOk = true;
            signup_username.textComponent.color = Color.black; 
        }
        else
        {
            signup_usernameInputisOk = false;
            signup_username.textComponent.color = Color.red;
        }            

        if (signup_password.text.Length >= 6)
        {
            signup_passwordInputisOk = true;
            signup_password.textComponent.color = Color.black;            
        }
        else
        {
            signup_passwordInputisOk = false;
            signup_password.textComponent.color = Color.red;
        }

        if (signup_c_password.text == signup_password.text)
        {
            signup_c_passwordInputisOk = true;
            signup_c_password.textComponent.color = Color.black;
        } 
        else
        {
            signup_c_passwordInputisOk = false;
            signup_c_password.textComponent.color = Color.red;
        }

        if (signup_email.text.Contains("@") && signup_email.text.Contains(".com"))
        {
            signup_emailInputisOk = true;
            signup_email.textComponent.color = Color.black;
        }
        else
        {
            signup_emailInputisOk = false;
            signup_email.textComponent.color = Color.red;
        }

        if (signup_usernameInputisOk && signup_passwordInputisOk && signup_c_passwordInputisOk && signup_emailInputisOk)
        {
            signup_submitButton.SetActive(true);
            signup_fakeSubmitButton.SetActive(false);
        } else
        {
            signup_submitButton.SetActive(false);
            signup_fakeSubmitButton.SetActive(true);
        }
    }
}


