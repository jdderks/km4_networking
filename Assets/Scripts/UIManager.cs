using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum UIPanels
{
    Scoreboards = 0,
    LoginRegister = 1,
    JoinGame = 2,
    HostGame = 3
}

public class UIManager : MonoBehaviour
{

    //[SerializeField] private Server server;
    //[SerializeField] private Client client;

    [SerializeField] private UIPanels selectedPanel;
    [SerializeField] private List<GameObject> panelObjects = default;

    [SerializeField] private TMP_InputField addressInput;




    //public void OnHostClick()
    //{
    //    server.Initialize(8007);
    //    client.Initialize("127.0.0.1", 8007);
    //}

    //public void OnConnectClick()
    //{
    //    client.Initialize(addressInput.text, 8007);
    //}



}
