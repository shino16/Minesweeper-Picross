using UnityEngine;
using UnityEngine.Tilemaps;

// Checks for the Tilemap in the Unity engine
[RequireComponent(typeof(Tilemap))]
// Inherits from MonoBehaviour since it uses a Tilemap
public class Board : MonoBehaviour
{
    int xOffset = 8;
    int yOffset = 15;

    public Tilemap tilemap
    {
        get;
        private set;
    }

    // List of tiles
    public Tile cell1;
    public Tile cell2;
    public Tile cell3;
    public Tile cell4;
    public Tile cell5;
    public Tile cell6;
    public Tile cell7;
    public Tile cell8;
    public Tile cellEmpty;
    public Tile cellUnknown;
    public Tile cellHidden;
    public Tile cellFlagged;
    public Tile cellMine;
    public Tile square1;
    public Tile square2;
    public Tile square3;
    public Tile square4;
    public Tile square5;
    public Tile square6;
    public Tile square7;
    public Tile square8;
    public Tile square9;
    public Tile square10;
    public Tile square11;
    public Tile square12;
    public Tile square13;
    public Tile square14;
    public Tile square15;
    public Tile square16;


    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }


    // Converts coordinates on the board to a cell position on tilemap.
    public Vector3Int CoordToCell(int i, int j) {
        return new Vector3Int(i + xOffset, yOffset - j, 0);
    }


    // Converts a cell position on tilemap to coordinates on the board.
    public (int, int) CellToCoord(Vector3Int vec) {
        return (vec.x - xOffset, -vec.y + yOffset);
    }


    // Converts a screen position wrt the main camera (e.g. Input.mousePosition)
    // to coordinates on the board.
    public (int, int) ScreenToCoord(Vector3 vec) {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(vec);
        Vector3Int cell = tilemap.WorldToCell(worldPoint);
        return CellToCoord(cell);
    }


    /* All draw methods are given (i,j) coordinates relative to
    only the Minesweeper grid (clickable cells) i.e. the top-left cell
    is at (0,0) and a potential Picross square to the
    left of it is at (-1,0) */

    public void drawSquare(Square square, int i, int j)
    {
        Vector3Int pos = CoordToCell(i, j);
        tilemap.SetTile(pos, squareTile(square));
    }


    public void drawCell(Cell cell, int i, int j)
    {
        Vector3Int pos = CoordToCell(i, j);
        tilemap.SetTile(pos, cellTile(cell));
    }


    private Tile squareTile(Square square)
    {
        switch (square.number)
        {
            case 1: return square1;
            case 2: return square2;
            case 3: return square3;
            case 4: return square4;
            case 5: return square5;
            case 6: return square6;
            case 7: return square7;
            case 8: return square8;
            case 9: return square9;
            case 10: return square10;
            case 11: return square11;
            case 12: return square12;
            case 13: return square13;
            case 14: return square14;
            case 15: return square15;
            case 16: return square16;
            default: return null;
        }
    }


    private Tile cellTile(Cell cell)
    {
        if (cell.flagged)
        {
            return cellFlagged;
        }
        else if (!cell.revealed)
        {
            return cellHidden;
        }
        else
        {
            switch (cell.number)
            {
                case -1: return cellMine;
                case 0: return cellEmpty;
                case 1: return cell1;
                case 2: return cell2;
                case 3: return cell3;
                case 4: return cell4;
                case 5: return cell5;
                case 6: return cell6;
                case 7: return cell7;
                case 8: return cell8;
                case 9: return cellUnknown;
                default: return null;
            }
        }
    }
}
