using UnityEngine;

// Inherits from MonoBehaviour - done for all classes in Unity
public class Cell 
{
    // An integer of 0 represents an empty cell, -1 represents a mine, 9 represents a ?
    public int number;
    public bool revealed;
    public bool flagged;
    
}