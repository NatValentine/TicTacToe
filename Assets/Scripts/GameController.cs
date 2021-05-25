using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject board;
    public GameObject player1Token;
    public GameObject player2Token;
    private bool isPlayer1Turn;
    public GameObject[] slots;
    private int[] logicSlots;
    private GameObject[] tokensInPlay;
    public Text gameOverTxt;
    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        logicSlots = new int[9];
        
        // Initialize everything for a new game.
        InitializeBoard();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    GameObject newToken;
    public void MakeAMove(int slot)
    {
        // Each slot sends a number from 1 to 9. Since the array starts at 0 I decrease it by 1.
        slot--;

        // Depending on which player's turn it is, I instantiate one token or the other.
        if (isPlayer1Turn)
        {
            newToken = Instantiate(player1Token, slots[slot].transform.position, Quaternion.identity);
            logicSlots[slot] = 1;
        } else
        {
            newToken = Instantiate(player2Token, slots[slot].transform.position, Quaternion.identity);
            logicSlots[slot] = 2;
        }

        // Set the new token as child of the board. It must be inside a canvas for it to show.
        newToken.transform.SetParent(board.transform);

        // Had a problem that when instantiating as child of the board it would mess up the scaling of the token.
        newToken.transform.localScale = new Vector3(1f, 1f, 1f);

        // Since players shouldn't be able to place multiple tokens in the same slot, I deactivate it.
        slots[slot].SetActive(false);

        // Check if game is still on
        CheckGameOver();
        
        // End of turn, change current player.
        NextTurn();
    }

    void NextTurn()
    {
        isPlayer1Turn = !isPlayer1Turn;
    }

    void KillAllTokens()
    {
        tokensInPlay = GameObject.FindGameObjectsWithTag("Token");
        foreach (GameObject t in tokensInPlay)
            Destroy(t);
    }

    public void InitializeBoard()
    {
        // Set gameOver as false
        gameOver = false;

        // Clear slots
        KillAllTokens();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetActive(true);
            logicSlots[i] = 0;
        }
                
        // Reset game over text
        gameOverTxt.text = "";

        // Set player 1 as the current player;
        isPlayer1Turn = true;
    }

    void CheckGameOver()
    {
        /* 8 win scenarios + tie
         * Logic slots as follows:
         * 0|1|2
         * 3|4|5
         * 6|7|8
         * */

        // I heard you like if statements, enjoy.
        if((logicSlots[0] != 0) && (logicSlots[0] == logicSlots[1]) && (logicSlots[1] == logicSlots[2]))
        {
            CurrentPlayerWins();
        }

        if ((logicSlots[3] != 0) && (logicSlots[3] == logicSlots[4]) && (logicSlots[4] == logicSlots[5]))
        {
            CurrentPlayerWins();
        }

        if ((logicSlots[6] != 0) && (logicSlots[6] == logicSlots[7]) && (logicSlots[7] == logicSlots[8]))
        {
            CurrentPlayerWins();
        }

        if ((logicSlots[0] != 0) && (logicSlots[0] == logicSlots[3]) && (logicSlots[3] == logicSlots[6]))
        {
            CurrentPlayerWins();
        }

        if ((logicSlots[1] != 0) && (logicSlots[1] == logicSlots[4]) && (logicSlots[4] == logicSlots[7]))
        {
            CurrentPlayerWins();
        }

        if ((logicSlots[2] != 0) && (logicSlots[2] == logicSlots[5]) && (logicSlots[5] == logicSlots[8]))
        {
            CurrentPlayerWins();
        }

        if ((logicSlots[0] != 0) && (logicSlots[0] == logicSlots[4]) && (logicSlots[4] == logicSlots[8]))
        {
            CurrentPlayerWins();
        }

        if ((logicSlots[2] != 0) && (logicSlots[2] == logicSlots[4]) && (logicSlots[4] == logicSlots[6]))
        {
            CurrentPlayerWins();
        }

        if(logicSlots.All(s => s > 0) && gameOver == false)
        {
            //no zeroes left means there's no more room and therefore a tie
            GameOver("It's a tie!");
        }
    }

    void CurrentPlayerWins()
    {
        // Set game over as true
        gameOver = true;

        if (isPlayer1Turn)
        {
            GameOver("X WINS!");
        }
        else
        {
            GameOver("O WINS!");
        }
    }

    void GameOver(string s)
    {
        gameOverTxt.text = s;

        // Can't make any more moves
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetActive(false);
        }
    }
}
