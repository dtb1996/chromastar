using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public float midAirControl;
    public float dashCooldown = 1f;
    public AudioClip jumpSFX;
    public AudioClip attackSFX;
    public AudioSource sfxPlayer;

    //Dash ability variables
    public float dashDistance = 15f;
    private bool isDashing;
    private float timeSinceLastDash;
    //private float doubleTapTime;
    //private KeyCode lastKeyCode;

    private bool isMoving;
    private bool isFacingRight;
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

        timeSinceLastDash -= Time.deltaTime;

        if (!isDashing && timeSinceLastDash <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (isFacingRight)
                {
                    StartCoroutine(Dash(1f));
                    timeSinceLastDash = dashCooldown;
                }
                else
                {
                    StartCoroutine(Dash(-1f));
                    timeSinceLastDash = dashCooldown;
                }
                
            }

            ////Dash left
            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    if (doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
            //    {
            //        StartCoroutine(Dash(-1f));
            //    }
            //    else
            //    {
            //        doubleTapTime = Time.time + 0.3f;
            //    }

            //    lastKeyCode = KeyCode.A;
            //}

            ////Dash right
            //if (Input.GetKeyDown(KeyCode.D))
            //{
            //    if (doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
            //    {
            //        StartCoroutine(Dash(1f));
            //    }
            //    else
            //    {
            //        doubleTapTime = Time.time + 0.3f;
            //    }

            //    lastKeyCode = KeyCode.D;
            //}
        }
        

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
        if (!isDashing)
        {
            HandleMovement();

            if (Input.GetMouseButtonDown(0))
            {
                //StartCoroutine("Attack");

                animator.SetBool("isAttacking", true);

                CapsuleCollider2D collider = attackCollider.GetComponent<CapsuleCollider2D>();

                if (isFacingRight)
                {
                    if (collider.offset.x < 0f)
                    {
                        collider.offset = new Vector2(-(collider.offset.x), collider.offset.y);
                    }
                }
                else
                {
                    if (collider.offset.x > 0f)
                    {
                        collider.offset = new Vector2(-(collider.offset.x), collider.offset.y);
                    }
                }

                attackCollider.SetActive(true);

                sfxPlayer.clip = attackSFX;
                sfxPlayer.Play();
            }
        }
        //HandleMovement();

        HandleAnimation();

        
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
            isFacingRight = false;
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else
        {
            if (Input.GetKey("d"))
            {
                isFacingRight = true;
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

    private IEnumerator Dash(float direction)
    {
        isDashing = true;
        //rb.velocity = new Vector2(rb.velocity.x * 0.5f, 0f);
        rb.velocity = new Vector2(rb.velocity.x * 0.2f, 0f);
        rb.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        if (IsGrounded())
        {
            rb.gravityScale = gravity * 0.4f;
        }
        else
        {
            rb.gravityScale = 0f;
        }
        

        yield return new WaitForSeconds(0.4f);

        isDashing = false;
        rb.gravityScale = gravity;
    }

    private void HandleAnimation()
    {
        spriteRenderer.flipX = !isFacingRight;

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
