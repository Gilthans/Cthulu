using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public MinionField PlayerMinionField;
    public MinionField EnemyMinionField;
    public float TimeBetweenAttacksSeconds = 1;

    public event System.Action InteractionStateChange;

    internal bool isGameInteractable = true;

    void Start()
    {
        Random.InitState(System.Guid.NewGuid().GetHashCode());
    }

    public async Task StartSkirmish()
    {
        InteractabilityOff();
        Debug.Log("Battle Starting!");
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
