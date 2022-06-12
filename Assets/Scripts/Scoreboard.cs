using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private List<GameObject> scoreObjects;


    [ContextMenu("SetScoresAllTime")]
    private void SetScoresAllTime()
    {
        for (int i = 0; i < scoreObjects.Count; i++)
        {
            Scores scores = scoreObjects[i].GetComponent<Scores>();
            scores.NumberText.text = i.ToString();
        }
    }

    private void SetScoresFromLastWeek()
    {

    }
}
