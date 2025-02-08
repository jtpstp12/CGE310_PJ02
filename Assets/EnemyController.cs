using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = new Vector2((isFacingRight ? 1 : -1) * moveSpeed, rb.velocity.y);

        // ����Ҫ���ᾧ�������
        if (Physics2D.OverlapCircle(groundCheck.position, 0.1f, wallLayer))
        {
            Flip();
        }

        anim.SetBool("isWalking", true);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ���¡�ѧ��ѹ Die() �ҡ GameController
            GameController player = collision.GetComponent<GameController>();
            if (player != null)
            {
                player.SendMessage("Die");
            }
        }
    }
}
