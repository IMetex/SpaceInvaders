using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrindlyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 2f;
    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Alien"))
        {
            //Destroy(gameObject);
            collision.gameObject.GetComponent<Aliens>().Kill();
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            //Destroy(collision.gameObject);
            //Destroy(gameObject);
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

    }
}
