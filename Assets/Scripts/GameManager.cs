using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Player> players = new List<Player>();
    private UIManager uiManager;
    
    [SerializeField] private Grid tileGrid;

    private int playerTurn;

    [SerializeField] private GameObject playerPrefab;

    void Start()
    {
        CreateGrid();
        CreatePlayer(new Vector2Int(5, 5));
        AssignCamera();

        playerTurn = Random.Range(1, players.Count);
        tileGrid.CurrentPlayerPosition = new Vector2Int(5,5);
    }

    void Update()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (i == playerTurn)
            {
                players[i].MoveTo(tileGrid.SelectedTile);
            }
        }
    }

    
    private void CreatePlayer(Vector2Int tileSpawnPosition)
    {
        GameObject playerGO = Instantiate(playerPrefab, tileGrid.GetTileFromPosition(tileSpawnPosition).gameObject.transform.position, playerPrefab.transform.rotation);
        //playerPosition = new Vector2Int(tile.x, tile.z);
        Player player = playerGO.GetComponent<Player>();
        player.CurrentPosition = new Vector2Int(tileGrid.GetTileFromPosition(tileSpawnPosition).x, tileGrid.GetTileFromPosition(tileSpawnPosition).z);
        player.MoveTo(tileSpawnPosition);
        players.Add(player);
        player.Number = players.Count;
    }

    private void CreateGrid()
    {
        tileGrid.Generate();
    }

    private void AssignCamera()
    {
        tileGrid.Camera = players[playerTurn].GetComponentInChildren<Camera>();
    }
}
