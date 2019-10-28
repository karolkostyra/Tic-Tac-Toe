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
    private int P1_icon;
    [SerializeField] private GameObject[] turnIcons; //displays whose turn it is
    [SerializeField] private Sprite[] Icons; //0 = red 'X', 1 = blue 'O'
    [SerializeField] private Button[] gridSpaces; //playable spaces in 3x3 grid
    [SerializeField] private TextMeshProUGUI TMP_whoseStart; //displays text about which player start the game
    [SerializeField] private TextMeshProUGUI TMP_Player_1;
    [SerializeField] private TextMeshProUGUI TMP_Player_2;

    private int[] markedSpaces; //ID's which space in grid was marked by which player


    void Start()
    {
        markedSpaces = new int[9];
        ResetGrid();
    }
    
    void Update()
    {
        
    }

    void GameSetup()
    {
        whoseTurn = 0;
        turnCount = 0;
        matchCount = 0;
        whichIcon = Random.Range(0, 2); //pick randomly icon for first player
        //whoseTurn = whichIcon; //P1 always start the game (with random icon)

        SetPlayersColor(whichIcon);

        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        TMP_whoseStart.text = "Player 1's starts game!";

        //ResetGrid();

        //if (matchCount != 0)
          //  ResetGrid();
    }

    public void GridButtonIcon(int number)
    {
        gridSpaces[number].image.sprite = Icons[whichIcon];
        gridSpaces[number].interactable = false;
        markedSpaces[number] = whoseTurn;

        whichIcon = (whichIcon + 1) % 2; // TO CHANGE!
        turnCount++;

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
