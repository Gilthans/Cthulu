using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Minion : MonoBehaviour
{
	public int Health;
	public int Attack;

	public float animationTimeSeconds = 1;

	private int RemainingHealth;

	private TextMeshProUGUI healthText;
	private TextMeshProUGUI remainingHealthText;
	private TextMeshProUGUI attackText;

	private Vector3 basePosition;

	private Minion shchuna;

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

	public void PerformAttack(Minion otherMinion)
	{
		shchuna = otherMinion;
		StartCoroutine(AnimateTowards(otherMinion));
	}

	IEnumerator AnimateTowards(Minion otherMinion)
	{
		var frameTime = 0.05f;
		var animationSteps = animationTimeSeconds / frameTime;
		var directionVector = otherMinion.basePosition - basePosition;

		for (var i = 0; i <= animationSteps / 2; i++)
		{
			var pctDone = i / (animationSteps / 2);
			this.transform.position = basePosition + pctDone*directionVector;
			yield return new WaitForSeconds(frameTime);
		}
		DoAttackLogic(otherMinion);

		for (var i = 0; i <= animationSteps / 2; i++)
		{
			var pctDone = i / (animationSteps / 2);
			this.transform.position = otherMinion.basePosition - pctDone * directionVector;
			yield return new WaitForSeconds(frameTime);
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
