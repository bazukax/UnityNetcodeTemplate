using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;
using TMPro;
public class PlayerHud : NetworkBehaviour
{
    private NetworkVariable<NetworkString> playerName = new NetworkVariable<NetworkString>();
    private bool overlaySet = false;
    public TMP_Text playerNameText;
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            playerName.Value = $"Player_{OwnerClientId}";
        }
    }
    public void SetOverlay()
    {
        playerNameText.text = playerName.Value;
    }
    private void Update()
    {
        if(!overlaySet &&!string.IsNullOrEmpty(playerName.Value))
        {
            SetOverlay();
            overlaySet =true;
        }
    }

}

public struct NetworkString : INetworkSerializable
{
    private FixedString32Bytes info;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref info);
    }
    public override string ToString()
    {
        return info.ToString();
    }
    public static implicit operator string(NetworkString s) => s.ToString();
    public static implicit operator NetworkString(string s) => new NetworkString() { info = new FixedString32Bytes() };

}