using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public delegate void UpdateScore(int P1_score, int P2_score);
    public static event UpdateScore OnUpdateScore;

    private int turnCount; //counts the number of turn played
    private int matchCount; //counts the number of match played
    private int whoseTurn; //0 = P1 turn, 1 = P2 turn
    private int whichIcon; // 0 = red 'X', 1 = blue 'O'
    private int[] markedSpaces; //ID's which space in grid was marked by which player
    private int markValue; // -1 for P1 and 1 for P2

    [SerializeField] private GameObject[] turnIcons; //displays whose turn it is
    [SerializeField] private GameObject[] winningLines;
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
    
    private void GameSetup()
    {
        markValue = -1;
        whoseTurn = turnCount = matchCount = 0;
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
        markedSpaces[number] = markValue;
        markValue = -markValue;

        whichIcon = (whichIcon + 1) % 2; // TO CHANGE!
        turnCount++;

        if (turnCount > 0)
        {
            TMP_showText.text = "";
        }

        if (turnCount > 4)
        {
            CheckForWinner();
        }

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

    private void CheckForWinner()
    {
        //all possible scenarios for win
        int row_1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int row_2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int row_3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int col_1 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int col_2 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int col_3 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int diag_1 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int diag_2 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];

        int[] winConditions = new int[] { row_1, row_2, row_3, col_1, col_2, col_3, diag_1, diag_2 };

        //-1 marks for P1 so 3 in row/colum or diagonally == -3
        //1 marks for P2 so 3 in row/colum or diagonally == 3
        for (int i = 0; i < winConditions.Length; i++)
        {
            if (winConditions[i] == -3) 
            {
                TMP_showText.text = "Player 1 wins!";
                DisplayWinningLine(i);
                OnUpdateScore(1, 0);
            }
            if (winConditions[i] == 3)
            {
                TMP_showText.text = "Player 2 wins!";
                DisplayWinningLine(i);
                OnUpdateScore(0, 1);
            }
        }
    }

    private void DisplayWinningLine(int index)
    {
        winningLines[index].SetActive(true);

        for (int i = 0; i < gridSpaces.Length; i++)
        {
            gridSpaces[i].interactable = false;
        }
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
            markedSpaces[i] = 0;
        }

        for(int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }
    }

    void SetPlayersColor(int whichPlayer)
    {
        if (whichPlayer == 0)
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
}
