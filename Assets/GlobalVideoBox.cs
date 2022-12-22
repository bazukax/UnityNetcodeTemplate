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

    private NetworkVariable<int> videoPlayerFrame = new NetworkVariable<int>();
  

     public void UpdateURL()
    {
         RequestURLUpdateServerRPC(inputField.text);
    }
    public override void OnNetworkSpawn()
    {
       SyncVideoClientRpc();
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

    [ClientRpc]
    void SyncVideoClientRpc()
    {
        if (IsServer)
       {
        videoPlayerFrame.Value = (int)videoPlayer.frame;
       }
        videoPlayer.frame = videoPlayerFrame.Value;
    }
    public void SyncTime()
    {
        SyncVideoClientRpc();
    }
}
