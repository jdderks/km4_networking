using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    IS_SETUP,
    IS_PLAYING,
    IS_PAUSED,
    IS_GAMEOVER
}


public class GameManager : MonoBehaviour
{

    [SerializeField] private GameState currentGameState = GameState.IS_SETUP;
    private List<Player> players = new List<Player>();
    private UIManager uiManager;

    [SerializeField] private Grid tileGrid;

    private int playerTurn;

    [SerializeField] private GameObject playerPrefab;

    void Start()
    {
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

    private void AssignCamera()
    {
        tileGrid.Camera = players[playerTurn].GetComponentInChildren<Camera>();
    }

}
