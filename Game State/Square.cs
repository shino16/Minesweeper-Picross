using UnityEngine;

/* A struct - a value type, meaning it is not designed to be
changed at all, after instantiation */
public struct Square 
{
    // The number representing the size of one of the non-mine blocks in the row/col
    public readonly int number;

    // Constructor - to be used upon instantiation
    public Square(int num) 
    {
        number = num;
    }
}