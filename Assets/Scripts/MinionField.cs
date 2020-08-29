using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MinionField : MonoBehaviour
{
    public List<Minion> minions;
    public int currentAttacker = 0;

    public IEnumerable<Minion> LiveMinions
    {
        get
        {
            return minions.Where(m => m.IsAlive);
        }
    }

	void Start()
    {
        UpdateMinions();
    }

    public Minion GetAttacker()
    {
        if (!LiveMinions.Any())
            return null;

        Minion result;
        do
        {
            result = minions[currentAttacker];
            currentAttacker = (currentAttacker + 1) % minions.Count;
        } while (!result.IsAlive);

        return result;
    }

    internal void UpdateMinions()
    {
        minions = GetComponentsInChildren<Minion>().ToList();
        minions.Sort(new PositionComparer());
        minions.Reverse();
    }
}

internal class PositionComparer : IComparer<Minion>
{
    public int Compare(Minion x, Minion y)
    {
        var xSlot = x.GetComponentInParent<CardSlot>();
        var ySlot = y.GetComponentInParent<CardSlot>();
        if (xSlot.horizontalPosition != ySlot.horizontalPosition)
            return xSlot.horizontalPosition - ySlot.horizontalPosition;
        return xSlot.verticalPosition - ySlot.verticalPosition;
    }
}