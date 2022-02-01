using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapManager : MonoBehaviour
{
    public GameObject effectPrefab;
    public float effectVerticalOffset;
    public SoundEffect soundEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            HandleOverlap();

            if (soundEffect)
            {
                soundEffect.StartCoroutine("PlaySound");
                soundEffect.transform.parent = null;
            }

            Instantiate(effectPrefab, new Vector3(transform.position.x, transform.position.y + effectVerticalOffset, transform.position.z), Quaternion.identity);
            
            Destroy(this.gameObject);
        }
    }

    private void HandleOverlap()
    {
        switch (this.tag)
        {
            case "Coin":
                PlayerInventory.Instance.IncreaseCoins(1);
                break;
        }

        //if (this.tag == "Coin")
        //{
        //    PlayerInventory.Instance.IncreaseCoins(1);
        //}
    }
}
