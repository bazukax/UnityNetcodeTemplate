using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Bullet : MonoBehaviour
{
    public float speed;
    void Start()
    {
        Destroy(gameObject,15);
    }
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
         if (!NetworkManager.Singleton.IsServer)
            return;
        
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().RespawnClientRpc();
        }
        Destroy(gameObject);
    }
}
