using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    private bool isOverlapping = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOverlapping = true;
            StartCoroutine("CheckForInput");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOverlapping = false;
            StopCoroutine("CheckForInput");
        }
    }

    private IEnumerator CheckForInput()
    {
        if (isOverlapping)
        {
            if (Input.GetMouseButton(1))
            {
                isOverlapping = false;
                EventManager.levelComplete.Invoke();
                LevelManager.ChangeLevel("level_2");
                StopCoroutine("CheckForInput");
                yield return null;
            }

            yield return new WaitForFixedUpdate();

            StartCoroutine("CheckForInput");
        }
        else
        {
            yield return null;
        }
        
    }
}
