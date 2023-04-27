using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // player bounds
    private float xMin, xMax, yMin, yMax;
    private float _buffer = 0.25f;

    // player speed
    public float speed = 2f;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        xMin = -2.8f;
        xMax = 2.8f;
        yMin = -5f;
        yMax = 5f;
        
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        
        Movement();
    }
    
    void Movement()
    {
        
        // check if player is out of bounds
        if(transform.position.x < xMin - _buffer)
        {
            transform.position = new Vector2(xMax + _buffer, transform.position.y);
        }
        
        if(transform.position.x > xMax + _buffer)
        {
            transform.position = new Vector2(xMin - _buffer, transform.position.y);
        }
        
        if (transform.position.y > yMax - _buffer)
        {
            transform.position = new Vector2(transform.position.x, yMax - _buffer);
        }
        
        if (transform.position.y < yMin + _buffer)
        {
            transform.position = new Vector2(transform.position.x, yMin + _buffer);
        }

        
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(h, v);
        _rigidbody2D.velocity = movement * speed;
        
    }

    void Shoot()
    {
        // TODO : add bullet pooling
        var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * 7f;
        
        Destroy(bullet, 2f);
        
    }
}
