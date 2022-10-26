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
    float deathMultiplier = 1;
    public bool isInvisible;
    float invisTime;

    [Header("Dependencies")]
    [SerializeField] Rigidbody2D rb;
    bool grounded;
    [SerializeField] LayerMask groundCheckMask;
    [SerializeField] SpriteRenderer sR;
    [SerializeField] Animator anim;
    [SerializeField] Health healthScript;
    [SerializeField] Inventory inv;
    [SerializeField] DamagingHitbox dmgHitbox;


    [Header("Animations")]
    bool wasGrounded;
    [SerializeField] Material SpriteMaterial;
    [SerializeField] Shader GreyscaleShader;
    [SerializeField] Shader NormalShader;



    // Start is called before the first frame update
    void Start()
    {
        SpriteMaterial.shader = NormalShader;
        invisTime = invisibilityDuration;
        refreshInvisBar();
        healthScript.healthValue = maxHealth;
        healthScript.maxHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        velocity.x = Mathf.Lerp(rb.velocity.x, Input.GetAxisRaw("Horizontal")*speed*deathMultiplier,Time.deltaTime*10);
        
        velocity.y = rb.velocity.y;
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Debug.Log(grounded);
            if (grounded)
            {
                velocity.y = jumpHeight*deathMultiplier;
                anim.SetTrigger("Jump");
                anim.ResetTrigger("Land");
            }
            

        }
        rb.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Use Healing item
            healthScript.HealthChange(3);
            inv.changeItem(item.Medicine,-1);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            //turn invisible
            isInvisible = !isInvisible;
            if (isInvisible)
            {
                SpriteMaterial.shader = GreyscaleShader;
            }
            else
            {
                SpriteMaterial.shader = NormalShader;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)&&grounded)
        {
            //attack
            StartCoroutine(attackHitbox());
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
        if (isInvisible)
        {
            invisTime -= Time.deltaTime;
            if (invisTime <= 0)
            {
                invisTime = 0;
                isInvisible = false;
                SpriteMaterial.shader = NormalShader;
            }

            refreshInvisBar();
        }
        else if (invisTime!=invisibilityDuration)
        {
            invisTime += Time.deltaTime;
            invisTime = invisTime > invisibilityDuration ? invisibilityDuration : invisTime;

            refreshInvisBar();
        }

        

        //animations
        if (rb.velocity.x != 0)
        {
            sR.flipX = rb.velocity.x < 0 ? true : false;
        }

        if (grounded && !wasGrounded&&rb.velocity.y<=0)
        {
            anim.SetTrigger("Land");
            anim.ResetTrigger("Jump");
        }
        anim.SetFloat("Horizontal", Mathf.Abs( Input.GetAxisRaw("Horizontal")));
        wasGrounded = grounded;
    }
    IEnumerator attackHitbox()
    {
        anim.SetTrigger("Attack");
        dmgHitbox.GetComponent<BoxCollider2D>().enabled = true;
        deathMultiplier = 0;
        yield return new WaitForSeconds(0.7f);
        deathMultiplier = 1;
        dmgHitbox.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void Die()
    {
        StartCoroutine(death());
        
        //teleport back to the place.
    }
    IEnumerator death()
    {
        anim.SetTrigger("Die");
        foreach (CapsuleCollider2D hitbox in GetComponents<CapsuleCollider2D>())
        {
            hitbox.enabled = false;
        }
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;
        deathMultiplier = 0;

        yield return new WaitForSeconds(1);
        transform.position = Vector3.zero;
        
        foreach (CapsuleCollider2D hitbox in GetComponents<CapsuleCollider2D>())
        {
            hitbox.enabled = true;
        }
        rb.gravityScale = 1;
        anim.SetTrigger("Respawn");
        healthScript.HealthChange(10000);
        deathMultiplier = 1;

    }
    void refreshInvisBar()
    {

    }
}
