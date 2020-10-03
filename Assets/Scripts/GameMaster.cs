using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;


public class GameMaster : MonoBehaviour
{
    public MinionField PlayerMinionField;
    public MinionField EnemyMinionField;

    public PlayerDeck Deck;
    public PlayerHand Hand;

    public float TimeBetweenAttacksSeconds = 1;

    public event System.Action InteractionStateChange;

    internal bool isGameInteractable = true;

    public int InitialDrawSize = 5;
    public int InitialDeckSize = 27;

    void Start()
    {
        Random.InitState(System.Guid.NewGuid().GetHashCode());

        Deck.InitializeRandom(InitialDeckSize);
        List<Minion> cards = Deck.Draw(InitialDrawSize);

        foreach (Minion card in cards)
        {
            card.transform.SetParent(Hand.transform);
        }

    }

    public async Task StartSkirmish()
    {
        InteractabilityOff();
        Debug.Log("Battle Starting!");
        PlayerMinionField.UpdateMinions();
        EnemyMinionField.UpdateMinions();
        var coinflip = Random.Range(0, 2);
        var isPlayerTurn = coinflip == 1;

        while (PlayerMinionField.LiveMinions.Any() && EnemyMinionField.LiveMinions.Any())
        {
            var attackerField = isPlayerTurn ? PlayerMinionField : EnemyMinionField;
            var defenderField = isPlayerTurn ? EnemyMinionField : PlayerMinionField;

            isPlayerTurn = !isPlayerTurn;

            var attacker = attackerField.GetAttacker();
            var liveDefenderMinions = defenderField.LiveMinions.ToList();
            var defenderIndex = Random.Range(0, liveDefenderMinions.Count);
            var defender = liveDefenderMinions[defenderIndex];

            await attacker.PerformAttack(defender);

            var delay = System.TimeSpan.FromSeconds(TimeBetweenAttacksSeconds);
            await Task.Delay(delay);
        }

        Debug.Log("Battle over");
        InteractabilityOn();
    }

    private void InteractabilityOff()
    {
        isGameInteractable = false;
        InteractionStateChange?.Invoke();
    }

    private void InteractabilityOn()
    {
        isGameInteractable = true;
        InteractionStateChange?.Invoke();
    }
}
