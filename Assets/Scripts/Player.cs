using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int number = (int)default;
    private Vector2 desiredPosition = (Vector2)default;
    private float moveSpeed = 2.0f;
    private Vector2Int currentPosition = (Vector2Int)default;

    [SerializeField] bool isMoving;

    public Vector2Int CurrentPosition { get => currentPosition; set => currentPosition = value; }
    public int Number { get => number; set => number = value; }
    public bool IsMoving { get => isMoving; }

    private Grid tileGrid;



    private void Awake()
    {
        
    }

    public void MoveTo(Tile tile)
    {
        desiredPosition = new Vector2(tile.x,tile.z);
        CurrentPosition = new Vector2Int(tile.x,tile.z);
    }

    public void MoveTo(Vector2Int position)
    {
        desiredPosition = position;
        CurrentPosition = position;
    }

    private void Update()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(desiredPosition.x,0,desiredPosition.y), step);
        if (Vector3.Distance(transform.position, desiredPosition) > 0.1f)
        {
            isMoving = true;
        } 
        else
        {
            isMoving = false;
        }
    }
}
