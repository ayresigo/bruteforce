using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameStart : MonoBehaviour
{
    public float gameVersion;
    IEnumerator getVersion ()
    {
        WWWForm versionForm = new WWWForm();
        versionForm.AddField("version", gameVersion.ToString());
        UnityWebRequest www = UnityWebRequest.Post("https://bruteforcegame.000webhostapp.com/version.php", versionForm);
        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
        www.Dispose();
    }

    private void Start()
    {
        StartCoroutine(getVersion());
    }
}
