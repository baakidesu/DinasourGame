using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Dino : MonoBehaviour
{
    #region Private
    
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private float score;
    private bool reloading;

    #endregion

    #region Public

    public Camera cam;
    public float jumpForce = 5;
    public TMP_Text scoreText;
    private static readonly int IsGood = Animator.StringToHash("isGood");

    #endregion
    
    void Awake()
    {
        Time.timeScale = 1f;
        score = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        score += Time.deltaTime * 4;
        scoreText.text = "Score : " + score.ToString("F");
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //Debug.Log("a");
            rb.AddForce(Vector2.up * (jumpForce ));
            isGrounded = false;
        }

        if (score>= 150)
        {
            Color camColor = new Color(0.3962264f, 0.3962264f, 0.3962264f);
            cam.DOColor(camColor, 1f).SetEase(Ease.Linear);
            scoreText.DOColor(Color.white, 1f).SetEase(Ease.Linear);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && isGrounded == false)
        {
            isGrounded = true;
        }else if (other.gameObject.CompareTag("Spike"))
        {
            Time.timeScale = 0.1f;
            StartCoroutine(Reload());
            if (reloading)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(0.5f);
    }
}
