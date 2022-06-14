using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    IS_MAIN_MENU = 0,
    IS_PLAYING = 1,
    IS_PAUSED = 2,
    IS_GAMEOVER = 3
}


public class GameManager : MonoBehaviour
{

    [SerializeField] private GameState currentGameState = GameState.IS_MAIN_MENU;
    private List<Player> players = new List<Player>();
    private MainMenuManager uiManager;

    [SerializeField] private List<GameObject> UIStateObjects = new List<GameObject>();

    [SerializeField] private Grid tileGrid;

    private int playerTurn;

    [SerializeField] private GameObject playerPrefab;

    public void StartGame()
    {
        SetActiveUIStateObjects(1);

        CreateGrid();
        CreatePlayer(new Vector2Int(5, 5), 5);
        AssignCamera();

        playerTurn = Random.Range(0, players.Count);
        Debug.Log(playerTurn + " is the turn");

        tileGrid.CurrentPlayerPosition = new Vector2Int(5, 5);
        currentGameState = GameState.IS_PLAYING;
    }

    void Update()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (i == playerTurn && tileGrid.SelectedTile != null)
            {
                players[i].MoveTo(tileGrid.SelectedTile);
                tileGrid.CurrentPlayerPosition = players[i].CurrentPosition;
            }
        }
    }
    /// <summary>
    /// Creates a player and sets the required data
    /// </summary>
    /// <param name="tileSpawnPosition"></param>
    /// <param name="movesLeft"></param>
    private void CreatePlayer(Vector2Int tileSpawnPosition, int movesLeft)
    {
        GameObject playerGO = Instantiate(playerPrefab, tileGrid.GetTileFromPosition(tileSpawnPosition).gameObject.transform.position, playerPrefab.transform.rotation);
        //playerPosition = new Vector2Int(tile.x, tile.z);
        Player player = playerGO.GetComponent<Player>();
        player.CurrentPosition = new Vector2Int(tileGrid.GetTileFromPosition(tileSpawnPosition).x, tileGrid.GetTileFromPosition(tileSpawnPosition).z);
        player.MovesLeft = movesLeft;
        player.MoveTo(tileSpawnPosition);
        players.Add(player);
        player.AssignedNumber = players.Count;
    }
    /// <summary>
    /// Helper method that executes the grid creation in the tilegrid class
    /// </summary>
    private void CreateGrid()
    {
        tileGrid.Generate();
    }

    private void SetActiveUIStateObjects(int activeNmbr)
    {
        for (int i = 0; i < UIStateObjects.Count; i++)
        {
            if (i == activeNmbr)
            {
                UIStateObjects[i].SetActive(true);
            }
            else
            {
                UIStateObjects[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// Assigns the acrive camera to the player
    /// </summary>
    private void AssignCamera()
    {
        tileGrid.Camera = players[playerTurn].GetComponentInChildren<Camera>();
    }

}
