using UnityEngine;
using UnityEngine.UI; // For UI
using TMPro;
using System.Net.Sockets;
using System.Text;

public class NetworkManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField ipInputField;
    public Button connectButton;
    public TMP_Text statusText;

    [Header("Network Settings")]
    public int port = 9050;

    private UdpClient client;
    private string serverIP = "";

    void Start()
    {
        client = new UdpClient();

        // Load saved IP if exists
        serverIP = PlayerPrefs.GetString("PC_IP", "");
        ipInputField.text = serverIP;

        connectButton.onClick.AddListener(OnConnectClicked);
    }

    void OnConnectClicked()
    {
        serverIP = ipInputField.text.Trim();
        if (string.IsNullOrEmpty(serverIP))
        {
            statusText.text = "Invalid IP!";
            return;
        }

        // Save IP for future use
        PlayerPrefs.SetString("PC_IP", serverIP);
        statusText.text = "Connected to " + serverIP;
    }

    public void SendMessageToPC(string message)
    {
        Debug.Log(message);

        if (string.IsNullOrEmpty(serverIP)) return;

        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, serverIP, port);
        }
        catch
        {
            statusText.text = "Failed to send!";
        }
    }

    private void OnApplicationQuit()
    {
        client.Close();
    }
}
