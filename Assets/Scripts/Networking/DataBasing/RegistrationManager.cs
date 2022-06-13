using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class RegistrationManager : MonoBehaviour
{
    [Header("Register info fields")]
    [SerializeField] private TMP_InputField usernameText;
    [SerializeField] private TMP_InputField mailadressText;
    [SerializeField] private TMP_InputField passwordText;
    [Space]
    [SerializeField] private TMP_InputField birthDayText;
    [SerializeField] private TMP_InputField birthMonthText;
    [SerializeField] private TMP_InputField birthYearText;
    [Space]
    [SerializeField] private TextMeshProUGUI registerInfoText;

    [ContextMenu("Register User")]
    public void RegisterUser()
    {
        if (birthYearText.text != "")
        {
            StartCoroutine(RegisterUserEnumerator(
                usernameText.text,
                mailadressText.text,
                passwordText.text,
                MakeValidDateFromStrings(birthYearText.text, birthMonthText.text, birthDayText.text))
                );

        }
    }

    //From https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.Post.html
    private IEnumerator RegisterUserEnumerator(string username, string mailadress, string password, string birthdate)
    {
        WWWForm form = new WWWForm();

        form.AddField("name", username);
        form.AddField("mailadress", mailadress);
        form.AddField("password", password);
        form.AddField("birthdate", birthdate);

        UnityWebRequest www = UnityWebRequest.Post("https://studenthome.hku.nl/~joris.derks/networking/register_user.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            registerInfoText.text = www.downloadHandler.text;
        }
        yield return null;
    }

    private string MakeValidDateFromStrings(string yyyy, string mm, string dd)
    {
        if (mm.Length < 2 && mm.Length > 0)
        {
            mm = "0" + mm;
        }
        if (dd.Length < 2 && dd.Length > 0)
        {
            dd = "0" + dd;
        }
        string stringDate = yyyy + "-" + mm + "-" + dd;
        Debug.Log(stringDate);
        return stringDate;
    }

    //Returns "Hello world" from the server for debugging
    //private IEnumerator GetText()
    //{
    //    UnityWebRequest www = UnityWebRequest.Get("https://studenthome.hku.nl/~joris.derks/networking/helloworld.php");
    //    yield return www.SendWebRequest();

    //    if (www.result == UnityWebRequest.Result.ConnectionError)
    //    {
    //        Debug.Log(www.error);
    //    }
    //    else
    //    {
    //        // Show results as text
    //        Debug.Log(www.downloadHandler.text);

    //        // Or retrieve results as binary data
    //        byte[] results = www.downloadHandler.data;
    //    }
    //}
}
