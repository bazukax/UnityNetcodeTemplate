using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class PlayerOverheadDisplay : NetworkBehaviour
{
     public TMP_Text playerNameText;
    
    private Dictionary<ulong, bool> m_ClientsInLobby;
    private string m_UserLobbyStatusText;
    private NetworkVariable<FixedString32Bytes> playerName = new NetworkVariable<FixedString32Bytes>();
    private NetworkVariable<ulong> playerId = new NetworkVariable<ulong>();
    bool OverHeadSet = false;

    public override void OnNetworkSpawn()
    {
       playerId.Value = OwnerClientId;
       playerNameText.text =  playerId.Value.ToString();
    }
    public ulong GetPlayerId()
    {
      return playerId.Value;
    }
}
