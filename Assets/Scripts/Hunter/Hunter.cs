using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public GameObject hunter;
    public Transform AttackPoint;
    public LayerMask PlayerLayer;

    private Color OwnColor;
    private Rigidbody2D rbPlayer;
    private Transform player;
    private Rigidbody2D rbHunter;

    public float moveSpeed = 5f; // Düþman hareket hýzý
    public float attackRange = 3f; // Saldýrý menzili
    public float attackCooldown = 2f; // Saldýrý aralýðý
    private float attackTimer = 0f;

    void Start()
    {
        OwnColor = hunter.GetComponent<SpriteRenderer>().color;
        rbPlayer = GetComponent<Rigidbody2D>(); // Düþmanýn Rigidbody bileþenini al
        rbHunter = GetComponent<Rigidbody2D>();
        rbHunter.gravityScale = 1;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncunun pozisyonunu al
    }

    void Update()
    {
        Move(); // Düþmaný hareket ettir
        Attack(); // Saldýrý gerçekleþtir
    }

    void Move()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rbHunter.velocity = new Vector2(direction.x * moveSpeed, rbHunter.velocity.y);

    }
    void Attack()
    {
        // Oyuncu saldýrý menziline girdiyse ve saldýrý aralýðýna ulaþýldýysa
        if (Vector2.Distance(rbPlayer.velocity, player.position) <= attackRange && Time.time >= attackTimer)
        {
            Collider2D hitPlayer = Physics2D.OverlapCircle(AttackPoint.position, attackRange, PlayerLayer);
            hitPlayer.GetComponent<Player>().TakeDamege();
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
