using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private List<GameObject> scoreObjects;
    private List<HighScore> scores = new List<HighScore>();

    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    private void Start()
    {
        StartCoroutine(GetLastWeekInfo());
    }

    [ContextMenu("Lastweekupdate")]
    public void SetScoresFromLastWeek()
    {
        StartCoroutine(GetLastWeekInfo());
    }

    [ContextMenu("SetScoresAllTime")]
    public void SetScoresAllTime()
    {
        StartCoroutine(GetAllTimeInfo());
    }

    private IEnumerator GetAllTimeInfo()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://studentdav.hku.nl/~joris.derks/networking/get_bestalltime.php");
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError("Couldn't collect info.");
        }
        else
        {
            if (www.downloadHandler.text != "Fail")
            {
                scores.Clear();
                string json = www.downloadHandler.text;
                //Debug.Log(www.downloadHandler.text);

                string[] jsonArray = json.Split(new string[] { "[", "]", "{", "}", ":", ",", "name", "score", "time", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                List<string> jsonList = jsonArray.ToList();
                for (int i = 0; i < jsonArray.Length; i += 3)
                {
                    //Debug.Log(jsonArray[i]);
                    HighScore high = new HighScore();
                    high.name = jsonArray[i];
                    high.score = int.Parse(jsonArray[i + 1]);
                    high.date = long.Parse(jsonArray[i + 2]);
                    scores.Add(high);
                }
                int number = 0;
                for (int i = 0; i < scoreObjects.Count; i++)
                {
                    number++;
                    scoreObjects[i].GetComponent<Scores>().NumberText.text = number.ToString();
                    if (i < scores.Count)
                    {
                        scoreObjects[i].GetComponent<Scores>().NameText.text = scores[i].name;
                        scoreObjects[i].GetComponent<Scores>().ScoreText.text = scores[i].score.ToString();
                        DateTime timeAchieved = epoch.AddSeconds(scores[i].date);
                        scoreObjects[i].GetComponent<Scores>().DateText.text = timeAchieved.Day.ToString() + "-" + timeAchieved.Month.ToString() + "-" + timeAchieved.Year.ToString();
                    }
                    else
                    {
                        scoreObjects[i].GetComponent<Scores>().NameText.text = "-";
                        scoreObjects[i].GetComponent<Scores>().ScoreText.text = "-";
                        scoreObjects[i].GetComponent<Scores>().DateText.text = "-";
                    }
                }
            }
        }
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
                scores.Clear();
                string json = www.downloadHandler.text;
                //Debug.Log(www.downloadHandler.text);

                //TODO: use newtownsoft unity version download
                string[] jsonArray = json.Split(new string[] { "[", "]", "{", "}", ":", ",", "name", "score", "time", "\"" }, StringSplitOptions.RemoveEmptyEntries);
                List<string> jsonList = jsonArray.ToList();
                for (int i = 0; i < jsonArray.Length; i += 3)
                {
                    //Debug.Log(jsonArray[i]);
                    HighScore high = new HighScore();
                    high.name = jsonArray[i];
                    high.score = int.Parse(jsonArray[i + 1]);
                    high.date = long.Parse(jsonArray[i + 2]);
                    scores.Add(high);
                }
                int number = 0;
                for (int i = 0; i < scoreObjects.Count; i++)
                {
                    number++;
                    scoreObjects[i].GetComponent<Scores>().NumberText.text = number.ToString();
                    if (i < scores.Count)
                    {
                        scoreObjects[i].GetComponent<Scores>().NameText.text = scores[i].name;
                        scoreObjects[i].GetComponent<Scores>().ScoreText.text = scores[i].score.ToString();
                        DateTime timeAchieved = epoch.AddSeconds(scores[i].date);
                        scoreObjects[i].GetComponent<Scores>().DateText.text = timeAchieved.Day.ToString() + "-" + timeAchieved.Month.ToString() +"-"+ timeAchieved.Year.ToString();
                    }
                    else
                    {
                        scoreObjects[i].GetComponent<Scores>().NameText.text = "-";
                        scoreObjects[i].GetComponent<Scores>().ScoreText.text = "-";
                        scoreObjects[i].GetComponent<Scores>().DateText.text = "-";
                    }
                }
            }
        }
    }
}

[Serializable]
public class HighScoresWrapper
{
    public HighScore[] items;
}

[Serializable]
public class HighScore
{
    public string name;
    public int score;
    public long date;
}
