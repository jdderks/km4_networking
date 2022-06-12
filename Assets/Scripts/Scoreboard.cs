using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private List<GameObject> scoreObjects;
    private string[] importedScoreTexts;

    [ContextMenu("Lastweekupdate")]
    private void LastWeekUpdate()
    {
        StartCoroutine(GetLastWeekInfo());
    }

    [ContextMenu("SetScoresAllTime")]
    private void SetScoresAllTime()
    {
        int number = 0;
        for (int i = 0; i < scoreObjects.Count; i++)
        {
            //Set number
            Scores scores = scoreObjects[i].GetComponent<Scores>();
            number++;
            scores.NumberText.text = number.ToString();


            //Set Username

        }
    }

    private void SetScoresFromLastWeek()
    {

    }

    private IEnumerator GetLastWeekInfo()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://studentdav.hku.nl/~joris.derks/networking/get_bestinlastweek.php");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("Couldn't collect info.");
        }
        else
        {
            if (www.downloadHandler.text != "Fail")
            {
                string json = www.downloadHandler.text;
                string[] jsonArray = json.Split(new string[] { "[", "]", "{", "}", ":", ",", "name", "score", "time", '"'.ToString()}, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < jsonArray.Length; i+=3)
                {
                    Debug.Log(jsonArray[i]);
                    HighScore high = new HighScore();
                    high.name = jsonArray[i];
                    high.score = int.Parse(jsonArray[i+1]);
                    high.date = long.Parse(jsonArray[i+2]);
                }
            }
        }
    }
}

[Serializable]
public class HighScore
{
    public string name;
    public int score;
    public long date;
}
