using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyRef;
    [SerializeField]
    private GameObject shooterRef;

    private Coroutine spawning;
    public static int wave;
    public static bool spawned;
    private int killsNeeded = 10;
    private int killIncrease = 10;
    public AudioClip roundSound;

    void Start()
    {
        wave = 0;
        spawned = true;
    }

    void Update()
    {
        // start spawning as coroutines aren't spawning already
        while (spawned == false)
        {
            startSpawning();
        }

        // wave handling
        if (GameMaster.points > killsNeeded)
        {
            spawned = false;
            wave++;
            killIncrease = 10 * wave;
            killsNeeded = GameMaster.points + killIncrease;
        }
    }

    // recursive spawning function
    private IEnumerator spawn(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-10f, 10f), 2f, 20f), Quaternion.identity);
        StartCoroutine(spawn(interval, enemy));
    }

    // wave handling
    void startSpawning()
    {
        spawned = true;
        if (wave == 1)
        {
            AudioSource.PlayClipAtPoint(roundSound, transform.position);
            Debug.Log("Round " + wave);
            spawning = StartCoroutine(spawn(2f, enemyRef));
            spawning = StartCoroutine(spawn(2f, enemyRef));
        }
        else if (wave == 2)
        {
            AudioSource.PlayClipAtPoint(roundSound, transform.position);
            Debug.Log("Round " + wave);
            spawning = StartCoroutine(spawn(3f, shooterRef));
        }
        else if (wave == 3)
        {
            AudioSource.PlayClipAtPoint(roundSound, transform.position);
            Debug.Log("Round " + wave);
            spawning = StartCoroutine(spawn(2.1f, enemyRef));
        }
        else
        {
            AudioSource.PlayClipAtPoint(roundSound, transform.position);
            Debug.Log("Round " + wave);
            spawning = StartCoroutine(spawn(1.9f, enemyRef));
            spawning = StartCoroutine(spawn(2.5f, shooterRef));
        }
    }
}
