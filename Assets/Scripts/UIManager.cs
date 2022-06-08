using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Server server;
    [SerializeField] private Client client;

    [SerializeField] private TMP_InputField addressInput;

    public void OnHostClick()
    {
        server.Initialize(8007);
        client.Initialize("127.0.0.1", 8007);
    }

    public void OnConnectClick()
    {
        client.Initialize(addressInput.text, 8007);
    }



}
