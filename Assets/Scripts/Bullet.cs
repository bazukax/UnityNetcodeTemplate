using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
    public float speed;
    void Start()
    {
        Invoke("DespawnBullet",15);
    }
    void DespawnBullet()
    {
        if(IsServer)GetComponent<NetworkObject>().Despawn();
    }
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
        /*
         if (!NetworkManager.Singleton.IsServer)
            return;
        */
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Respawn();
            //collision.gameObject.transform.position = new Vector3(Random.Range(-10,10),5,Random.Range(-10,10));
        }
        if(IsServer)GetComponent<NetworkObject>().Despawn();
    }
}
