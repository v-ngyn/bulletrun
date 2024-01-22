using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static int points;
    public GameObject scoreRef;
    public GameObject roundRef;
    public GameObject titleRef;
    public GameObject retryRef;
    public GameObject menuRef;
    Transform Player;
    IEnumerator co;
    public AudioClip roundSound;

    void Start()
    {
        ResumeGame();
        Cursor.visible = false;
        Player = GameObject.FindGameObjectWithTag("Player").transform; // find player
        points = 0;
        titleRef.SetActive(true);
        retryRef.SetActive(false);
        menuRef.SetActive(false);
        co = intro();
        StartCoroutine(co);
    }

    void Update()
    {
        // player has died
        if (Player == null)
        {
            PauseGame();
            titleRef.GetComponent<Text>().text = "YOU ARE DEAD";
            Cursor.visible = true;
            titleRef.SetActive(true);
            retryRef.SetActive(true);
            menuRef.SetActive(true);
        }

        // score and round textboxes
        scoreRef.GetComponent<Text>().text = "KILLS: " + points;
        roundRef.GetComponent<Text>().text = "" + int2roman(EnemySpawner.wave);

        // win condition
        if (points == 200)
        {
            titleRef.GetComponent<Text>().text = "YOU HAVE SURVIVED, YOU ARE WORTHY.";
            titleRef.SetActive(true);
            retryRef.SetActive(true);
            menuRef.SetActive(true);
            Cursor.visible = true;
            PauseGame();
        }
    }

    IEnumerator intro()
    {
        Debug.Log("Waiting");
        yield return new WaitForSecondsRealtime(5); // wait 5 seconds
        titleRef.SetActive(false);
        EnemySpawner.wave++; // start the waves
        AudioSource.PlayClipAtPoint(roundSound, transform.position);
        Debug.Log("Waited");
        EnemySpawner.spawned = false;
        StopCoroutine(co);
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    // convert integer to roman numeral
    // source: https://unitycoder.com/blog/2011/08/13/function-integer-to-roman-numerals/
    string int2roman(int value)
    {
        int[] arabic = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        string[] roman = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"};
        int i;
        string result = "";
        for (i = 0; i < 13; i++)
        {
            while (value >= arabic[i])
            {
                result = result + roman[i].ToString();
                value = value - arabic[i];
            }
        }
        return result;
    }
}
