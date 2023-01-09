using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;
public class PlayerAvatar : NetworkBehaviour
{
    [SerializeField] NetworkVariable<int> avatarId = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField] List<GameObject> avatarPrefabs;
    private float updateDuration = 0.1f;

    void Update()
    {

        if (!IsOwner) return;
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (avatarId.Value < avatarPrefabs.Count - 1)
            {
                ChangeHatId((avatarId.Value + 1));
                Invoke("UpdateavatarPrefabserverRPC", updateDuration);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (avatarId.Value > 0)
            {
                ChangeHatId(avatarId.Value - 1);
                Invoke("UpdateavatarPrefabserverRPC", updateDuration);
            }
        }
    }
    public void SetPlayerAvatar(int Value)
    {
        if (avatarId.Value < avatarPrefabs.Count - 1 && avatarId.Value > 0)
        {
            ChangeHatId((avatarId.Value));
            Invoke("UpdateavatarPrefabserverRPC", updateDuration);
        }
    }
    public void IncrementPlayerAvatar()
    {
        if (avatarId.Value < avatarPrefabs.Count - 1)
            {
                ChangeHatId((avatarId.Value + 1));
                Invoke("UpdateavatarPrefabserverRPC", updateDuration);
            }
    }
    public void DecrementPlayerAvatar()
    {
         if (avatarId.Value > 0)
            {
                ChangeHatId(avatarId.Value - 1);
                Invoke("UpdateavatarPrefabserverRPC", updateDuration);
            }
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        SyncavatarPrefabs();

        // UpdateavatarPrefabserverRPC();
        //ChangeHatClientRPC(hat);
        //avatarPrefabs[0].SetActive(true);
    }
    private void SyncavatarPrefabs()
    {
        PlayerAvatar[] players = FindObjectsOfType<PlayerAvatar>();
        foreach (PlayerAvatar avatar in players)
        {
            Debug.Log(OwnerClientId + " -hat: " + avatar.avatarId.Value);
            avatar.DisableavatarPrefabsForLateJoin();
            avatar.EnableHat(avatar.GetHatId());
        }
    }
    [ServerRpc]
    private void UpdateavatarPrefabserverRPC()
    {
        ChangeHatClientRPC();
    }

    [ClientRpc]
    private void ChangeHatClientRPC()
    {
        foreach (GameObject obj in avatarPrefabs)
        {
            obj.SetActive(false);
        }
        avatarPrefabs[avatarId.Value].SetActive(true);
    }
    public void DisableavatarPrefabsForLateJoin()
    {
        foreach (GameObject obj in avatarPrefabs)
        {
            obj.SetActive(false);
        }
    }
    public void EnableHat(int id)
    {
        avatarPrefabs[id].SetActive(true);
    }
    public int GetHatId()
    {
        return avatarId.Value;
    }
    public void ChangeHatId(int id)
    {
        if (!IsOwner) return;
        avatarId.Value = id;
    }
}
