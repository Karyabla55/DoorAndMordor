using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    private Color OwnColor;
    private Rigidbody2D rb;
    private Transform player;
    
    public float moveSpeed = 5f; // D��man hareket h�z�
    public float attackRange = 3f; // Sald�r� menzili
    public float attackCooldown = 2f; // Sald�r� aral���
    private float attackTimer = 0f;

    void Start()
    {
        OwnColor = GetComponent<SpriteRenderer>().color;
        rb = GetComponent<Rigidbody2D>(); // D��man�n Rigidbody bile�enini al
        player = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncunun pozisyonunu al
    }

    void Update()
    {
        Move(); // D��man� hareket ettir
        Attack(); // Sald�r� ger�ekle�tir
    }

    void Move()
    {
        // D��man� oyuncuya do�ru hareket ettir
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    void Attack()
    {
        // Oyuncu sald�r� menziline girdiyse ve sald�r� aral���na ula��ld�ysa
        if (Vector2.Distance(transform.position, player.position) <= attackRange && Time.time >= attackTimer)
        {
            // Sald�r� ger�ekle�tir
            Debug.Log("Oyuncuya sald�r� ger�ekle�ti!");
            // Sald�r� aral���n� yeniden hesapla
            attackTimer = Time.time + attackCooldown;
        }
    }

    public void TakeDamage(Color color)
    {
        Debug.Log("Work");
        if (color == OwnColor)
        {
            Debug.Log("Die");
            Destroy(gameObject);

        }
    }
    


   
}
