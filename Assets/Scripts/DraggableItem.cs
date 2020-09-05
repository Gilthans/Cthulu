using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	Vector3 startPosition;
	Transform startParent = null;
	RectTransform draggableRectTransform;
	Canvas floatingCanvas;

	protected abstract bool IsDraggable { get; }

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!IsDraggable) {
			eventData.pointerDrag = null;
			return;
		}

		// TODO: Disable when the game is not interactable
		draggableRectTransform = GetComponent<RectTransform>();
		floatingCanvas = GameObject.FindGameObjectWithTag("FloatingCanvas").GetComponent<Canvas>();

		startPosition = draggableRectTransform.anchoredPosition;
		startParent = transform.parent;

		var canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0.6f;
		canvasGroup.blocksRaycasts = false;

		draggableRectTransform.SetParent(floatingCanvas.transform);
	}

	public void OnDrag(PointerEventData eventData)
	{
		draggableRectTransform.anchoredPosition += eventData.delta / floatingCanvas.scaleFactor;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		var canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1f;
		canvasGroup.blocksRaycasts = true;
		if (floatingCanvas.transform == transform.parent)
		{
			draggableRectTransform.SetParent(startParent);
			draggableRectTransform.anchoredPosition = startPosition;
		}
	}
}