using UnityEngine;

public class JoystickMouse : MonoBehaviour
{
    public bl_Joystick joystick;
    public float sensitivity = 10f;

    private NetworkManager networkManager;

    private void Awake()
    {
        networkManager = FindAnyObjectByType<NetworkManager>();
    }

    void Update()
    {
        float moveX = joystick.Horizontal * sensitivity;
        float moveY = joystick.Vertical * sensitivity;

        if (Mathf.Abs(moveX) > 0.01f || Mathf.Abs(moveY) > 0.01f)
        {
            // Send joystick movement as mouse delta to PC
            SendMouseMove(moveX, moveY);
        }
    }

    void SendMouseMove(float x, float y)
    {
        // TODO: Replace with network send code

       // Debug.Log();
        networkManager.SendMessageToPC($"Mouse Move: {x}, {y}");
    }
}
