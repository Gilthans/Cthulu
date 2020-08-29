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

	private int RemainingHealth;

	private TextMeshProUGUI healthText;
	private TextMeshProUGUI remainingHealthText;
	private TextMeshProUGUI attackText;

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
		otherMinion.RemainingHealth = Math.Max(0, otherMinion.RemainingHealth - Attack);
		RemainingHealth = Math.Max(0, RemainingHealth - otherMinion.Attack);
		UpdateStatus();
		otherMinion.UpdateStatus();
	}

	void Update()
	{

	}
}
