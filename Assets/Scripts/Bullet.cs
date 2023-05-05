using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private float speed = 2f;
    
    private EnemySpawner _enemySpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            // play explosion particle effect
            collision.gameObject.GetComponent<Enemy>().PlayExplosion();
            
            // destroy enemy    
            _enemySpawner.ReleaseEnemy(collision.gameObject.GetComponent<Enemy>());
            
            Destroy(gameObject);
            
            // play explosion sound
            // play explosion particle effect
            
        }
    }
}
