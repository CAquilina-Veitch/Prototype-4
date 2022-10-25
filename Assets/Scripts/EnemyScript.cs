using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public int speed;
    public int damage;
    [SerializeField] float attackAnimationTime;
    [Tooltip("Values only 1 or 0 please.")]
    public Vector2 movementFreedom;


    //[Header("MovingStats")]

    [Header("Dependencies")]
    Animator anim;
    [SerializeField] DamagingHitbox dmgHitbox;
    [SerializeField] GameObject itemDropPrefab;




    // Start is called before the first frame update
    void Start()
    {
        dmgHitbox.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void attack()
    {
        
    }
    IEnumerator attackHitbox()
    {
        //anim.SetTrigger("Attacking");
        dmgHitbox.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(attackAnimationTime);
        dmgHitbox.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void Die()
    {
        anim.SetTrigger("Die");
        //drop item
        Instantiate(itemDropPrefab,transform.position,Quaternion.identity);
        Destroy(gameObject);

    }






}
