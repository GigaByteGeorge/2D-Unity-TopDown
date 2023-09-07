using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{   
    public GameObject HitEffect;

    // Add a reference to the player's tag (make sure it matches the player's tag)
    public string playerTag = "Player";
    public string enemyLayer = "Enemy";

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves the player character
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Do nothing or add any specific behavior you want for the player collision.
            // You might want to add damage logic here, for example.
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer(enemyLayer))
        {
            // Destroy the enemy sprite
            Destroy(collision.gameObject);

            // Create the hit effect and destroy the arrow
            GameObject effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.8f);
            Destroy(gameObject);
        }
        else
        {
            // If it's not the player or an enemy, create the hit effect and destroy the arrow
            GameObject effect = Instantiate(HitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.8f);
            Destroy(gameObject);
        }
    }
}
