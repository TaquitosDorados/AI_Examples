using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private bool derecha = true;

    public float speed = 5f;
    public bool foundPlayer;
    public bool startIdle;
    public bool attacking;

    public GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        int modificadorLado;
        if (!derecha)
        {
            modificadorLado = -1;
        }
        else
        {
            modificadorLado = 1;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.right), modificadorLado * 10f, LayerMask.GetMask("Player"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                foundPlayer= true;
            }
        } else if(foundPlayer)
        {
            foundPlayer= false;
        }

        if (attacking)
        {
            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Attack")
            {
                attacking = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            derecha = !derecha;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Move()
    {
        int modificadorLado;
        if (!derecha)
        {
            modificadorLado = -1;
        }
        else
        {
            modificadorLado = 1;
        }

        transform.Translate(Vector2.right * modificadorLado * speed * Time.deltaTime);
        animator.Play("Walk");
    }

    public void IdleStarter()
    {
        StartCoroutine(IdleStarterCoroutine());
    }

    IEnumerator IdleStarterCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(3, 6));
        startIdle = true;
    }

    public void timeOnIdle(float _time)
    {
        animator.Play("Idle");
        StartCoroutine(timeOnIdleCoroutine(_time));
    }

    IEnumerator timeOnIdleCoroutine(float _time)
    {
        yield return new WaitForSeconds(_time);
        startIdle = false;
    }

    public void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void Attack()
    {
        animator.Play("Attack");
        attacking = true;
    }
}
