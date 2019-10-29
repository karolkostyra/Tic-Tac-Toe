using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    private int turnCount; //counts the number of turn played
    private int matchCount; //counts the number of match played
    private int whoseTurn; //0 = P1 turn, 1 = P2 turn
    private int whichIcon; // 0 = red 'X', 1 = blue 'O'
    //private int P1_icon;
    public int[] markedSpaces; //ID's which space in grid was marked by which player

    [SerializeField] private GameObject[] turnIcons; //displays whose turn it is
    [SerializeField] private Sprite[] Icons; //0 = red 'X', 1 = blue 'O'
    [SerializeField] private Button[] gridSpaces; //playable spaces in 3x3 grid
    [SerializeField] private TextMeshProUGUI TMP_showText; //displays text about which player start the game
    [SerializeField] private TextMeshProUGUI TMP_Player_1;
    [SerializeField] private TextMeshProUGUI TMP_Player_2;


    private void Start()
    {
        markedSpaces = new int[9];
        ResetGrid();
    }

    private void Update()
    {
        
    }

    private void GameSetup()
    {
        whoseTurn = 0;
        turnCount = 0;
        matchCount = 0;
        whichIcon = Random.Range(0, 2); //pick randomly icon for first player
        //whoseTurn = whichIcon; //P1 always start the game (with random icon)

        SetPlayersColor(whichIcon);

        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        TMP_showText.text = "Player 1's starts game!";
    }

    public void GridButtonIcon(int number)
    {
        gridSpaces[number].image.sprite = Icons[whichIcon];
        gridSpaces[number].interactable = false;
        markedSpaces[number] = whoseTurn + 1;

        whichIcon = (whichIcon + 1) % 2; // TO CHANGE!
        turnCount++;

        if(turnCount > 4)
            CheckForWinner();

        if (whoseTurn == 0)
        {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    void SetPlayersColor(int whichPlayer)
    {
        if(whichPlayer == 0)
        {
            TMP_Player_1.faceColor = new Color32(209, 1, 1, 255); // #D10101 red
            TMP_Player_2.faceColor = new Color32(0, 85, 255, 255); // #0055FF blue 
        }
        else
        {
            TMP_Player_1.faceColor = new Color32(0, 85, 255, 255); // #0055FF blue
            TMP_Player_2.faceColor = new Color32(209, 1, 1, 255); // #D10101 red
        }
    }

    private void CheckForWinner3()
    {
        int[] v = markedSpaces;


        bool row_1 = v[0] == v[1] && v[1] == v[2];
        bool row_2 = v[3] == v[4] && v[4] == v[5];
        bool row_3 = v[6] == v[7] && v[7] == v[8];
        bool col_1 = v[0] == v[3] && v[3] == v[6];
        bool col_2 = v[1] == v[4] && v[4] == v[7];
        bool col_3 = v[2] == v[5] && v[5] == v[8];
        bool diag_1 = v[0] == v[4] && v[4] == v[8];
        bool diag_2 = v[2] == v[4] && v[4] == v[6];

        bool[] winConditions = new bool[] { row_1, row_2, row_3, col_1, col_2, col_3, diag_1, diag_2 };

        for(int i = 0; i < winConditions.Length; i++)
        {
            if(winConditions[i] == true)
                TMP_showText.text = "Player " + (whoseTurn+1) + "wins!";
        }
    }

    private void CheckForWinner()
    {
        int[] v = markedSpaces;

        
        int row_1 = v[0] + v[1] + v[2];
        int row_2 = v[3] + v[4] + v[5];
        int row_3 = v[6] + v[7] + v[8];
        int col_1 = v[0] + v[3] + v[6];
        int col_2 = v[1] + v[4] + v[7];
        int col_3 = v[2] + v[5] + v[8];
        int diag_1 = v[0] + v[4] + v[8];
        int diag_2 = v[2] + v[4] + v[6];

        int[] winConditions = new int[] { row_1, row_2, row_3, col_1, col_2, col_3, diag_1, diag_2 };

        //two-dimensional array
        int[,] winConditions2 = new int[8,3] { {0,1,2}, {3,4,5}, {6,7,8}, {0,3,6}, {1,4,7}, {2,5,8}, {0,4,8}, {2,4,6} };
        
        /*
        for (int i = 0; i < winConditions.Length; i++)
        {
            //Debug.Log(markedSpaces[winConditions[i, 0]]);
            if (markedSpaces[winConditions[i, 0]] == markedSpaces[winConditions[i, 1]] && markedSpaces[winConditions[i, 0]] == markedSpaces[winConditions[i, 2]])
            {
                Debug.Log(markedSpaces[winConditions[i, 0]] == markedSpaces[winConditions[i, 1]]);
                Debug.Log(markedSpaces[winConditions[i, 0]] == markedSpaces[winConditions[i, 2]]);
                TMP_showText.text = "Player " + ++whoseTurn + "wins!";
            }
        }
        */

        for (int i = 0; i < winConditions.Length; i++)
        {
            if (!CheckValues(i, winConditions2))

                Debug.Log("nope!");
                //i++;
            //if (winConditions[i] == System.Math.Abs(3*(whoseTurn+1)))
            else
                TMP_showText.text = "Player " + (whoseTurn+1) + "wins!";
        }
    }

    private bool CheckValues(int i, int[,] values)
    {
        int[] v = markedSpaces;
        Debug.Log("i=" + i + ": "+v[values[i, 0]] + ", " + v[values[i, 1]] + ", " + v[values[i, 2]]);
        if (v[values[i, 0]] == v[values[i, 1]] && v[values[i, 1]] == v[values[i, 2]])
        {
            Debug.Log("true");
            return true;
        }
        return false;
    }

    public void ResetGrid() //works but not reset which player starts match (only reset grid)
    {
        GameSetup();

        for (int i = 0; i < gridSpaces.Length; i++)
        {
            gridSpaces[i].interactable = true;
            gridSpaces[i].GetComponent<Image>().sprite = null;
        }

        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -1;
        }
    }
}
