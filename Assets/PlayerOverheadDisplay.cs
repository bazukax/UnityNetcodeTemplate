using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class PlayerOverheadDisplay : NetworkBehaviour
{
     public TMP_Text playerNameText;
    
    private Dictionary<ulong, bool> m_ClientsInLobby;
    private string m_UserLobbyStatusText;
     public override void OnNetworkSpawn()
    {
         m_ClientsInLobby = new Dictionary<ulong, bool>();
        
        //Always add ourselves to the list at first
        m_ClientsInLobby.Add(NetworkManager.LocalClientId, false);

        //If we are hosting, then handle the server side for detecting when clients have connected
        //and when their lobby scenes are finished loading.

        //Update our lobby
        GenerateUserStatsForLobby();

        SceneTransitionHandler.sceneTransitionHandler.SetSceneState(SceneTransitionHandler.SceneStates.Lobby);
    }
       private void GenerateUserStatsForLobby()
    {
        m_UserLobbyStatusText = string.Empty;
        foreach (var clientLobbyStatus in m_ClientsInLobby)
        {
            playerNameText.text = "PLAYER_" + clientLobbyStatus.Key + "          ";
        }
    }
}
