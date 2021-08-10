using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;

    public int counter=0;
    public Text CoinText;
    public AudioSource coinSource;
    bool collected = false;
    public GameObject gameOverText,victoryText, restartButton;

    // Start is called before the first frame update
    void Start()
    {
        CoinText.text = counter.ToString();
        gameOverText.SetActive(false);
        victoryText.SetActive(false);
        restartButton.SetActive(false);
        coinSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Brzina", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump")) {
            jump = true;
            animator.SetBool("DaLiSkace", true);
        }

    }

    public void OnLanding()
    {
        animator.SetBool("DaLiSkace", false);
    }

    void FixedUpdate()
    {
        //Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        collected = false;
        if (col.CompareTag("Dijamant") && !collected) {
            coinSource.Play();
            Destroy(col.gameObject);
            counter++;
            CoinText.text = counter.ToString();
            collected = true;

        }
        if (col.CompareTag("Siljak"))
        {
           gameObject.SetActive(false);
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
        }
        if (col.CompareTag("Kuca"))
        {
            gameObject.SetActive(false);
            victoryText.SetActive(true);
            restartButton.SetActive(true);
        }
    }

}
