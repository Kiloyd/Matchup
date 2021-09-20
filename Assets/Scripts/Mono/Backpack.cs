using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack: MonoBehaviour
{
    public static Backpack b;

    [SerializeField]
    private ItemSpriteMap spriteMap;
    [SerializeField]
    private ItemDescriptionMap descriptionMap;

    [Header("Item backpack")]
    [SerializeField]
    private Image[] itemSprites;
    [SerializeField]
    private Text[] itemAmountText;
    [SerializeField]
    private RectTransform bg;
    [SerializeField]
    private RectTransform upperBg;

    [Header("Item description")]
    [SerializeField]
    private RectTransform descriptionTrans;
    [SerializeField]
    private Image clickedItemSprite;
    [SerializeField]
    private Text clickedItemAmountText;
    [SerializeField]
    private Text clickedItemTitle;
    [SerializeField]
    private Text clickedItemDescription;

    private int[] itemAmount;
    private List<int> nonZeroItemIndex;
    private List<int> nonZeroItemAmount;
    private int descriptionTargetIndex;

    private void Start()
    {
        if (b == null)
            b = this;
        else
        {
            Destroy(this);
            return;
        }
        descriptionTargetIndex = -1;
        nonZeroItemIndex = new List<int>();
        nonZeroItemAmount = new List<int>();
    }

    public void syncItemAmount()
    {
        itemAmount = new int[Player.p.status.itemAmount.Length];
        
        if (nonZeroItemIndex != null)
            nonZeroItemIndex.Clear();
        if (nonZeroItemAmount != null)
            nonZeroItemAmount.Clear();

        Player.p.status.itemAmount.CopyTo(itemAmount, 0);

        for (int i = 0; i < itemAmount.Length; ++i)
        {
            if (itemAmount[i] > 0)
            {
                nonZeroItemIndex.Add(i);
                nonZeroItemAmount.Add(itemAmount[i]);
            }
        }
    }

    public void syncBackpackUIDisplay()
    {
        for(int i = 0; i < nonZeroItemIndex.Count; ++i)
        {
            itemSprites[i].transform.parent.gameObject.SetActive(true);
            itemSprites[i].sprite = spriteMap.itemSpriteMap[nonZeroItemIndex[i]];
            itemAmountText[i].text = nonZeroItemAmount[i].ToString();
        }

        for (int i = nonZeroItemIndex.Count; i < itemSprites.Length; ++i)
        {
            itemSprites[i].transform.parent.gameObject.SetActive(false);
        }
        upperBg.sizeDelta = new Vector2(upperBg.rect.width, 85 + (int)((nonZeroItemIndex.Count - 1) / 3) * 80);
        bg.sizeDelta = new Vector2(bg.rect.width, 150 + (int)((nonZeroItemIndex.Count - 1) / 3) * 80);

        descriptionTrans.anchoredPosition = new Vector2(descriptionTrans.anchoredPosition.x, -140 - (int)((nonZeroItemIndex.Count - 1) / 3) * 80);
    }

    public void syncDescriptionByImage(Image image)
    {
        //descriptionTrans.anchoredPosition = new Vector2(descriptionTrans.anchoredPosition.x, -140 - (int)((nonZeroItemIndex.Count - 1) / 3) * 80);
        
        for (int i = 0; i < spriteMap.itemSpriteMap.Length; ++i)
        {
            if (image.sprite == spriteMap.itemSpriteMap[i])
            {
                descriptionTargetIndex = i;
                break;
            }
        }

        if (descriptionTargetIndex == -1)
        {
            Debug.Log("target cannot found in sprite map");
            return;
        }

        clickedItemSprite.sprite = image.sprite;
        clickedItemTitle.text = descriptionMap.titleMap[descriptionTargetIndex];
        clickedItemAmountText.text = itemAmount[descriptionTargetIndex].ToString();
        clickedItemDescription.text = descriptionMap.descriptionMap[descriptionTargetIndex];
    }

    public void syncDescriptionBySprite(Sprite sprite)
    {
        //descriptionTrans.anchoredPosition = new Vector2(descriptionTrans.anchoredPosition.x, -140 - (int)((nonZeroItemIndex.Count - 1) / 3) * 80);

        for (int i = 0; i < spriteMap.itemSpriteMap.Length; ++i)
        {
            if (sprite == spriteMap.itemSpriteMap[i])
            {
                descriptionTargetIndex = i;
                break;
            }
        }

        if (descriptionTargetIndex == -1)
        {
            Debug.Log("target cannot found in sprite map");
            return;
        }

        clickedItemSprite.sprite = sprite;
        clickedItemTitle.text = descriptionMap.titleMap[descriptionTargetIndex];
        clickedItemAmountText.text = itemAmount[descriptionTargetIndex].ToString();
        clickedItemDescription.text = descriptionMap.descriptionMap[descriptionTargetIndex];
    }

    public void useItem()
    {
        if (descriptionTargetIndex == -1)
        {
            Debug.Log("target item not assign");
            return;
        }
        else
        {
            Player.p.status.updateItemAmount_consume((ItemEnum)descriptionTargetIndex);
            syncItemAmount();
            if (itemAmount[descriptionTargetIndex] <= 0)
            {
                descriptionTrans.gameObject.SetActive(false);
                descriptionTargetIndex = -1;
            }
        }
    }
}
