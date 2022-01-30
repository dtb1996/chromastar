using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public float midAirControl;
    public AudioClip jumpSFX;
    public AudioClip attackSFX;
    public AudioSource sfxPlayer;

    private bool isMoving;
    private Vector2 input;

    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    [SerializeField] private GameObject attackCollider;
    [SerializeField] private LayerMask platformsLayerMask;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    //private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent <CapsuleCollider2D>();
        attackCollider.SetActive(false);
    }

    private void Update()
    {
        HandleJump();

        //if (IsGrounded() && rb.velocity.y == 0)
        //{
        //    //animator.SetBool("isJumping", false);
        //}

        //if (!isMoving)
        //{
        //    input.x = Input.GetAxisRaw("Horizontal");

        //    if (input != Vector2.zero)
        //    {
        //        animator.SetFloat("moveX", input.x);

        //        //Flips sprite based on movement direction
        //        if (input.x < 0)
        //            spriteRenderer.flipX = true;
        //        else
        //            spriteRenderer.flipX = false;

        //        //var targetPos = transform.position;
        //        //targetPos.x += input.x;

        //        //StartCoroutine(Move(targetPos));
        //    }

        //    animator.SetBool("isMoving", isMoving);
        //}
        //if (IsGrounded())
        //{
        //    HandleMovement();
        //}
        HandleMovement();

        HandleAnimation();

        if (Input.GetMouseButtonDown(0))
        {
            //StartCoroutine("Attack");

            animator.SetBool("isAttacking", true);
            attackCollider.SetActive(true);
            
            sfxPlayer.clip = attackSFX;
            sfxPlayer.Play();
        }
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    animator.SetBool("isAttacking", false);
        //    attackCollider.SetActive(false);
        //}
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    IEnumerator Attack()
    {
        animator.SetBool("isAttacking", true);
        attackCollider.SetActive(true);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length * animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        animator.SetBool("isAttacking", false);
        attackCollider.SetActive(false);
    }

    public void AttackFinished()
    {
        animator.SetBool("isAttacking", false);
        attackCollider.SetActive(false);
    }

    private void HandleMovement()
    {
        if (Input.GetKey("a"))
        {
            spriteRenderer.flipX = true;
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else
        {
            if (Input.GetKey("d"))
            {
                spriteRenderer.flipX = false;
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }
    }

    private void HandleJump()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            //animator.SetBool("isJumping", true);
            rb.velocity = Vector2.up * jumpHeight;
            sfxPlayer.clip = jumpSFX;
            sfxPlayer.Play();

            jumpBufferCounter = 0f;
        }

        //Detects if jump is released before apex
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }
    }

    private void HandleAnimation()
    {
        if (IsGrounded())
        {
            animator.SetBool("isJumping", false);
            if (rb.velocity.x == 0f)
            {
                animator.SetBool("isMoving", false);
            }
            else
            {
                animator.SetBool("isMoving", true);
                //animator.SetFloat("moveX", rb.velocity.x);
            }
        }
        else
        {
            animator.SetBool("isJumping", true);
            //animator.SetFloat("velocityY", rb.velocity.y);
        }
        animator.SetFloat("moveX", rb.velocity.x);
        animator.SetFloat("velocityY", rb.velocity.y);
    }

    private bool IsGrounded()
    {
        //RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size - new Vector3(0.1f, 0f, 0f), 0f, Vector2.down, 0.1f, platformsLayerMask);
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size - new Vector3(0.1f, 0f, 0f), CapsuleDirection2D.Vertical, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return raycastHit.collider != null;
    }
}
