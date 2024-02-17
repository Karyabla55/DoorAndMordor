using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public partial class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;    // oyuncu hareket hizi
    [SerializeField] private float sprintSpeed = 10f; // kosma hizi
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    
    public Animator animator;
    
    public Weapon weapon;
    private int Combo;
    private int index;
    private float currentSpeed;
    private float moveInput;
    private bool isJump;
    private bool isAttack;

    private Rigidbody2D rb;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSpeed = moveSpeed;
    }

    private void Update()
    {   
        if (weapon != null)
        {
            index = weapon.CurrentWeaponIndex();
            isAttack = weapon.isAttack();
            

        }
        else
        {
            index = 0;
        }

        
        
        if (isAttack)
        {
            AnimatorUpdateAttack(index,Combo);

        }
        else
        {
            // Yatay hareket i�lemi
            if (Input.GetKeyDown(sprintKey))
            {
                currentSpeed = sprintSpeed;
            }
            else if (Input.GetKeyUp(sprintKey))
            {
                currentSpeed = moveSpeed;
            }

            moveInput = Input.GetAxisRaw("Horizontal");
            if (isGrounded && !isJump)
            {
                rb.velocity = new Vector2(moveInput * currentSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);
            }

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(SetIsJumpedFalse());
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJump = true;
            }

            AnimatorUpdate(index);
        }
        
    }

   
    private IEnumerator SetIsJumpedFalse()
    {
        yield return new WaitForSeconds(0.5f);
        isJump = false;
    }

    private void AnimatorUpdate(int index)
    {
        if (moveInput > 0f && Input.GetKey(sprintKey))
        {
            if (index == 1)
            {
                animator.SetInteger("State", 8);
            }
            else if (index == 2)
            {
                animator.SetInteger("State", 5);
            }
            else 
            {
                animator.SetInteger("State", 2);
            }
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0f && Input.GetKey(sprintKey))
        {
            if (index == 1)
            {
                animator.SetInteger("State", 8);
            }
            else if (index == 2)
            {
                animator.SetInteger("State", 5);
            }
            else
            {
                animator.SetInteger("State", 2);
            }
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0f)
        {
            if (index == 1)
            {
                animator.SetInteger("State", 7);
            }
            else if (index == 2)
            {
                animator.SetInteger("State", 4);
            }
            else
            {
                animator.SetInteger("State", 1);
            }
            spriteRenderer.flipX = false;
        }
        else if (moveInput < 0f)
        {
            if (index == 1)
            {
                animator.SetInteger("State", 7);
            }
            else if (index == 2)
            {
                animator.SetInteger("State", 4);
            }
            else
            {
                animator.SetInteger("State", 1);
            }
            spriteRenderer.flipX = true;
        }
        else if(index == 1)
        {
            animator.SetInteger("State", 6);
        }
        else if (index == 2)
        {
            animator.SetInteger("State", 3);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
        if (rb.velocity.y > 0f)
        {
            animator.SetInteger("State", 9);
        }
        else if (rb.velocity.y < 0f)
        {
            animator.SetInteger("State", 10);
        }
    }

    private void AnimatorUpdateAttack(int index,int Combo)
    {
        if(index == 1)
        {
            animator.SetInteger("State", 11);
        }
        else if (index == 2)
        {
            if(Combo%2 == 0)
            animator.SetInteger("State", 12);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
