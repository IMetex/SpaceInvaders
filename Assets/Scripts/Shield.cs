using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Sprite[] states;
    private SpriteRenderer sP;
    private int health;
    void Start()
    {
        health = 4;
        sP = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet") || other.gameObject.CompareTag("FriendlyBullet"))
        {
            other.gameObject.SetActive(false);
            health--;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                sP.sprite = states[health - 1];
            }
        }

    }
}
