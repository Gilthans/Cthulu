using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHand : MonoBehaviour, IDropHandler
{
	public float maxCardDistance = 40;
	public float handMargins = 40;

	public void Start()
	{
		ReorderHand();
	}

	public void OnDrop(PointerEventData eventData)
	{
		var minion = eventData.pointerDrag;
		minion.transform.SetParent(transform);
		minion.GetComponent<RectTransform>().localPosition = GetComponent<RectTransform>().localPosition;
		ReorderHand();
	}

	private void ReorderHand()
	{
		var allCards = GetComponentsInChildren<Minion>().Select(m => m.gameObject).ToList();
		var handWidth = GetComponent<RectTransform>().rect.width - 2 * handMargins;
		var cardSize = allCards[0].GetComponent<RectTransform>().rect.width;

		if (cardSize * allCards.Count + maxCardDistance * (allCards.Count - 1) <= handWidth)
		{
			// Easy case - just place them one after the other.
			var currentHandSize = cardSize * allCards.Count + maxCardDistance * (allCards.Count - 1);

			for (var i = 0; i < allCards.Count; i++)
			{
				var card = allCards[i];
				var list = new List<string>();
				var requiredX = cardSize / 2.0f + i * (cardSize + maxCardDistance);
				card.GetComponent<RectTransform>().anchoredPosition = new Vector2((requiredX - currentHandSize / 2.0f), 0);
			}
		}
		else
		{
			throw new NotImplementedException();
		}
	}
}
