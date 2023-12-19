using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float fallSpeed = 10f;
    [SerializeField]
    private float gravityDown = 20f;
    [SerializeField]
    private float gravityUp = 5f;
    private float originalGravityDown;
    private bool isFalling = false;
    public HealthBar healthBar;
    private bool hasReceivedDamage = false;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Collider2D playerCollider;
    public float damagePerHit = 10f;
    private float lastDamageTime = 0f;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;

    public GameObject lastTarget;

    private Transform playerTransform;
    [SerializeField]
    private LayerMask groundLayer;

    private int isFlyHash;



    Vector2 movement;

    void Start()
    {
        ////Set gravitasi dari awal langsung ada
        isFlyHash = Animator.StringToHash("IsFly");
        rb.gravityScale = gravityDown;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalGravityDown = gravityDown;
    }
    void Update()
    {
        if (lastTarget == null)
        {
            animator.SetBool("IsMoveDamage", false);
            Debug.Log("Test");
        }
       
        DestroyGround();

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Movement();
        HandleMovement();
        HandleGravity();

    }
    private void Movement()
    {
        //Memangil fungsi input gerakan Horizontal & Vertical di keyboard
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        Vector2 direction = new Vector2(x, 0);
        animator.SetFloat("Speed", Mathf.Abs(movement.x));



        if (moveDirection.magnitude == 0)
        {

            moveDirection = Vector2.zero;

        }

        //Mendeklarasi untuk membalik player
        if (x < 0)
        {
            Facing(false);
        }
        if (x > 0)
        {
            Facing(true);
        }
    }

    private void Facing(bool isFacingRight)
    {
        if (isFacingRight)
        {
            spriteRenderer.flipX = false; // Tidak flip
        }
        else
        {
            spriteRenderer.flipX = true; // Melakukan flip pada sumbu X
        }

    }
    void HandleGravity()
    {
        if (IsGrounded())
        {
            isFalling = false;
            animator.SetBool("IsFly", false);
        }
        else if (movement.y <= 0 && !isFalling)
        {
            rb.gravityScale = 20f;
            isFalling = true;
            animator.SetBool("IsFly", false);
            //Audioplayer.instance.StopSpecificSFX(4);
            //Audioplayer.instance.stopSFX();
        }

        if (isFalling && rb.gravityScale < 35f)
        {
            rb.gravityScale += Time.fixedDeltaTime * 10f;
        }
        bool wasFlying = animator.GetBool(isFlyHash);
        if (movement.y > 0)
        {
            rb.gravityScale = gravityUp;
            isFalling = false;
            hasReceivedDamage = false;
            animator.SetBool("IsFly", true);


            //if (!wasFlying)
            //{
               // Audioplayer.instance.PlaySFX(4);
            //}
        }
       
        if (IsGrounded() && rb.gravityScale > 30f && !hasReceivedDamage)
        {
            FallDamage();

        }


        if (IsGrounded() && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            rb.gravityScale = gravityDown;
            hasReceivedDamage = false;

        }
    }

    void FallDamage()
    {
        if (healthBar != null)
        {
            Audioplayer.instance.PlaySFX(3);
            healthBar.TakeDamage(5);
            hasReceivedDamage = true;

            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.red;
                StartCoroutine(ResetSpriteColor(0.5f)); 
            }
        }
    }

    void HandleMovement()
    {
        rb.velocity = new Vector2(movement.x * fallSpeed, rb.velocity.y);
    }
    void DestroyGround()
    {

        Vector2 playerPosition = playerCollider.bounds.center;
        bool isGrounded = IsGrounded();

        if (Input.GetKey(KeyCode.DownArrow) && isGrounded)
        {

            RaycastHit2D hit = Physics2D.Raycast(playerPosition, Vector2.down, playerCollider.bounds.extents.y * 2, groundLayer);

            if (hit.collider != null)
            {
                DamageGround(hit.collider);

                animator.SetBool("IsDownDamage", true);
            }
        }
        else
        {
            animator.SetBool("IsDownDamage", false);
        }

         if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveAndDamage(Vector2.right, isGrounded);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveAndDamage(Vector2.left, isGrounded);
        }
       
      
    }

    void DamageGround(Collider2D groundCollider)
    {
       
        GroundHealth groundHealth = groundCollider.GetComponent<GroundHealth>();
        lastTarget = groundCollider.gameObject; 
        if (groundHealth != null && Time.time - lastDamageTime >= 1f)
        {
            Audioplayer.instance.PlaySFX(5);

            groundHealth.TakeDamage(damagePerHit);
            lastDamageTime = Time.time;

        }
    }

    void MoveAndDamage(Vector2 direction, bool IsGrounded)
    {


        if (IsGrounded && Time.time - lastDamageTime >= 1f)
        {
            RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, direction, playerCollider.bounds.extents.x + 0.1f, groundLayer);

            if (hit.collider != null)
            {
                
                DamageGround(hit.collider);
                animator.SetBool("IsMoveDamage", true);
            }

            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                lastDamageTime = Time.time;
               
            }
            playerCollider.transform.Translate(movement * Time.deltaTime);

        }
       
    }
    
    bool IsGrounded()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + 0.1f, groundLayer);
        return hit.collider != null;

    }
    IEnumerator ResetSpriteColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white; 
        }
    }

}
    







