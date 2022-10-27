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



    [Header("MovingStats")]
    int currentDirection=-1;
    Vector2 velocity;

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



        //check for wall
        RaycastHit2D wallCheck = Physics2D.Raycast(transform.position + new Vector3(currentDirection * 0.6f, 0), Vector2.down, 0.01f);
        //Debug.DrawRay(transform.position + new Vector3(currentDirection*0.6f, 0), Vector2.down * 0.01f, Color.magenta, 5);

        if (wallCheck.collider != null)
        {
            currentDirection = -currentDirection;
            if(wallCheck.collider.tag == "Player")
            {
                if (!wallCheck.collider.GetComponent<PlayerController>().isInvisible)
                {
                    currentDirection = -currentDirection;
                    attack();
                }
            }
        }
        else
        {
            //check for walk off edge
            RaycastHit2D edgeCheck = Physics2D.Raycast(transform.position + new Vector3(currentDirection * 0.6f, 0), Vector2.down, 1.11f);
            //Debug.DrawRay(transform.position + new Vector3(currentDirection, 0), Vector2.down, Color.cyan, 5);

            if (edgeCheck.collider != null)
            {
                if (edgeCheck.collider.tag != "GroundCollision")
                {

                    currentDirection = -currentDirection;
                }
            }
            else
            {
                currentDirection = -currentDirection;

            }
        }




        velocity.x = Mathf.Lerp(rb.velocity.x, currentDirection * speed, Time.deltaTime *10);
        rb.velocity = velocity;

        sR.flipX = rb.velocity.x > 0 ? true : false;


    }
    public void takeDamage()
    {
        anim.SetTrigger("Damage");
    }
    void attack()
    {
        StartCoroutine(attackHitbox());
    }
    IEnumerator attackHitbox()
    {
        anim.SetTrigger("Attack");
        dmgHitbox.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(attackAnimationTime);
        dmgHitbox.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void Die()
    {
        anim.SetTrigger("Die");
        //drop item
        GameObject itemobj = Instantiate(itemDropPrefab,transform.position,Quaternion.identity);
        int temp = Random.Range(0, 3);
        item _temp = temp == 0 ? item.Medicine : item.Apple;
        itemobj.GetComponent<ItemScript>().Typechange(_temp);
        Destroy(gameObject);

    }






}
