using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "User", menuName = "ScriptableObjects/User", order = 1)]
public class UserScriptableObject : ScriptableObject
{
    [Header("asdasdasd")]
    public int id;
    public string username;
    public string emailAdress;
    public string birthDate;
    public int birthDay;
    public int birthMonth;
    public int birthYear;
}
