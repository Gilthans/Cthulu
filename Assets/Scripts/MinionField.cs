using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinionField : MonoBehaviour
{
    public List<Minion> minions;
    public int currentAttacker = 0;

    public IEnumerable<Minion> LiveMinions
    {
        get
        {
            return minions.Where(m => m.IsAlive());
        }
    }

    void Start()
    {
        minions = GetComponentsInChildren<Minion>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        } while (!result.IsAlive());

        return result;
    }
}
