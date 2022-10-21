using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("STATS")]
    [SerializeField] float Health;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        velocity.x = Mathf.Lerp(rb.velocity.x, Input.GetAxisRaw("Horizontal")*speed,Time.deltaTime*10);
        
        velocity.y = rb.velocity.y;
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log(grounded);
            if (grounded)
            {
                velocity.y = jumpHeight;
            }
            

        }
        rb.velocity = velocity;

        // 
    }
    private void FixedUpdate()
    {
        Vector3 offset = -transform.up * 0.75f;
        RaycastHit2D groundCheck1 = Physics2D.Raycast(transform.position + offset + transform.right * 0.3f, Vector2.down, 0.1f, groundCheckMask);
        RaycastHit2D groundCheck2 = Physics2D.Raycast(transform.position + offset + transform.right * -0.3f, Vector2.down, 0.1f, groundCheckMask);
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
        

    }
}
