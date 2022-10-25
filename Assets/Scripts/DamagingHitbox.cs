using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingHitbox : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.HealthChange(-damage,transform.position);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
