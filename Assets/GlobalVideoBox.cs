using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using UnityEngine.Video;
public class GlobalVideoBox : NetworkBehaviour
{

    [SerializeField]
    TMP_InputField inputField;
    [SerializeField]
    VideoPlayer videoPlayer;

     public void UpdateURL()
    {
         RequestURLUpdateServerRPC(inputField.text);
    }

    [ServerRpc(RequireOwnership =false)]
      void RequestURLUpdateServerRPC(string url)
    {
        SendURLClientRPC(url);
    }
    [ClientRpc]
     void SendURLClientRPC(string url)
    {
        videoPlayer.url = url;
        videoPlayer.Play();
    }
}
