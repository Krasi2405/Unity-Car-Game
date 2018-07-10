using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    
    public float damage = 25;
    public CarPhysics carParent;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shot")
            return;
            
        CarCollider carCollider = collision.GetComponent<CarCollider>();
        if (carCollider && carCollider.car != carParent)
        {
            carCollider.TakePenetrationDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Boost")
        {
            Destroy(collision.gameObject, 0.1f);
            Destroy(gameObject);
        }
    }
}
