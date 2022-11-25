using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{

    void Update()
    {
        if(!IsOwner)return;

        Vector3 moveDir = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.W))moveDir.z = +1f;
        if(Input.GetKey(KeyCode.S))moveDir.z = -1f;
        if(Input.GetKey(KeyCode.A))moveDir.x = -1f;
        if(Input.GetKey(KeyCode.D))moveDir.x = +1f;

        float moveSpeed =3f;
        if( moveDir*moveSpeed*Time.deltaTime != Vector3.zero)
        {
        transform.position +=moveDir*moveSpeed*Time.deltaTime;
        }
    }

    [ServerRpc]
    private void TestServerRpc(string message)
    {
        Debug.Log("This code is only executed on server. Message received - " + message);
    }
     [ClientRpc]
    private void TestClientRpc(string message)
    {
        Debug.Log("This code is for sending messages from the server to the client/clients. Message received - " + message);
    }
}
