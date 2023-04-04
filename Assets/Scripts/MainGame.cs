using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainGame : MonoBehaviour
{
    //initialise game size
    public int width = 16;
    public int height = 16;
    public int mineNum = 50;

    public GameObject gameOverTitle, gameOverMessage, gameOverBackground;

    private const float LongPressDuration = 0.5f;

    private Board board;
    private Cell[,] state;
    private Cell[,] mines;
    private int[] picrossRows;
    private int[] picrossColumns;
    private int score;

    private void Awake()
    {
        board = GetComponentInChildren<Board>();
    }

    private void Start()
    {
        NewGame();
    }

    //generates new game (need to modify to start new game on user input later)
    private void NewGame()
    {
        //new game has initial game state
        state = new Cell[height, width];
        MakeCells();
        MakeMines();
        NumberCells();
        MakeSquares();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                board.drawCell(state[i, j], i, j);
            }
        }
    }

    //INITIAL SET UP =====================================================================================

    private void MakeMines()
    {
        //Iterates through the number of mines required (set to 50 by default)
        for (int iter = 0; iter < mineNum; iter++)
        {
            int i;
            int j;
            do
            {
                //Mines can be anywhere ranging from (0,0) to (height-1, width-1)
                i = Random.Range(0, height);
                j = Random.Range(0, width);
                //This is to avoid generating a mine in the same position more than once. This ensures we have exactly 50 mines
            } while (state[i, j].number == -1);

            //sets cell to -1, allows board to draw the correct cell type (nuke-y)
            state[i, j].number = -1;
        }
    }

    private void MakeSquares()
    {
        //generates a random number between 2 and just below half of the respective dimension
        //This determines the number of the picross rows and columns
        int rows = Random.Range(2, (height / 2) - 2);
        int columns = Random.Range(2, (width / 2) - 2);

        picrossRows = new int[rows];
        picrossColumns = new int[columns];

        //This randomly picks the rows and columns that will be the picross part of the game.
        //The number of rows and columns does not exceed the numbers generated above.
        var picrossRowsSet = new HashSet<int>();
        var picrossColumnsSet = new HashSet<int>();
        while (picrossRowsSet.Count < rows)
        {
            picrossRowsSet.Add(Random.Range(0, height));
        }
        while (picrossColumnsSet.Count < columns)
        {
            picrossColumnsSet.Add(Random.Range(0, width));
        }
        picrossRowsSet.CopyTo(picrossRows);
        picrossColumnsSet.CopyTo(picrossColumns);

        foreach (int i in picrossRows)
        {
            int pos = -1; //initialise position of picross squares. Must be to the left of the minesweeper grid
            int picrossSquare = 0; //initialise length of first maximal consecutive non-nukey cells to 0

            // to fill in picross squares from right to left we count the cells from right to left
            for (int j = width - 1; j >= 0; j--)
            {
                if (state[i, j].number != -1)
                {//if a cell is not a mine, set it to ? and increase our count of consecutive non-mines by 1
                    state[i, j].number = 9;
                    picrossSquare++;
                }
                else
                {//otherwise reset count to 0 and move to the next picross square position, so if (i,-1) is filled in, (i,-2) will be next ...
                    if (picrossSquare != 0)
                    {
                        board.drawSquare(new Square(picrossSquare), i, pos);
                        pos--;
                        picrossSquare = 0;
                    }
                }
            }
            board.drawSquare(new Square(picrossSquare), i, pos);
        }

        foreach (int j in picrossColumns)
        {
            int pos = -1; //initialise position of picross squares. Must be to the above the minesweeper grid
            int picrossSquare = 0; //initialise length of first maximal consecutive non-nukey cells to 0

            // fill in picross squares from bottom to top
            for (int i = height - 1; i >= 0; i--)
            {
                if (state[i, j].number != -1)
                {//if a cell is not a mine, set it to ? and increase our count of consecutive non-mines by 1
                    state[i, j].number = 9;
                    picrossSquare++;
                }
                else
                {//otherwise reset count to 0 and move to next picross square position
                    if (picrossSquare != 0)
                    {
                        board.drawSquare(new Square(picrossSquare), pos, j);
                        pos--;
                        picrossSquare = 0;
                    }
                }
            }
            board.drawSquare(new Square(picrossSquare), pos, j);
        }
    }

    private void MakeCells()
    {
        //for each box in our grid, make a new empty cell
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Cell cell = new Cell();
                cell.number = 0;
                cell.revealed = false;
                cell.flagged = false;
                state[i, j] = cell;
            }
        }
    }

    private int CountMines(int centerI, int centerJ)
    {
        int count = 0;
        for (int i = centerI - 1; i < centerI + 2; i++)
        {//for each cell (i,j), check all 8 cells surrounding it (cell(i-1,j-1) to cell(i+1,j+1))
            for (int j = centerJ - 1; j < centerJ + 2; j++)
            {
                if (i < 0 || i >= height || j < 0 || j >= width)
                {//avoids checking cells that are out of bounds
                    continue;
                }
                if (state[i, j].number == -1)
                { //increase count if a surrounding cell is a mine
                    count++;
                }
            }
        }
        return count;
    }

    private void NumberCells()
    {
        for (int i = 0; i < height; i++)
        {//for each cell in the grid, count the mines surrounding it and number it accordingly
            for (int j = 0; j < width; j++)
            {
                if (state[i, j].number != -1 && state[i, j].number != 9)
                {
                    state[i, j].number = CountMines(i, j);
                }
            }
        }
    }

    //USER INPUT==========================================================================================

    // Returns the position vector if a right click or a long tap is detected
    // Returns null otherwise
    private Vector3? DetectFlagAction()
    {
        if (Input.GetMouseButtonDown(1))
            return Input.mousePosition;

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended
                    && touch.deltaTime >= LongPressDuration)
                return touch.position;
        }

        return null;
    }

    // Returns the position vector if a left click or a tap is detected
    // Returns null otherwise
    private Vector3? DetectRevealAction()
    {
        if (Input.GetMouseButtonDown(0))
            return Input.mousePosition;

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended
                    && touch.deltaTime < LongPressDuration)
                return touch.position;
        }

        return null;
    }

    private void Update()
    {
        if (DetectFlagAction() is Vector3 flagPos)
        {
            (int i, int j) = board.ScreenToCoord(flagPos);
            if (i >= 0 && i < height && j >= 0 && j < width)
            {
                Debug.Log($"Flag ({i}, {j})");
                if (state[i, j].revealed)
                    Debug.Log("Ignoring Flag on a revealed cell");
                else
                    ToggleFlagCell(i, j);
            }
        }

        if (DetectRevealAction() is Vector3 revealPos)
        {
            (int i, int j) = board.ScreenToCoord(revealPos);
            if (i >= 0 && i < height && j >= 0 && j < width)
            {
                Debug.Log($"Reveal ({i}, {j})");
                if (state[i, j].flagged)
                    Debug.Log("Ignoring Reveal on a flagged cell");
                else
                    RevealCell(i, j);
            }
        }
    }

    private void ToggleFlagCell(int i, int j)
    {
        state[i, j].flagged = !state[i, j].flagged;
        board.drawCell(state[i, j], i, j);
    }

    private void RevealCell(int i, int j)
    {
        state[i, j].flagged = false;
        state[i, j].revealed = true;
        board.drawCell(state[i, j], i, j);
        if (state[i, j].number == -1)
            GameOver();
        else
            score += 1;
    }

    private void GameOver()
    {
        TMP_Text message = gameOverMessage.GetComponentInChildren<TMP_Text>();
        message.text = string.Format(message.text, score);

        gameOverTitle.SetActive(true);
        gameOverMessage.SetActive(true);
        gameOverBackground.SetActive(true);
    }
}
