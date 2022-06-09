using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : IMove
{
    private int newPositionX, newPositionZ;
    Player player;


    public void Execute()
    {
        player.transform.position = new Vector3(newPositionX, 0 , newPositionZ);
    }
}
