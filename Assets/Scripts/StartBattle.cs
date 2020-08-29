using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StartBattle : MonoBehaviour
{
    public float TimeBetweenAttacks = 1;
    public MinionField enemyField;
    public MinionField playerField;

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

        Debug.Log("Attacking!");
        timeToNextAttack = TimeBetweenAttacks;

        MinionField attackerField;
        MinionField defenderField;
        if (isPlayerTurn)
        {
            attackerField = isPlayerTurn ? playerField : enemyField;
            defenderField = isPlayerTurn ? enemyField: playerField;
        }
        else
        {
            attackerField = isPlayerTurn ? enemyField : playerField;
            defenderField = isPlayerTurn ? playerField : enemyField;
        }

        var attacker = attackerField.GetAttacker();
        var liveDefenderMinions = defenderField.LiveMinions.ToList();
        var defender = liveDefenderMinions[Random.Range(0, liveDefenderMinions.Count - 1)];

        attacker.PerformAttack(defender);

        if (!playerField.LiveMinions.Any() || !enemyField.LiveMinions.Any())
        {
            Debug.Log("Battle over");
            isFighting = false;
            GetComponent<Button>().interactable = true;
        }
    }

    public void OnClick()
    {
        if (isFighting)
            return;

        Debug.Log("Battle Starting!");
        isFighting = true;
        GetComponent<Button>().interactable = false;
        timeToNextAttack = TimeBetweenAttacks;
        isPlayerTurn = Random.Range(0, 1) == 1;
    }
}
