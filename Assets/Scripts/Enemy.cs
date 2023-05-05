using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private float speed = 2f;
    private Rigidbody2D _rigidbody2D;
    
    public ParticleSystem explosionParticle;
    
    
    void Start()
    {
        speed = Random.Range(2f, 5f);
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(0, -1);
        _rigidbody2D.velocity = movement * speed;
        
        if(transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit!");
            
            // play explosion particle effect
            PlayExplosion();
            
        }
    }
    
    public void PlayExplosion()
    {
        Instantiate(explosionParticle, transform.position, Quaternion.identity);
        explosionParticle.Play();
    }
    
}
