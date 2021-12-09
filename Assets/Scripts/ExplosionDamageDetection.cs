using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamageDetection : MonoBehaviour
{
    public int gunDamage = 1;


    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        //ShootableBox health = GetComponent<Collider>().GetComponent<ShootableBox>();
        ShootableBox health = collision.collider.gameObject.GetComponent<ShootableBox>();
        Debug.Log(collision.collider.name, collision.collider.gameObject);

        if (health != null)
        {
            // Call the damage function of that script, passing in our gunDamage variable
            health.Damage(gunDamage);
        }
    }


}
