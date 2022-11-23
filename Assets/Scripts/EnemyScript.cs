using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth;
    public int speed;
    public int damage;
    [SerializeField] float attackAnimationTime;
    [SerializeField] Vector3 offset;
    [SerializeField] float hitboxWidth;



    [Header("MovingStats")]
    int currentDirection=-1;
    Vector2 velocity;
    bool dead = false;
    bool canAttack = true;

    [Header("Dependencies")]
    [SerializeField] Animator anim;
    [SerializeField] DamagingHitbox dmgHitbox;
    [SerializeField] GameObject itemDropPrefab;
    [SerializeField] SpriteRenderer sR;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Health healthScript;



    // Start is called before the first frame update
    void Start()
    {
        dmgHitbox.damage = damage;
        healthScript.healthValue = maxHealth;
        healthScript.maxHealth = maxHealth;
    }


    private void FixedUpdate()
    {
        if (dead) { return; }


        //check for wall
        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position + new Vector3(currentDirection * hitboxWidth, 0) + offset, Vector2.down, 0.1f);
        //Debug.DrawRay(transform.position + new Vector3(currentDirection * hitboxWidth, 0)  + offset, Vector2.down * 0.1f, Color.magenta, 5);
        //Debug.Log(wallCheck.collider);
        if (wallCheck.collider != null)
        {
            currentDirection = -currentDirection;
            if (wallCheck.collider.tag == "Player")
            {
                if (!wallCheck.collider.GetComponent<PlayerController>().isInvisible)
                {
                    currentDirection = -currentDirection;
                    if (canAttack)
                    {
                        attack();
                    }
                    
                }
            }
        }
        else
        {
            //check for walk off edge
            RaycastHit2D edgeCheck = Physics2D.Raycast(transform.position + new Vector3(currentDirection * hitboxWidth, 0) + offset, Vector2.down, 1.11f);
            //Debug.DrawRay(transform.position + new Vector3(currentDirection * hitboxWidth, 0)  + offset, Vector2.down*1.11f, Color.cyan, 5);
            //Debug.Log(edgeCheck.collider);
            if (edgeCheck.collider != null)
            {
                if (edgeCheck.collider.tag != "GroundCollision")
                {

                    currentDirection = -currentDirection;
                }
            }
            else
            {
                RaycastHit2D floor = Physics2D.Raycast(transform.position - (new Vector3(currentDirection * hitboxWidth, 0) + offset), Vector2.down, 1.6f);
                //Debug.DrawRay(transform.position - (new Vector3(currentDirection * hitboxWidth, 0)  + offset), Vector2.down*1.6f, Color.green, 5);
                //Debug.LogError("AAAAAAAAAAAAAAAAA" + floor.collider);
                if (floor.collider != null)
                {
                    //Debug.LogError("HGHGGGGGG" + floor.collider.tag);
                    if (floor.collider.tag == "GroundCollision")
                    {
                        currentDirection = -currentDirection;
                    }

                }
                //Debug.Log(floor.collider);

            }
        }




        velocity.x = Mathf.Lerp(rb.velocity.x, currentDirection * speed, Time.deltaTime * 10);
        rb.velocity = new Vector3(velocity.x, rb.velocity.y);

        sR.flipX = rb.velocity.x > 0 ? false : true;


    }
    void attack()
    {
        canAttack = false;
        StartCoroutine(attackHitbox());
    }
    IEnumerator attackHitbox()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackAnimationTime*0.5f);
        if (dead) { dmgHitbox.GetComponent<BoxCollider2D>().enabled = false; yield break; }
        dmgHitbox.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(attackAnimationTime * 0.5f);
        dmgHitbox.GetComponent<BoxCollider2D>().enabled = false;
        canAttack = true;
    }
    public void Die()
    {
        //drop item
        GameObject itemobj = Instantiate(itemDropPrefab, transform.position, Quaternion.identity);
        int temp = Random.Range(0, 3);
        item _temp = temp == 0 ? item.Medicine : item.Apple;
        itemobj.GetComponent<ItemScript>().Typechange(_temp);
        GetComponent<Health>().enabled = false;
        anim.SetTrigger("Die");
        StartCoroutine(Death());
        this.enabled = false;
        

    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }






}
