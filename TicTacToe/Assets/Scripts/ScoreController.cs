using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private Text TMP_p1;
    private Text TMP_p2;
    private int P1_value;
    private int P2_value;

    void Start()
    {
        TMP_p1 = CreateScoreHandler(1, new Vector3(-325, 365, 0)).GetComponent<Text>();
        TMP_p2 = CreateScoreHandler(2, new Vector3(325, 365, 0)).GetComponent<Text>();

        P1_value = P2_value = 0;
    }

    public void AddPoint(int whoScores)
    {
        if (whoScores == 0)
        {
            P1_value++;
            Debug.Log(P1_value);
            TMP_p1.text = P1_value.ToString();
        }
        if (whoScores == 1)
        {
            P2_value++;
            TMP_p2.text = P2_value.ToString();
        }
    }

    private GameObject CreateScoreHandler(int number, Vector3 pos)
    {
        var go = GameObject.Find("Canvas");
        var i = number;
        GameObject go_1 = new GameObject("TMP_new_p" + i.ToString());
        go_1.transform.SetParent(go.transform);
        Text player_txt = go_1.AddComponent<Text>();
        player_txt.text = "0";
        player_txt.GetComponent<RectTransform>().anchoredPosition = pos;
        player_txt.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 200);
        Font font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        player_txt.font = font;
        player_txt.fontSize = 135;
        player_txt.alignment = TextAnchor.MiddleCenter;
        player_txt.color = new Color32(50, 50, 50, 255);
        return go_1;
    }
}