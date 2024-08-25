using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

[RequireComponent(typeof(NetworkManager))]
[RequireComponent(typeof(UnityTransport))]
public class NetcodeHUD : MonoBehaviour
{
    private GUIStyle buttonStyle;
    private GUIStyle inputFieldStyle;
    private UnityTransport transport;
    private NetworkManager manager;

    private void Awake()
    {
        transport = GetComponent<UnityTransport>();
        manager = GetComponent<NetworkManager>();
    }

    private string IP
    {
        get
        {
            return transport.ConnectionData.Address;
        }
        set
        {
            transport.ConnectionData.Address = value;
        }
    }

    private void OnGUI()
    {
        if (buttonStyle == null)
        {
            buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 28;
        }
        if (inputFieldStyle == null)
        {
            inputFieldStyle = new GUIStyle(GUI.skin.textField);
            inputFieldStyle.fontSize = 28;
            inputFieldStyle.alignment = TextAnchor.MiddleCenter;
        }
        if (GUI.Button(new Rect(5, 5, 400, 80), "LAN Host", buttonStyle))
        {
            manager.StartHost();
        }
        if (GUI.Button(new Rect(5, 90, 150, 80), "LAN Client", buttonStyle))
        {
            manager.StartClient();
        }
        IP = GUI.TextField(new Rect(155, 90, 250, 80), IP, inputFieldStyle);
        if (GUI.Button(new Rect(5, 175, 400, 80), "LAN Server", buttonStyle))
        {
            manager.StartServer();
        }
    }
}
