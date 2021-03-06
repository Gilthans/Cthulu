﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum HorizontalPosition
{
    BACK = 1, MID = 2, FRONT = 3
}

public enum VerticalPosition
{
    BOTTOM = 1, TOP = 2
}

public class CardSlot : MonoBehaviour, IDropHandler
{
    public HorizontalPosition horizontalPosition;
    public VerticalPosition verticalPosition;

    public bool IsPlayerSlot =>  GetComponentInParent<MinionField>().name == "Player";

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

    public void OnDrop(PointerEventData eventData)
    {
        if (GetComponentInChildren<Minion>() != null || !IsPlayerSlot)
            return;

        var minion = eventData.pointerDrag;
        minion.transform.SetParent(transform);
        var minionRect = minion.GetComponent<RectTransform>();
        var thisRect = GetComponent<RectTransform>();
        minionRect.anchoredPosition = thisRect.anchoredPosition;
    }
}
