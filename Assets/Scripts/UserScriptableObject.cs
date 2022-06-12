using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "User", menuName = "ScriptableObjects/User", order = 1)]
public class UserScriptableObject : ScriptableObject
{
    public bool isCurrentlyLoggedIn = false;

    [Header("User info")]
    public int id;
    public string username;
    public string emailAdress;
    [Space]
    public int regdate;
    public int regDay;
    public int regMonth;
    public int regYear;
    [Space]
    public int birthDate;
    public int birthDay;
    public int birthMonth;
    public int birthYear;
    [Space]
    public bool stayLoggedIn;
}
