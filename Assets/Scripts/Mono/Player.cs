using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Property

    [SerializeField]
    private InventoryDataSetting inventorySetting;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float fallingSpeed;
    [SerializeField]
    private float JumpPower;
    [SerializeField]
    private Collider2D detectArea;

    [SerializeField]
    private PlayerStatus status;
    private PlayerController playerController;

    #endregion

    private void Start()
    {
        detectArea = GetComponent<Collider2D>();

        if (inventorySetting != null)
            status = new PlayerStatus(inventorySetting, detectArea);
        else
            status = new PlayerStatus(ScriptableObject.CreateInstance<InventoryDataSetting>(), gameObject.AddComponent<Collider2D>());

        playerController = new PlayerController(playerSpeed, fallingSpeed, JumpPower, GetComponent<Rigidbody2D>(), GetComponent<Collider2D>());

        //Debug.Log(status.inventoryData.itemAmount[1]);
    }

    private void FixedUpdate()
    {
        playerController.Moving(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), status.isInAir, status.isJumpRelease);
        status.updateStatus();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "item")
        {
            Debug.Log(transform.name + " touch " + collision.name);
            status.updateItemAmount_pick(collision.GetComponent<Item>().itemType);
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "finish")
        {
            Debug.Log("reach finish line!");
            collision.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().gameOver();
        }

    }
}
