using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Minion : MonoBehaviour
{
	public int Health;
	public int Attack;
	public float AnimationTimeSeconds = 1;

	private int RemainingHealth;

	private TextMeshProUGUI healthText;
	private TextMeshProUGUI remainingHealthText;
	private TextMeshProUGUI attackText;

	private Vector3 basePosition;

	internal bool IsAlive()
	{
		return RemainingHealth > 0;
	}

	void Start()
	{
		var comps = this.GetComponentsInChildren<Component>();
		var texts = this.GetComponentsInChildren<TextMeshProUGUI>();
		healthText = texts.Single(t => t.name == "Health");
		remainingHealthText = texts.Single(t => t.name == "RemainingHealth");
		attackText = texts.Single(t => t.name == "Attack");
		basePosition = transform.position;

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
		var directionVector = otherMinion.basePosition - basePosition;

		for (var i = 0; i <= animationSteps / 2; i++)
		{
			var pctDone = i / (animationSteps / 2.0f);
			transform.position = basePosition + pctDone * directionVector;
			await Task.Delay(frameTime);
		}

		DoAttackLogic(otherMinion);

		for (var i = 0; i <= animationSteps / 2; i++)
		{
			var pctDone = i / (animationSteps / 2.0f);
			transform.position = otherMinion.basePosition - pctDone * directionVector;
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

	void Update()
	{

	}
}
