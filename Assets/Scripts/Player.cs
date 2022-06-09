using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //The number this player is assigned
    private int assignedNumber = (int)default;

    //Movement Related
    private float moveSpeed = 8.0f;
    private Vector2 desiredPosition = (Vector2)default;
    private Vector2Int currentPosition = (Vector2Int)default;

    //Whether or not the player is currently moving
    [SerializeField] bool isMoving;

    [SerializeField] int movesLeft = 5;

    public Vector2Int CurrentPosition { get => currentPosition; set => currentPosition = value; }
    public int AssignedNumber { get => assignedNumber; set => assignedNumber = value; }
    public bool IsMoving { get => isMoving; }
    public int MovesLeft { get => movesLeft; set => movesLeft = value; }

    public void MoveTo(Tile tile)
    {
        if (tile != null)
        {
            desiredPosition = new Vector2(tile.x, tile.z);
            currentPosition = new Vector2Int(tile.x, tile.z); 
            transform.position = new Vector3(desiredPosition.x, 0 ,desiredPosition.y);

        }
    }

    public void MoveTo(Vector2Int position)
    {
        desiredPosition = position;
        currentPosition = position;
        transform.position = new Vector3(desiredPosition.x, 0, desiredPosition.y);

    }

    private void Update()
    {
        
    }
}
