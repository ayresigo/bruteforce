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
    public GameObject playerSession;

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
