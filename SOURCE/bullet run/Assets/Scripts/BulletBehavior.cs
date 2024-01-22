using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // destroy itself when you hit a wall or enemy
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
