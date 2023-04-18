using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool = null;
    [SerializeField] private GameObject bulletPrefab;
    private const float maxX = 2.15f;
    private const float minX = -2.2f;

    private bool isShooting;

    public ShipStats shipStats;
    private Vector2 offScreenPos = new Vector2(0, -20f);
    private Vector2 startPos = new Vector2(0, -4.5f);

    private void Start()
    {
        shipStats.currentLifes = shipStats.maxLifes;
        shipStats.currentHealt = shipStats.maxHealth;
        transform.position = startPos;
        UIManager.UpdateHealthBar(shipStats.currentHealt);
        UIManager.UpdateLives(shipStats.currentLifes);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A) && transform.position.x > minX)
        {
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < maxX)
        {
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);
        }
        if (Input.GetKey(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(Shoot());
        }
#endif
    }
    private IEnumerator Shoot()
    {
        isShooting = true;
        //Instantiate(bulletPrefab,transform.position,Quaternion.identity);
        GameObject obj = objectPool.GetPooledObject();
        obj.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(shipStats.fireRate);
        isShooting = false;
        UIManager.UpdateHealthBar(shipStats.currentHealt);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            collision.gameObject.SetActive(false);
            TakeDamage();
        }
    }
    private IEnumerator Respawn()
    {
        transform.position = offScreenPos;
        yield return new WaitForSeconds(2);
        shipStats.currentHealt = shipStats.maxHealth;
        transform.position = startPos;
    }
    void TakeDamage()
    {
        shipStats.currentHealt--;
        UIManager.UpdateHealthBar(shipStats.currentHealt);
        if (shipStats.currentHealt <= 0)
        {
            shipStats.currentLifes--;
            UIManager.UpdateLives(shipStats.currentLifes);
            
            if (shipStats.currentLifes <= 0)
            {
                Debug.Log("Game Over");
            }
            else
            {
                //Debug.Log("Respawn");
                StartCoroutine(Respawn());
            }
        }

    }
}
