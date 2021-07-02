using System.Collections;
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
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.LogError("Não foi possivel enviar o POST para o servidor...");
            Debug.LogError(www.error);
        }
        else
        {
            Debug.Log("Enviando o formulário para o servidor.");
            string account_info = www.downloadHandler.text;
            string[] account_infoArr;
            if (account_info == "0")
            {
                Debug.LogWarning("Usuário ou senha não existem!");
            } else
            {
                Debug.Log("Usuário conectado! Resgatando dados do banco...");
                account_infoArr = account_info.Split("*"[0]);

                Debug.Log("Iniciando sessão...");
                GameObject session = new GameObject();
                session.name = "Player Session";
                session.AddComponent<SessionManager>();

                /* account_infoArr =    #
                 * 1,                   0
                 * login,               1
                 * creation_date        2
                 */

                for (int i = 0; i < account_infoArr.Length; i++)
                {
                    switch (i)
                    {
                        case 1:
                            session.GetComponent<SessionManager>().login = account_infoArr[i];
                            Debug.Log("Login: " + account_infoArr[i]);
                            break;
                        case 2:
                            session.GetComponent<SessionManager>().creation_date = account_infoArr[i];
                            Debug.Log("Creation Date: " + account_infoArr[i]);
                            break;
                        default:
                            break;
                    }
                }
            }            
        }
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
