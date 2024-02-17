using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    private Color OwnColor;
    private Rigidbody2D rb;
    private Transform player;
    
    public float moveSpeed = 5f; // Düþman hareket hýzý
    public float attackRange = 3f; // Saldýrý menzili
    public float attackCooldown = 2f; // Saldýrý aralýðý
    private float attackTimer = 0f;

    void Start()
    {
        OwnColor = GetComponent<SpriteRenderer>().color;
        rb = GetComponent<Rigidbody2D>(); // Düþmanýn Rigidbody bileþenini al
        player = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncunun pozisyonunu al
    }

    void Update()
    {
        Move(); // Düþmaný hareket ettir
        Attack(); // Saldýrý gerçekleþtir
    }

    void Move()
    {
        // Düþmaný oyuncuya doðru hareket ettir
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    void Attack()
    {
        // Oyuncu saldýrý menziline girdiyse ve saldýrý aralýðýna ulaþýldýysa
        if (Vector2.Distance(transform.position, player.position) <= attackRange && Time.time >= attackTimer)
        {
            // Saldýrý gerçekleþtir
            Debug.Log("Oyuncuya saldýrý gerçekleþti!");
            // Saldýrý aralýðýný yeniden hesapla
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
