using UnityEngine;
using UnityEngine.EventSystems; // Needed for pointer events

public class ButtonKey : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string keyToSend = "Space"; // Key this button emulates

    private NetworkManager networkManager;

    private void Awake()
    {
        networkManager = FindAnyObjectByType<NetworkManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        networkManager.SendMessageToPC("KeyDown:" + keyToSend);
        //SendKeyDown(keyToSend);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        networkManager.SendMessageToPC("KeyUp:" + keyToSend);
        //SendKeyUp(keyToSend);
    }

    void SendKeyDown(string key)
    {
        // TODO: send key down to PC receiver
        Debug.Log("Key Down: " + key);
        
    }

    void SendKeyUp(string key)
    {
        // TODO: send key up to PC receiver
        Debug.Log("Key Up: " + key);
      // networkManager.SendMessageToPC("MouseDown:" + key);
    }
}
