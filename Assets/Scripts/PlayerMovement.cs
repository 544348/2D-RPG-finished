using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public VectorValue startingPosition;
    public int coins;
    private TextMeshProUGUI coinsValue;
    private GameObject shop;

    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        transform.position = startingPosition.initialValue;
        coinsValue = GameObject.Find("Valueofcoin").GetComponent<TextMeshProUGUI>();
        shop = GameObject.Find("Shop");
        shop.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "coin")
        {
            coins++;
            Object.Destroy(collision.gameObject);
            coinsValue.text = coins.ToString();
        }
        if(collision.gameObject.tag == "shopkeeper")
        {
            shop.SetActive(true);
        }
    }
    public void UpdatingCoinValue()
    {
        coinsValue.text = coins.ToString();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove() // Added parentheses here
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    } // Added closing curly brace here

    void MoveCharacter()
    {
        myRigidbody.MovePosition(
           transform.position + change * speed * Time.deltaTime
        );
    }
}