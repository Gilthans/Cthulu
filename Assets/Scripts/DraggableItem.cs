using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DraggableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	Vector3 startPosition;
	Transform startParent;
	RectTransform rectTransform;
	Canvas canvas;

	//public void OnBeginDrag(PointerEventData eventData)
	//{
	//	startPosition = transform.position;
	//	startParent = transform.parent;
	//	GetComponent<CanvasGroup>().blocksRaycasts = false;
	//	canvas = GameObject.FindGameObjectWithTag("FloatingCanvas").GetComponent<Canvas>();
	//	transform.parent = canvas.transform;
	//}

	//public void OnDrag(PointerEventData eventData)
	//{
	//	transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
	//}

	//public void OnEndDrag(PointerEventData eventData)
	//{
	//	GetComponent<CanvasGroup>().blocksRaycasts = true;
	//	if (transform.parent == canvas.transform)
	//	{
	//		transform.position = startPosition;
	//		transform.parent = startParent;
	//	}
	//}

	public void OnBeginDrag(PointerEventData eventData)
	{
		// TODO: Disable when the game is not interactable
		rectTransform = GetComponent<RectTransform>();
		startPosition = rectTransform.anchoredPosition;
		startParent = transform.parent;
		var canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 0.6f;
		canvasGroup.blocksRaycasts = false;
		canvas = GameObject.FindGameObjectWithTag("FloatingCanvas").GetComponent<Canvas>();
		rectTransform.SetParent(canvas.transform);
	}

	public void OnDrag(PointerEventData eventData)
	{
		rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		var canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha = 1f;
		canvasGroup.blocksRaycasts = true;
		if (canvas.transform == transform.parent)
		{
			rectTransform.SetParent(startParent);
			rectTransform.anchoredPosition = startPosition;
		}
	}
}