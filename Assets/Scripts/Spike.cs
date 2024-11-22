using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class Spike : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameManager _gameManager;
    [Inject]
    private void Construct(GameManager gm)
    {
        Debug.Log("asdf");
        _gameManager = gm;
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log("update girdi");
        transform.Translate(Vector2.left * (_gameManager.ReturnCurrentTime() * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Create" )
        {
            Debug.Log("GenerateWithGap");
            _gameManager.GenerateWithGap();
        }else if (other.tag=="Destroy")
        {
            Debug.Log(gameObject.name + " is destroyed");
            Lean.Pool.LeanPool.Despawn(gameObject);
        }
    }
}
