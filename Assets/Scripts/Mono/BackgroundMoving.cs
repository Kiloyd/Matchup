using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{
    [SerializeField]
    private Material m;
    [SerializeField][Range(0, 1)]
    private float backgroundScrollingFactor;

    private float counting;
    
    private void Start()
    {
        m = GetComponent<SpriteRenderer>().material;
    }
    void Update()
    {
        if (counting >= 1)
            counting = 0;
        else
            counting += Time.deltaTime * backgroundScrollingFactor;

        m.SetTextureOffset("_MainTex", new Vector2(counting, 0));
    }
}
