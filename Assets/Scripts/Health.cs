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
    SpriteRenderer sR;
  

    // Start is called before the first frame update
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HealthChange(int dmg)
    {
        StartCoroutine(ColourFlash(dmg > 0));
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
            rb.velocity = Vector3.zero;
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
        sR.color = heal ? Color.green : Color.red;
        yield return new WaitForSeconds(0.25f);
        sR.color = Color.white;
    }





}
