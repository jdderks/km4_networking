using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [Header("Login info fields")]
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [Space]
    [SerializeField] private TextMeshProUGUI loginInfoText;
    [Space]
    [SerializeField] private UserScriptableObject currentLoggedInUserObject;
    [Space]
    [SerializeField] private Toggle staySignedInToggle;


    [ContextMenu("Login User")]
    public void LoginUser()
    {
        StartCoroutine(LoginUserEnumerator(
            usernameInputField.text, 
            passwordInputField.text)
            );
    }

    private IEnumerator LoginUserEnumerator(string username, string password)
    {
        WWWForm form = new WWWForm();

        form.AddField("name", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("https://studentdav.hku.nl/~joris.derks/networking/user_login.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            loginInfoText.text = www.error;
        }
        else
        {
            if (www.downloadHandler.text != "Fail")
            {
                loginInfoText.text = "Succesfully logged in.";

                string json = www.downloadHandler.text;
                UserData data = JsonUtility.FromJson<UserData>(json);
                currentLoggedInUserObject.id = data.id;
                currentLoggedInUserObject.username = data.name;
                currentLoggedInUserObject.emailAdress = data.mailadress;
                currentLoggedInUserObject.regdate = data.reg_date;
                currentLoggedInUserObject.birthDate = data.birth_date;

                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime birthDate = epoch.AddSeconds(data.birth_date);

                currentLoggedInUserObject.birthYear = birthDate.Year;
                currentLoggedInUserObject.birthMonth = birthDate.Month;
                currentLoggedInUserObject.birthDay  = birthDate.Day;

                DateTime regDate = epoch.AddSeconds(data.reg_date);
                currentLoggedInUserObject.regYear = regDate.Year;
                currentLoggedInUserObject.regMonth = regDate.Month;
                currentLoggedInUserObject.regDay = regDate.Day;

                currentLoggedInUserObject.stayLoggedIn = staySignedInToggle.isOn;
                currentLoggedInUserObject.isCurrentlyLoggedIn = true;
            } 
            else
            {
                loginInfoText.text = "Wrong username/password or user does not exist";
            }
            //loginInfoText.text = www.downloadHandler.text;
        }


        yield return null;
    }

    [ContextMenu("Logout")]
    public void Logout()
    {
        currentLoggedInUserObject.id = default; 
        currentLoggedInUserObject.username = default;
        currentLoggedInUserObject.emailAdress = default;
        currentLoggedInUserObject.regdate = default;
        currentLoggedInUserObject.birthDate = default;
        currentLoggedInUserObject.birthYear = default;
        currentLoggedInUserObject.birthMonth = default;
        currentLoggedInUserObject.birthDay = default; 
        currentLoggedInUserObject.regYear = default;
        currentLoggedInUserObject.regMonth = default;
        currentLoggedInUserObject.regDay = default;
        currentLoggedInUserObject.stayLoggedIn = false;
        staySignedInToggle.isOn = false;
        currentLoggedInUserObject.isCurrentlyLoggedIn = false;
    }
}


[Serializable]
public class UserData
{
    public int id;
    public string name;
    public string mailadress;
    public int reg_date;
    public int birth_date;
}