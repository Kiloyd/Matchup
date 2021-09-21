using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Property

    public static Player p;
    [SerializeField]
    private InventoryDataSetting inventorySetting;
    [SerializeField]
    private ItemSpriteMap itemSpriteMap;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float fallingSpeed;
    [SerializeField]
    private float JumpPower;
    [SerializeField]
    private Collider2D detectArea;

    public PlayerStatus status;
    public PlayerController playerController;

    #endregion

    private void Start()
    {
        if (p == null)
            p = this;
        else
        {
            Destroy(this);
            return;
        }

        playerReset();
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

            Backpack.b.syncItemAmount();
            Backpack.b.syncBackpackUIDisplay();
            Backpack.b.syncDescriptionBySprite(itemSpriteMap.itemSpriteMap[(int)collision.GetComponent<Item>().itemType]);
            
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "finish")
        {
            Debug.Log("reach finish line!");
            collision.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().gameOver();
        }
    }

    public void playerReset()
    {
        detectArea = GetComponent<Collider2D>();

        if (inventorySetting != null)
            status = new PlayerStatus(inventorySetting, detectArea);
        else
            status = new PlayerStatus(ScriptableObject.CreateInstance<InventoryDataSetting>(), gameObject.AddComponent<Collider2D>());

        playerController = new PlayerController(playerSpeed, fallingSpeed, JumpPower, GetComponent<Rigidbody2D>(), GetComponent<Collider2D>(), GetComponent<Animator>());
    }
}
