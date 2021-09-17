using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{

    public ItemEnum itemType;
    [SerializeField]
    private ItemSpriteMap spriteMap;

    private SpriteRenderer itemSpriteRenderer;

    private void Start()
    {
        gameObject.tag = "item";
        itemSpriteRenderer = GetComponent<SpriteRenderer>();
        itemSpriteRenderer.sprite = spriteMap.itemSpriteMap[(int)itemType];
    }
}
