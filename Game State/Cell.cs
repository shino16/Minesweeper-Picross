using UnityEngine;

public class Cell
{
    // An integer of 0 represents an empty cell, -1 represents a mine, 9 represents a ?
    public readonly int number;
    public bool revealed;
    public bool flagged;
}