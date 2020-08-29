using System;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Minion : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	public int Health;
	public int Attack;
	public float AnimationTimeSeconds = 1;

	private int RemainingHealth;

	private TextMeshProUGUI healthText;
	private TextMeshProUGUI remainingHealthText;
	private TextMeshProUGUI attackText;

	private Vector3 basePosition;
	private RectTransform rectTransform;
	private CanvasGroup canvasGroup;
	private Canvas canvas;

	internal bool IsAlive  => RemainingHealth > 0;

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		canvasGroup = GetComponent<CanvasGroup>();
		canvas = GetComponentInParent<Canvas>();
	}

	void Start()
	{
		var comps = this.GetComponentsInChildren<Component>();
		var texts = this.GetComponentsInChildren<TextMeshProUGUI>();
		healthText = texts.Single(t => t.name == "Health");
		remainingHealthText = texts.Single(t => t.name == "RemainingHealth");
		attackText = texts.Single(t => t.name == "Attack");
		basePosition = rectTransform.anchoredPosition;

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
			rectTransform.anchoredPosition = basePosition + pctDone * directionVector;
			await Task.Delay(frameTime);
		}

		DoAttackLogic(otherMinion);

		for (var i = 0; i <= animationSteps / 2; i++)
		{
			var pctDone = i / (animationSteps / 2.0f);
			rectTransform.anchoredPosition = otherMinion.basePosition - pctDone * directionVector;
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

	Transform dragStartParent;
	public void OnBeginDrag(PointerEventData eventData)
	{
		// TODO: Disable when the game is not interactable
		canvasGroup.alpha = 0.6f;
		canvasGroup.blocksRaycasts = false;
		dragStartParent = transform.parent;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		canvasGroup.alpha = 1f;
		canvasGroup.blocksRaycasts = true;
		if (dragStartParent == transform.parent)
		{
			Debug.Log(basePosition);
			rectTransform.anchoredPosition = basePosition;
		}
		else
		{
			basePosition = rectTransform.anchoredPosition;
			Debug.Log(basePosition);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
	}
}
