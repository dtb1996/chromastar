using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontManager : MonoBehaviour
{
    [SerializeField] private Sprite[] numSprites;

    public Sprite ReturnNumSprite(int index)
    {
        return numSprites[index];
    }
    
    public Sprite ReturnNumSprite(string str, int index)
    {
        return numSprites[str[index] - 1];
    }

    private Sprite ReturnSprite(int index)
    {
        Sprite sprite = null;

        //switch (index)
        //    case 0:
        //        sprite = 

        return null;
    }
}
