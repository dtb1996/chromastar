using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] Image digit0;
    [SerializeField] Image digit1;
    [SerializeField] Image digit2;

    public FontManager fontManager;

    private int testCount = 0;

    private void Start()
    {
        StartCoroutine("IncreaseCount");
    }

    private IEnumerator IncreaseCount()
    {
        Debug.Log("Updating count");
        //UpdateCount(testCount.ToString());
        UpdateCount(testCount);

        yield return new WaitForSeconds(0.1f);

        testCount++;
        Debug.Log("Increasing count");
        StartCoroutine("IncreaseCount");
    }

    public void UpdateCount(int newCount)
    {
        //Debug.Log("Value of newCount: " + newCount);
        //Debug.Log("Char at index 0: " + newCount[0]);
        //Debug.Log("Length of newCount string: " + newCount.Length);

        Debug.Log("Value of newCount: " + newCount);

        if (newCount < 10)
        {
            digit0.sprite = fontManager.ReturnNumSprite(0);
            digit1.sprite = fontManager.ReturnNumSprite(newCount);
        }
        else if (newCount >= 10)
        {
            digit0.sprite = fontManager.ReturnNumSprite((newCount / 10) % 10);
            digit1.sprite = fontManager.ReturnNumSprite(newCount % 10);
        }


        return;

        //switch (newCount.Length)
        //{
        //    case 0:
        //        digit0.sprite = fontManager.ReturnNumSprite(0);
        //        digit1.sprite = fontManager.ReturnNumSprite(0);
        //        break;
        //    case 1:
        //        Debug.Log(1);
        //        digit0.sprite = fontManager.ReturnNumSprite(0);
        //        Debug.Log(2);
        //        digit1.sprite = fontManager.ReturnNumSprite(((int)newCount[0]) + 1);
        //        Debug.Log(3);
        //        break;
        //    case 2:
        //        digit0.sprite = fontManager.ReturnNumSprite(newCount[0]);
        //        digit1.sprite = fontManager.ReturnNumSprite(newCount[1]);
        //        break;
        //    default:
        //        digit0.sprite = fontManager.ReturnNumSprite(0);
        //        digit1.sprite = fontManager.ReturnNumSprite(0);
        //        break;
        //}
    }


}
