using UnityEngine;
using UnityEngine.EventSystems;

public class UIDraggable : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private ButtonKey buttonKey;

    private bool isDraggable = false; // Toggle this externally


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        buttonKey = GetComponent<ButtonKey>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isDraggable) return;
        rectTransform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        buttonKey.enabled = false;
        if (!isDraggable || canvas == null) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void IsDraggable(bool newState)
    {
        isDraggable = newState;
        buttonKey.enabled = !newState;
    }
}
