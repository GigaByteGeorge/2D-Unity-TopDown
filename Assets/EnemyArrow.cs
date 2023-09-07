using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    public GameObject HitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle player interaction here (e.g., apply damage or effects).
        }
        else
        {
            // Handle other interactions (e.g., create hit effect and destroy arrow).
            GameObject effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.8f);
            Destroy(gameObject);
        }
    }
}
