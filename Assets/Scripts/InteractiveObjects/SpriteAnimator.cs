using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public Sprite[] animSprites;
    public float fps = 10f;
    public bool shouldLoop;

    private int frameCounter = 0;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //frameCounter = animSprites.Length;
        if (animSprites.Length > 0)
            StartCoroutine("PlayAnimation");
    }

    private IEnumerator PlayAnimation()
    {
        if (frameCounter > animSprites.Length - 1f)
        {
            frameCounter = 0;
        }

        spriteRenderer.sprite = animSprites[frameCounter];
        frameCounter++;

        yield return new WaitForSeconds(1f / fps);

        //Determines whether the animation should loop, if not, destroy when animation is finished
        if (shouldLoop || frameCounter < animSprites.Length - 1f)
        {
            StartCoroutine("PlayAnimation");
        }
        else if (!shouldLoop && frameCounter == animSprites.Length - 1f)
        { 
            Destroy(gameObject);
        }
    }
}
