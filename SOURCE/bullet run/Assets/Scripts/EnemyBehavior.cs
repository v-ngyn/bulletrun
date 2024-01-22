using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    Transform Player;
    public int moveSpeed;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform; // finds player
    }

    void Update()
    {
        // "zombie ai", continuously follows player
        gameObject.transform.LookAt(Player);
        gameObject.transform.position += transform.forward * moveSpeed * Time.deltaTime;
        
        if (Player == null)
        {
            moveSpeed = 0;
        }
    }
    
    // die when you get shot by player and increment points
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            GameMaster.points++;
            Destroy(this.gameObject);
        }
    }
}
