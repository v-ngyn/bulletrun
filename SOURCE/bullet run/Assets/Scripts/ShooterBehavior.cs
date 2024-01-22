using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehavior : MonoBehaviour
{
    Transform Player;
    public int moveSpeed;
    public GameObject offset;
    public GameObject bullet;
    private bool shooting = false;
    public AudioClip gunSound;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform; // finds player
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting == false)
        {
            InvokeRepeating("shootPlayer", 1f, 1f);
            shooting = true;
        }

        // "zombie ai", continuously follows player
        gameObject.transform.LookAt(Player);
        gameObject.transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    // shoot at position of player
    private void shootPlayer()
    {
        AudioSource.PlayClipAtPoint(gunSound, transform.position, 2f);
        // instantiate bullet
        GameObject instBullet = Instantiate(bullet, offset.transform.position, Quaternion.identity) as GameObject;
        // bullet facing player
        instBullet.transform.LookAt(Player, Vector3.up);
        // copy rigidbody properties onto bullet clone
        Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
        // bullet velocity pew pew
        instBulletRigidbody.velocity = transform.forward * 26f;
        // destroys bullet over time if it misses
        Destroy(instBullet, 2f);
    }

    // die when it gets shot
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            GameMaster.points++;
            Destroy(this.gameObject);
        }
    }
}
