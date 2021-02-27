using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Rigidbody2D rigidBodyPlayer;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocityPlayer = Vector3.zero;
    
    private bool isGrounded;
    private bool isJumping;

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
        // Calcul the horizontal movement (time pressed on arrows)
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        // Jump gestion
        

        MovePlayer(horizontalMovement);

        Flip(rigidBodyPlayer.velocity.x);

        float charVelocity = Mathf.Abs(rigidBodyPlayer.velocity.x);
        animator.SetFloat("Speed", charVelocity);
        animator.SetBool("IsGrounded", isGrounded);
    }

    /*
     * Make the move of the player smoooooooooth movement to the target
     */
    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rigidBodyPlayer.velocity.y);
        rigidBodyPlayer.velocity = Vector3.SmoothDamp(rigidBodyPlayer.velocity, targetVelocity, ref velocityPlayer, 0.05f);

        if(isJumping == true)
        {
            rigidBodyPlayer.AddForce(new Vector2(0f, jumpForce));
            
            isJumping = false;
        }
    }

    /*
     * Handle the flip orientation of the character
     */
    void Flip(float _velocity)
    {
        if(_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }else if(_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }
}
