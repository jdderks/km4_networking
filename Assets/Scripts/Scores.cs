using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scores : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI numberText;
    [SerializeField] private TMPro.TextMeshProUGUI nameText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI dateText;

    public TextMeshProUGUI NumberText { get => numberText; set => numberText = value; }
    public TextMeshProUGUI NameText { get => nameText; set => nameText = value; }
    public TextMeshProUGUI ScoreText { get => scoreText; set => scoreText = value; }
    public TextMeshProUGUI DateText { get => dateText; set => dateText = value; }
}
