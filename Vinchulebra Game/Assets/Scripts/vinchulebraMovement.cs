using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vinchulebraMovement : MonoBehaviour
{
    public float runSpeed, jumpForce;

    private float jumpHight=.4f;
    private float moveInput;

    private Rigidbody2D myBody2D;
    private Animator anim;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool facingRight=true;
    private bool grounded = true;

    public Vector3 range;


    void Awake()
    {
        myBody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>() ;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        checkCollisionForJump();
    }
    void Movement()
    {

        moveInput = Input.GetAxisRaw("Horizontal")*runSpeed;
        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        myBody2D.velocity = new Vector2(moveInput, myBody2D.velocity.y);
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (myBody2D.velocity.y > 0)
            {
                myBody2D.velocity = new Vector2(myBody2D.velocity.x, myBody2D.velocity.y * jumpHight);
            }
 
        }
       ;
        if (moveInput > 0 && !facingRight||moveInput<0&&facingRight)
        {
            Flip();
        }
    }

    void checkCollisionForJump()
    {
        Collider2D bottomHit=Physics2D.OverlapBox(groundCheck.position,range,0,groundLayer);
        if (bottomHit != null)
        {
            if (bottomHit.gameObject.tag == "Ground"&&Input.GetKeyDown(KeyCode.Space))
            {
                myBody2D.velocity = new Vector2(myBody2D.velocity.x,jumpForce);
                anim.SetBool("Jump", true);
            }
            else
            {
                anim.SetBool("Jump", false);
            }

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(groundCheck.position, range);
    }
    

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 transformScale = transform.localScale;

        transformScale.x *= -1;
        transform.localScale = transformScale;

    }

}
