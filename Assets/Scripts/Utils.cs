using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomUtils
{
    public static void Shuffle<T>(List<T> list)
    {        
        for (int n = list.Count - 1; n > 1; n--)
        {
            int k = Random.Range(0,n);
            T temp = list[n];
            list[n] = list[k];
            list[k] = temp;
        }
    }
}