using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using TMPro;


[RequireComponent(typeof(CharacterController))]

public class PlayerController : NetworkBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public GameObject playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public GameObject bulletPrefab;
    public TMP_Text playerNameText;


    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        if(!IsOwner)return;
        playerCamera.SetActive(true);
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        if(SceneManager.GetActiveScene().buildIndex != 3)return;
        //Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;
    }


    void Update()
    {
        if(!IsOwner)return;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.None)
            {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            }else
            {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            }

        }
         if (Input.GetKeyDown(KeyCode.E)) ShootServerRPC();
        if(  playerCamera.activeSelf == false)  playerCamera.SetActive(true);
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    [ServerRpc]
    private void ShootServerRPC()
    {

           GameObject m_MyBullet = Instantiate(bulletPrefab, transform.position + transform.forward *2 + transform.right + Vector3.up, Quaternion.identity);
           m_MyBullet.transform.rotation = transform.rotation;
           m_MyBullet.GetComponent<NetworkObject>().Spawn();

            m_MyBullet = Instantiate(bulletPrefab, transform.position + transform.forward *2 - transform.right + Vector3.up, Quaternion.identity);
           m_MyBullet.transform.rotation = transform.rotation;
           m_MyBullet.GetComponent<NetworkObject>().Spawn();
    }

    [ClientRpc]
    public void RespawnClientRpc()
    {
        if(!IsOwner)return;
        characterController.Move( new Vector3(Random.Range(-10,10),5,Random.Range(-10,10)));
        Debug.Log("CLIENTRPC");
    }
   // [ServerRpc]
    public void Respawn()
    {
        RespawnClientRpc();
       // RespawnServerRpc();
    }

    [ServerRpc]
    private void UpdateNameServerRPC(ulong clientid)
    {
        UpdateNameClientRPC(clientid);
    }
    [ClientRpc]
    private void UpdateNameClientRPC(ulong clientid)
    {
           // if(!IsOwner)return;
       playerNameText.text = "Player " + clientid;

    }

}