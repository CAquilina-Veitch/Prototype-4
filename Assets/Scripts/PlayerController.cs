using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] int maxHealth;
    [SerializeField] float jumpHeight;
    [SerializeField] float speed;
    [SerializeField] float invisibilityDuration;

    [Header("MovingStats")]
    
    [SerializeField] float horizMoveSpeed;
    [SerializeField] float vertVel;
    [SerializeField] Vector2 velocity;

    [Header("Dependencies")]
    [SerializeField] Rigidbody2D rb;
    bool grounded;
    [SerializeField] LayerMask groundCheckMask;
    [SerializeField] SpriteRenderer sR;
    [SerializeField] Animator anim;
    [SerializeField] Health healthScript;

    [Header("Animations")]
    bool wasGrounded;
    // Start is called before the first frame update
    void Start()
    {
        healthScript.healthValue = maxHealth;
        healthScript.maxHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        velocity.x = Mathf.Lerp(rb.velocity.x, Input.GetAxisRaw("Horizontal")*speed,Time.deltaTime*10);
        
        velocity.y = rb.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Debug.Log(grounded);
            if (grounded)
            {
                velocity.y = jumpHeight;
                anim.SetTrigger("Jump");
                anim.ResetTrigger("Land");
            }
            

        }
        rb.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Use Healing item

        }


        // 
    }
    private void FixedUpdate()
    {
        Vector3 offset = -transform.up * 0.75f*transform.localScale.x;
        RaycastHit2D groundCheck1 = Physics2D.Raycast(transform.position + offset + transform.right * 0.3f * transform.localScale.x, Vector2.down, 0.1f, groundCheckMask);
        RaycastHit2D groundCheck2 = Physics2D.Raycast(transform.position + offset + transform.right * -0.3f * transform.localScale.x, Vector2.down, 0.1f, groundCheckMask);
        /*Debug.DrawRay(transform.position + transform.right * 0.3f - transform.up * 0.75f, Vector2.down, Color.blue, groundCheckMask);
        Debug.DrawRay(transform.position - transform.right * 0.3f - transform.up * 0.75f, Vector2.down, Color.blue, groundCheckMask);*/
        if (groundCheck1.collider!=null || groundCheck2.collider != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        

        //animations
        if (rb.velocity.x != 0)
        {
            sR.flipX = rb.velocity.x < 0 ? true : false;
        }

        if (grounded && !wasGrounded&&rb.velocity.y<=0)
        {
            anim.SetTrigger("Land");
        }
        wasGrounded = grounded;
    }
    public void Die()
    {
        anim.SetTrigger("Die");
        //teleport back to the place.
    }
}
