using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryDataSetting : ScriptableObject
{
    // with ItemEnum as index to read the amount of each item.
    public int[] itemAmount = new int[9];
}
