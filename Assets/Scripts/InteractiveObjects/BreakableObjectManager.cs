using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjectManager : MonoBehaviour
{
    public GameObject effectPrefab;
    public float effectVerticalOffset;
    public SoundEffect soundEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerAttack")
        {
            if (soundEffect)
            {
                soundEffect.StartCoroutine("PlaySound");
                soundEffect.transform.parent = null;
            }

            Instantiate(effectPrefab, new Vector3(transform.position.x, transform.position.y + effectVerticalOffset, transform.position.z), Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}
