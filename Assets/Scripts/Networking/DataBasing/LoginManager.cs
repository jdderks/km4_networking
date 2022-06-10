using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoginManager : MonoBehaviour
{
    private string _username = "naamVanuitUnity";
    private string _mailadress = "mailVanuitUnity";
    private string _password = "passwordVanuitUnity";
    private string _birthdate = "01-01-01";

    void Start()
    {
        //StartCoroutine(RegisterUserEnumerator());
    }

    [ContextMenu("Register User")]
    void RegisterUser()
    {
        StartCoroutine(RegisterUserEnumerator());
    }


    //Returns "Hello world" from the server for debugging
    private IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://studenthome.hku.nl/~joris.derks/networking/helloworld.php");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }

    //From https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.Post.html
    private IEnumerator RegisterUserEnumerator()
    {
        WWWForm form = new WWWForm();

        form.AddField("name",_username);
        form.AddField("mailadress", _mailadress);
        form.AddField("password", _password);

        UnityWebRequest www = UnityWebRequest.Post("https://studenthome.hku.nl/~joris.derks/networking/register_user.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            
        }

        yield return null;
    }
}
