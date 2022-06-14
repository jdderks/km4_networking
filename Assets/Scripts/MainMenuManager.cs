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

public class MainMenuManager : MonoBehaviour
{

    //[SerializeField] private Server server;
    //[SerializeField] private Client client;

    [SerializeField] private UIPanels selectedPanel;
    [SerializeField] private List<GameObject> panelObjects = default;


    public void ChangeMenu(int menuNumber)
    {
        for (int i = 0; i < panelObjects.Count; i++)
        {
            if (i == menuNumber)
            {
                panelObjects[i].SetActive(true);
            }
            else
            {
                panelObjects[i].SetActive(false);
            }
        }
    }




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
