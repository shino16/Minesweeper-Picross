using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    //initialise game size
    public int width = 16;
    public int height = 16;
    public int mineNum = 50;

    private Board board;
    private Cell[,] state;
    private Cell[,] mines;
    private int[] picrossRows;
    private int[] picrossColumns;



    private void Awake(){
        board = GetComponentInChildren<Board>();
    }


    private void Start(){
        NewGame();
    }


    //generates new game (need to modify to start new game on user input later)
    private void NewGame(){
        //new game has initial game state
        state = new Cell[width, height];
        MakeCells();
        MakeMines();
        NumberCells();
        MakeSquares();
        board.drawGrid(state);
    }




    //INITIAL SET UP =====================================================================================

    private void MakeMines(){
        //Iterates through the number of mines required (set to 50 by default)
        for (int i = 0; i < mineNum; i++){
            int x = 0;
            int y = 0;
            bool isMine = true;
            while (isMine){
                //Mines can be anywhere ranging from (0,0) to (width-1, height-1)
                x = Random.Range(0, width);
                y = Random.Range(0,height);
                //This is to avoid generating a mine in the same position more than once. This ensures we have exactly 50 mines
                if (!(state[x,y].number == -1)){
                    isMine = false;
                }
            }
            
            //sets cell to -1, allows board to draw the correct cell type (nuke-y)
            state[x,y].number = -1;
        }
    }



    private void MakeSquares(){
        //generates a random number between 2 and just below half of the respective dimension
        //This determines the number of the picross rows and columns
        int rows = Random.Range(2,(height/2)-2);
        int columns = Random.Range(2,(width/2)-2);

        picrossRows = new int[rows]; 
        picrossColumns = new int[columns]; 

        //This randomly picks the rows and columns that will be the picross part of the game. 
        //The number of rows and columns does not exceed the numbers generated above.
        for (int i = 0; i < rows; i++){
            picrossRows[i] = Random.Range(0,height);
        }


        for (int i = 0; i < columns; i++){
            picrossColumns[i] = Random.Range(0,width);
        }


        foreach (int row in picrossRows){
            int pos = -1; //initialise position of picross squares. Must be to the left of the minesweeper grid
            int picrossSquare = 0; //initialise length of first maximal consecutive non-nukey cells to 0
            int x = height-1; // to fill in picross squares from right to left we count the cells from right to left

            while (x >= 0){
                if (!(state[x,row].number == -1)){//if a cell is not a mine, set it to ? and increase our count of consecutive non-mines by 1
                    state[x,row].number = 9;
                    picrossSquare ++;
                }else{//otherwise reset count to 0 and move to the next picross square position, so if (-1,row) is filled in, (-2,row) will be next ...
                    if (picrossSquare != 0){
                        board.drawSquare(new Square(picrossSquare),pos,row);
                        pos --;
                        picrossSquare = 0;
                    }
                }
                x--;
            }
            board.drawSquare(new Square(picrossSquare),pos,row);
        }


        foreach (int column in picrossColumns){
            int pos = -1; //initialise position of picross squares. Must be to the above the minesweeper grid
            int picrossSquare = 0; //initialise length of first maximal consecutive non-nukey cells to 0
            int y = width-1; // fill in picross squares from bottom to top

            while (y >= 0){
                if (!(state[column,y].number == -1)){//if a cell is not a mine, set it to ? and increase our count of consecutive non-mines by 1
                    state[column,y].number = 9;
                    picrossSquare ++;
                }else{//otherwise reset count to 0 and move to next picross square position
                    if (picrossSquare != 0){
                        board.drawSquare(new Square(picrossSquare),column,pos);
                        pos --;
                        picrossSquare = 0;
                    }
                }
                y--;
            }
            board.drawSquare(new Square(picrossSquare),column,pos);
        }

    }



    private void MakeCells(){
        //for each box in our grid, make a new empty cell
        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                Cell cell = new Cell();
                cell.number = 0;
                cell.revealed = false;
                cell.flagged = false;
                state[x,y] = cell;
            }
        }
    }



    private int CountMines(int x, int y){
        int count = 0;
        for (int i = x-1; i < x+2; i++){//for each cell (x,y), check all 8 cells surrounding it (cell(x-1,y-1) to cell(x+1,y+1))
            for (int j = y-1; j < y+2; j++){
                if (i<0 || i>=width || j<0 || j>=height){//avoids checking cells that are out of bounds
                    continue;
                }
                if (state[i,j].number == -1){ //increase count if a surrounding cell is a mine
                    count ++;
                }
            }
        }
        return count;
    }



    private void NumberCells(){
        for (int x = 0; x < width; x++){//for each cell in the grid, count the mines surrounding it and number it accordingly
            for (int y = 0; y < height; y++){
                if (state[x,y].number != -1){
                    if (state[x,y].number != 9){
                        state[x,y].number = CountMines(x,y);
                    }
                    
                }
            }
        }
    }


    //USER INPUT==========================================================================================

    //private void Update(){ //Mobile commands???
      //  foreach(Touch touch in Input.touches){
        //    if (touch.phase == TouchPhase.Began){
          //  }
    //}

    private void Update(){
        if (Input.GetMouseButtonDown(0)){ // left click
            //flag
        }

        if (Input.GetMouseButtonDown(1)){ //right click
            //reveal
        }
    }


    private void FloodFill(){

    }

    
}
