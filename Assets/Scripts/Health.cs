using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool isPlayer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float knockBack;
    [Header("Dont set these in inspector, but in their controller scripts")]
    public int maxHealth=1000;
    public int healthValue=1000;
    
    

    Color healColour = new Color(92,156,83);
    Color damageColour = new Color(152,83,83);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HealthChange(int dmg,Vector3 from)
    {
        ColourFlash(dmg > 0);
        if (dmg > 0)
        {
            //heal
            healthValue += dmg;
            healthValue = healthValue > maxHealth ? maxHealth : healthValue;
        }
        else
        {
            //take damage
            healthValue += dmg;
            Vector3 differenceVector = transform.position - from;

            rb.velocity = differenceVector.normalized * knockBack;
        }

        if (healthValue <= 0)
        {
            //die

            if (isPlayer)
            {
                GetComponent<PlayerController>().Die();
            }
            else
            {
                GetComponent<EnemyScript>().Die();
            }
            
        }
        else
        {
            UpdateHealthBar();
        }

    }
    void UpdateHealthBar()
    {

    }
    IEnumerator ColourFlash( bool heal)
    {
        GetComponent<SpriteRenderer>().color = heal ? healColour : damageColour;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }





}
