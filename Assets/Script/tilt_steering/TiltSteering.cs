using TMPro;
using UnityEngine;

public class TiltSteering : MonoBehaviour
{
    [Header("Tilt Settings")]
    public float deadzone = 0.1f;        // Ignore small tilts
    public float smoothSpeed = 5f;       // Smooth the tilt input

    private float tiltX = 0f;
    //private float smoothedTilt = 0f;

    private bool isAPressed = false;
    private bool isDPressed = false;

    public TMP_Text value;

    private NetworkManager networkManager;

    private void Awake()
    {
        networkManager = FindAnyObjectByType<NetworkManager>();
    }

    void Update()
    {
        //float tiltX = 0f;
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A)) tiltX = -1f;
        else if (Input.GetKey(KeyCode.D)) tiltX = 1f;
        else tiltX = 0f;
#else
    
#endif
        tiltX = Input.acceleration.x;

        float deadzone = 0.1f;
        if (Mathf.Abs(tiltX) < deadzone) tiltX = 0f;

        value.text = tiltX.ToString();

        // Tilt left
        if (tiltX < -deadzone)
        {
            if (!isAPressed) // only log/send when state changes
            {
               // SendKeyDown("A");
                //Debug.Log("Key Down: A");
                networkManager.SendMessageToPC("KeyDown:A");
                isAPressed = true;
            }
            if (isDPressed)
            {
               // SendKeyUp("D");
                networkManager.SendMessageToPC("KeyUp:D");
                isDPressed = false;
            }
        }
        // Tilt right
        else if (tiltX > deadzone)
        {
            if (!isDPressed)
            {
               // SendKeyDown("D");
               // Debug.Log("Key Down: D");
                networkManager.SendMessageToPC("KeyDown:D");
                isDPressed = true;
            }
            if (isAPressed)
            {
                //SendKeyUp("A");
                networkManager.SendMessageToPC("KeyUp:A");
                isAPressed = false;
            }
        }
        // Neutral
        else
        {
            if (isAPressed)
            {
               // SendKeyUp("A");
               // Debug.Log("Key Up: A");
               networkManager.SendMessageToPC("KeyUp:A");
                isAPressed = false;
            }
            if (isDPressed)
            {
               // SendKeyUp("D");
              //  Debug.Log("Key Up: D");
                networkManager.SendMessageToPC("KeyUp:D");
                isDPressed = false;
            }
        }
    }

    // Placeholder functions to send key events (to implement your PC communication)
    void SendKeyDown(string key)
    {
        // TODO: send "key down" event to PC receiver
        Debug.Log("Key Down: " + key);
    }

    void SendKeyUp(string key)
    {
        // TODO: send "key up" event to PC receiver
        Debug.Log("Key Up: " + key);
    }
}
