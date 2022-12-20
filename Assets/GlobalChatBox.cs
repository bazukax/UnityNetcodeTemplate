using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class GlobalChatBox : NetworkBehaviour
{

    [SerializeField]
    TMP_Text chatbox;
    [SerializeField]
    TMP_InputField inputField;



    public void SendText()
    {
         RequestChatboxUpdateServerRPC(inputField.text);
    }

    [ServerRpc(RequireOwnership =false)]
     public void RequestChatboxUpdateServerRPC(string str)
    {
        SendMessageClientRPC(str);
    }
    [ClientRpc]
    public void SendMessageClientRPC(string text)
    {
        chatbox.text += "\n" + text;
    }
}
