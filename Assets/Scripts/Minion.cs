using System;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Minion : DraggableItem
{
	public GameMaster GameMaster;

	public int Health;
	public int Attack;
	public float AnimationTimeSeconds = 1;

	private int RemainingHealth;

	private TextMeshProUGUI healthText;
	private TextMeshProUGUI remainingHealthText;
	private TextMeshProUGUI attackText;

	private RectTransform rectTransform;

	internal bool IsAlive  => RemainingHealth > 0;

	public bool IsPlayerCard => GetComponentInParent<MinionField>()?.name == "Player" || GetComponentInParent<PlayerHand>() != null;

	protected override bool IsDraggable => GameMaster.isGameInteractable && IsPlayerCard;


	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	void Start()
	{
		var comps = GetComponentsInChildren<Component>();
		var texts = GetComponentsInChildren<TextMeshProUGUI>();
		healthText = texts.Single(t => t.name == "Health");
		remainingHealthText = texts.Single(t => t.name == "RemainingHealth");
		attackText = texts.Single(t => t.name == "Attack");

		RemainingHealth = Health;

		UpdateStatus();
	}

	private void UpdateStatus()
	{
		healthText.text = Health.ToString();
		remainingHealthText.text = RemainingHealth.ToString();
		attackText.text = Attack.ToString();
	}

	public async Task PerformAttack(Minion otherMinion)
	{
		var frameTime = TimeSpan.FromMilliseconds(50);
		var animationSteps = TimeSpan.FromSeconds(AnimationTimeSeconds).Ticks / frameTime.Ticks;
		Debug.Log($"Animation steps: {animationSteps}");
		var directionVector = otherMinion.rectTransform.anchoredPosition - rectTransform.anchoredPosition;

		for (var i = 0; i <= animationSteps / 2; i++)
		{
			var pctDone = i / (animationSteps / 2.0f);
			rectTransform.anchoredPosition = rectTransform.anchoredPosition + pctDone * directionVector;
			await Task.Delay(frameTime);
		}

		DoAttackLogic(otherMinion);

		for (var i = 0; i <= animationSteps / 2; i++)
		{
			var pctDone = i / (animationSteps / 2.0f);
			rectTransform.anchoredPosition = otherMinion.rectTransform.anchoredPosition - pctDone * directionVector;
			await Task.Delay(frameTime);
		}
	}

	private void DoAttackLogic(Minion otherMinion)
	{
		otherMinion.RemainingHealth = Math.Max(0, otherMinion.RemainingHealth - Attack);
		RemainingHealth = Math.Max(0, RemainingHealth - otherMinion.Attack);
		UpdateStatus();
		otherMinion.UpdateStatus();
	}
}
