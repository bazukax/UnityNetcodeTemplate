using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AvatarChanger : NetworkBehaviour
{
    public void ChangeAvatar(int avatarValue)
    {
        FindPlayer().gameObject.GetComponentInChildren<PlayerAvatar>().SetPlayerAvatar(avatarValue);
    }
    public void IncrementAvatar()
    {

        FindPlayer().gameObject.GetComponentInChildren<PlayerAvatar>().IncrementPlayerAvatar();
    }
    public void DecrementAvatar()
    {
        FindPlayer().gameObject.GetComponentInChildren<PlayerAvatar>().DecrementPlayerAvatar();
    }
    public GameObject FindPlayer()
    {
        return NetworkManager.LocalClient.PlayerObject.gameObject;
    }
}
