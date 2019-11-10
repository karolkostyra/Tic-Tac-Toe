using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scores : MonoBehaviour
{
    [SerializeField] private Text TMP_player_1;
    [SerializeField] private Text TMP_player_2;
    private int P1_value;
    private int P2_value;


    private void Awake()
    {
        GameController.OnUpdateScore += AddPoint;
    }

    private void Start()
    {
        P1_value = P2_value = 0;
    }

    /*
    private void AddPoint(int P1_score, int P2_score)
    {
        P1_value += P1_score;
        P2_value += P2_score;
        
        if (P1_score == 1)
        {
            TMP_player_1.text = P1_value.ToString();
        }
        if (P2_score == 1)
        {
            TMP_player_2.text = P2_value.ToString();
        }
    }
    */
    private void AddPoint(int whoScores)
    {
        if (whoScores == 0)
        {
            P1_value++;
            TMP_player_1.text = P1_value.ToString();
        }
        if (whoScores == 1)
        {
            P2_value++;
            TMP_player_2.text = P2_value.ToString();
        }
    }
}
