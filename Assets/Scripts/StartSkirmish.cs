using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StartSkirmish : MonoBehaviour
{
    public float TimeBetweenAttacks = 1;
    public GameMaster GameMaster;

    private bool isFighting = false;
    private float timeToNextAttack = 0;
    private bool isPlayerTurn;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFighting)
            return;

        timeToNextAttack -= Time.deltaTime;
        if (timeToNextAttack > 0)
            return;

        if (!GameMaster.PlayerMinionField.LiveMinions.Any() || !GameMaster.EnemyMinionField.LiveMinions.Any())
        {
            Debug.Log("Battle over");
            isFighting = false;
            GetComponent<Button>().interactable = true;
            return;
        }

        Debug.Log("Attacking!");
        timeToNextAttack = TimeBetweenAttacks;

        var attackerField = isPlayerTurn ? GameMaster.PlayerMinionField : GameMaster.EnemyMinionField;
        var defenderField = isPlayerTurn ? GameMaster.EnemyMinionField : GameMaster.PlayerMinionField;

        isPlayerTurn = !isPlayerTurn;

        var attacker = attackerField.GetAttacker();
        var liveDefenderMinions = defenderField.LiveMinions.ToList();
        var defenderIndex = Random.Range(0, liveDefenderMinions.Count);
        Debug.Log($"Attacking {defenderIndex}");
        var defender = liveDefenderMinions[defenderIndex];

        attacker.PerformAttack(defender);


    }

    public void OnClick()
    {
        if (isFighting)
            return;

        Debug.Log("Battle Starting!");
        isFighting = true;
        GetComponent<Button>().interactable = false;
        timeToNextAttack = 0;
        var coinflip = Random.Range(0, 2);
        Debug.Log($"Coin flip: {coinflip}");
        isPlayerTurn = coinflip == 1;
    }
}
