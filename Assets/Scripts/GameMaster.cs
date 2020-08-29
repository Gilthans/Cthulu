using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public MinionField PlayerMinionField;
    public MinionField EnemyMinionField;

    public event System.Action InteractionStateChange;

    private bool isGameInteractable;

    void Start()
    {
        Random.InitState(System.Guid.NewGuid().GetHashCode());
    }

    void Update()
    {
    }

    void RunSkirmish()
    {

    }
}
