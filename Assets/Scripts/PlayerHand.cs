using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHand : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var minion = eventData.pointerDrag;
        minion.transform.SetParent(transform);
        minion.GetComponent<RectTransform>().localPosition = GetComponent<RectTransform>().localPosition;
        // TODO: Reorder cards in hand
    }
}
