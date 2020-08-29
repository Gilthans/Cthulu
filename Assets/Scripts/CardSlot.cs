using System;
using UnityEngine;


public enum HorizontalPosition
{
    BACK = 1, MID = 2, FRONT = 3
}

public enum VerticalPosition
{
    BOTTOM = 1, TOP = 2
}

public class CardSlot : MonoBehaviour
{
    public HorizontalPosition horizontalPosition;
    public VerticalPosition verticalPosition;

    void Start()
    {
        if (name.Contains("Mid"))
            horizontalPosition = HorizontalPosition.MID;
        else if (name.Contains("Front"))
            horizontalPosition = HorizontalPosition.FRONT;
        else if (name.Contains("Back"))
            horizontalPosition = HorizontalPosition.BACK;
        else
            throw new NotImplementedException($"Unknown hposition in {name}");

        if (name.Contains("Top"))
            verticalPosition = VerticalPosition.TOP;
        else if (name.Contains("Bottom"))
            verticalPosition = VerticalPosition.BOTTOM;
        else
            throw new NotImplementedException($"Unknown hposition in {name}");
    }
}
