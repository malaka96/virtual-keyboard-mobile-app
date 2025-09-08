using UnityEngine;
using UnityEngine.UI;

public class DragToggleManager : MonoBehaviour
{
    public Toggle dragToggle; // Assign in Inspector
    public UIDraggable[] draggableElements; // Assign all draggable UI elements

    void Start()
    {
        dragToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    public void OnToggleChanged(bool isOn)
    {
        foreach (var element in draggableElements)
        {
            element.isDraggable = isOn;
        }
    }
}
