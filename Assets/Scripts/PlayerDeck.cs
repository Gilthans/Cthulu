using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerDeck : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Minion> cards;
    public Minion minionPrefab;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int NumCards()
    {
        return this.cards.Count;
    }

    public void Shuffle()
    {
        RandomUtils.Shuffle<Minion>(this.cards);
    }

    public List<Minion> Draw(int n)
    {

        List<Minion> drawn = new List<Minion>();
        int numToDraw = Math.Min(n, this.cards.Count);

        for (int i = 0; i <= numToDraw; i++)
        {
            drawn.Add(this.cards[this.cards.Count - 1]);
            this.cards.RemoveAt(this.cards.Count - 1);
        }

        return drawn;
    }

    public void Add(List<Minion> cards)
    {
        this.cards.AddRange(cards);
    }

    public void Add(Minion card)
    {
        this.cards.Add(card);
    }

    public void InitializeRandom(int n)
    {
        for (int i = 1; i <= n; i++)
        {
            Minion card = Instantiate(this.minionPrefab);
            this.Add(card);
        }
    }
}
