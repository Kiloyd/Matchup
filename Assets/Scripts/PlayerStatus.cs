using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerStatus
{
    #region Property

    public InventoryDataSetting inventoryData { get; }
    public Collider2D detectCollider;
    public int[] itemAmount;
    public bool isInAir;
    public bool isJumpRelease;

    #endregion

    #region Public function

    public PlayerStatus(InventoryDataSetting setting, Collider2D collider)
    {
        inventoryData = setting;
        detectCollider = collider;

        itemAmount = new int[setting.itemAmount.Length];
        setting.itemAmount.CopyTo(itemAmount, 0);

        isInAir = false;
        isJumpRelease = true;
    }

    public void updateStatus()
    {
        if (detectCollider.IsTouchingLayers(LayerMask.GetMask("floor")))
            isInAir = false;
        else
            isInAir = true;
        
        if (Input.GetAxis("Jump") <= 0.01)
            isJumpRelease = true;
        else
            isJumpRelease = false;
    }

    public void updateItemAmount_pick(ItemEnum itemNum)
    {
        itemAmount[(int)itemNum] += 1;
    }

    #endregion
}
