using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.Sockets;
using System.Text;
using System.Net;

public class NetworkManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField ipInputField;
    public Button connectButton;
    public Button disconnectButton; // New button
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
        disconnectButton.onClick.AddListener(OnDisconnectClicked); // Hook up disconnect
    }

    void OnConnectClicked()
    {
        serverIP = ipInputField.text.Trim();

        // Validate IP format
        if (!IPAddress.TryParse(serverIP, out _))
        {
            statusText.text = "Invalid IP format!";
            return;
        }

        PlayerPrefs.SetString("PC_IP", serverIP);
        statusText.text = "Connected to " + serverIP;
    }

    void OnDisconnectClicked()
    {
        serverIP = "";
        //PlayerPrefs.DeleteKey("PC_IP");
        statusText.text = "Disconnected";
        //ipInputField.text = "";

        // Optional: close and reopen client to reset state
        client.Close();
        client = new UdpClient();
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
