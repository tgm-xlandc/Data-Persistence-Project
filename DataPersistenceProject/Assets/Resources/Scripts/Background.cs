using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.size = new Vector2(spriteRenderer.size.x,spriteRenderer.size.y + Time.deltaTime);
        if(spriteRenderer.size.y>1000)
            spriteRenderer.size = new Vector2(spriteRenderer.size.x,100.0f);
    }
}
