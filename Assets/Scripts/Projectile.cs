using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    
    public float damage = 25;
    public GameObject parent;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (parent != collision.gameObject && collision.gameObject.tag != "Shot")
        {
            try
            {
                CarPhysics car = collision.gameObject.GetComponent<CarPhysics>();
                car.currentHealth -= damage;
                car.SmokeState();
                Destroy(gameObject);
            }
            catch (MissingComponentException)
            {
                if (collision.gameObject.tag == "Boost")
                {
                    Destroy(collision.gameObject, 0.1f);
                    Destroy(gameObject);
                }
                
            }
            catch(System.NullReferenceException)
            {
                print(collision.gameObject.tag);
                if (collision.gameObject.tag == "Boost")
                {
                    Destroy(collision.gameObject, 0.1f);
                    Destroy(gameObject);
                }
            }
        }
    }
}
