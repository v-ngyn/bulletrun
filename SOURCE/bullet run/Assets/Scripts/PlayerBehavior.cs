using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public GameObject reloadindicator;
    public Camera cam;
    public GameObject bullet;
    public float speed = 100f;
    public Transform offset;
    Rigidbody rb;
    [SerializeField] float movementSpeed;
    private float fireRate2 = 1f;
    private float lastShot2 = 0.0f;
    public AudioClip gunSound;
    public AudioClip shotgunSound;
    public AudioClip reloadSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // basic wasd movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

        pointAndShoot();

        // reload
        if (Time.time < fireRate2 + lastShot2)
            reloadindicator.GetComponent<Text>().text = "RELOADING ...";
        else
            reloadindicator.GetComponent<Text>().text = "";
    }

    // collision function
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    // aiming and shooting function
    void pointAndShoot()
    {
        // aiming function
        // converting the mouse position to a point in 3D-space
        Vector3 point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        float t = cam.transform.position.y / (cam.transform.position.y - point.y);
        Vector3 finalPoint = new Vector3(t * (point.x - cam.transform.position.x) + cam.transform.position.x, 1, t * (point.z - cam.transform.position.z) + cam.transform.position.z);
        // rotating the object to that point
        transform.LookAt(finalPoint, Vector3.up);

        // shooting function
        // pistol
        if (Input.GetButtonDown("Fire1"))
        {
            AudioSource.PlayClipAtPoint(gunSound, transform.position);

            // instantiate bullet
            GameObject instBullet = Instantiate(bullet, offset.position, Quaternion.identity) as GameObject;
            // bullet facing crosshair
            instBullet.transform.LookAt(finalPoint, Vector3.up);
            // copy rigidbody properties onto bullet clone
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            // bullet velocity pew pew
            instBulletRigidbody.velocity = transform.forward * speed;
            // destroys bullet over time
            Destroy(instBullet, 1f);
        }

        // shotgun
        if (Input.GetButtonDown("Fire2"))
        {
            if (Time.time > fireRate2 + lastShot2)
            {
                AudioSource.PlayClipAtPoint(shotgunSound, transform.position);
                // instantiate bullet
                GameObject pellet0 = Instantiate(bullet, offset.position, Quaternion.identity) as GameObject;
                GameObject pellet1 = Instantiate(bullet, new Vector3(offset.position.x + 1f, offset.position.y, offset.position.z + 3f), Quaternion.identity) as GameObject;
                GameObject pellet2 = Instantiate(bullet, new Vector3(offset.position.x - 1f, offset.position.y, offset.position.z + 3f), Quaternion.identity) as GameObject;
                GameObject pellet3 = Instantiate(bullet, new Vector3(offset.position.x + 2f, offset.position.y, offset.position.z + 3f), Quaternion.identity) as GameObject;
                GameObject pellet4 = Instantiate(bullet, new Vector3(offset.position.x - 2f, offset.position.y, offset.position.z + 3f), Quaternion.identity) as GameObject;
                // bullet facing crosshair
                pellet0.transform.LookAt(finalPoint, Vector3.up);
                pellet1.transform.LookAt(finalPoint, Vector3.up);
                pellet2.transform.LookAt(finalPoint, Vector3.up);
                pellet3.transform.LookAt(finalPoint, Vector3.up);
                pellet4.transform.LookAt(finalPoint, Vector3.up);
                // copy rigidbody properties onto bullet clone
                Rigidbody instBulletRigidbody0 = pellet0.GetComponent<Rigidbody>();
                Rigidbody instBulletRigidbody1 = pellet1.GetComponent<Rigidbody>();
                Rigidbody instBulletRigidbody2 = pellet2.GetComponent<Rigidbody>();
                Rigidbody instBulletRigidbody3 = pellet3.GetComponent<Rigidbody>();
                Rigidbody instBulletRigidbody4 = pellet4.GetComponent<Rigidbody>();
                // bullet velocity pew pew
                instBulletRigidbody0.velocity = transform.forward * speed;
                instBulletRigidbody1.velocity = transform.forward * speed;
                instBulletRigidbody2.velocity = transform.forward * speed;
                instBulletRigidbody3.velocity = transform.forward * speed;
                instBulletRigidbody4.velocity = transform.forward * speed;
                // destroys bullet over time
                Destroy(pellet0, 1f);
                Destroy(pellet1, 1f);
                Destroy(pellet2, 1f);
                Destroy(pellet3, 1f);
                Destroy(pellet4, 1f);

                lastShot2 = Time.time;

                AudioSource.PlayClipAtPoint(reloadSound, transform.position);
            }
        }
    }
}
