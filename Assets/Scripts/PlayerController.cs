using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform player;
    public Rigidbody rb;
    public GameObject Coin;

    public float frwrdFrc = 1000f;
    public float sdwyFrc = 1000f;
    public float jmpFrc = 1000f;
    
    public int health = 5;

    private int score = 0;

    public Text scoreText;
    public Text healthText;
    public Text WinLoseText;

    public Text textColor;
    public Image backgroundColor;
    public GameObject WinLoseBG;

    void FixedUpdate ()
    {
        if (Input.GetKey("w") )
        {
            rb.AddForce(0, 0, frwrdFrc * Time.deltaTime);
        }
        if (Input.GetKey("s") )
        {
            rb.AddForce(0, 0, -frwrdFrc * Time.deltaTime);
        }
        if (Input.GetKey("a") )
        {
            rb.AddForce(-sdwyFrc * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("d") )
        {
            rb.AddForce(sdwyFrc * Time.deltaTime, 0, 0);
        }

        if(Input.GetKeyDown(KeyCode.Space) ) 
        {
            rb.AddForce(0, jmpFrc * Time.deltaTime, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Pickup")
        {
            score++;
            SetScoreText();
            Destroy(other.gameObject);
        }
        if (other.transform.tag == "Trap")
        {
            health--;
            SetHealthText();
        }
        if (other.transform.tag == "Goal")
        {
            Win();

            StartCoroutine(LoadScene(3));
        }

    }
    void SetScoreText()
    {
        scoreText.text = score.ToString("Score: " + score);
    }
    void SetHealthText()
    {
        healthText.text = health.ToString("Health: " + health);
    }
    
    public void Win()
    {

        WinLoseBG.SetActive(true);
        backgroundColor.color = Color.green;
        WinLoseText.text = "You Win";
        textColor.color = Color.black;



    }

    public void Lose()
    {
        WinLoseBG.SetActive(true);
        backgroundColor.color = Color.red;
        WinLoseText.text = "Game Over!";
        textColor.color = Color.white;

    }

    void Update()
    {
        if (health == 0)
        {
            Lose();
           
           score = 0;
           health = 5;

           StartCoroutine(LoadScene(3));
        }
    }


    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
