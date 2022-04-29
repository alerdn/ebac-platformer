using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;

    public Vector2 friction = new Vector2(0.1f, 0);

    public float speed;

    public float forceJump = 25;


    private void Update()
    {
        HandleJump();
        HandleMoviment();
    }

    private void HandleMoviment()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // BAD CODE: myRigidbody2D.MovePosition(myRigidbody2D.position - velocity * Time.deltaTime);
            myRigidbody2D.velocity = new Vector2(-speed, myRigidbody2D.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // BAD CODE: myRigidbody2D.MovePosition(myRigidbody2D.position + velocity * Time.deltaTime);
            myRigidbody2D.velocity = new Vector2(speed, myRigidbody2D.velocity.y);
        }

        if (myRigidbody2D.velocity.x > 0)
        {
            myRigidbody2D.velocity += friction;
        }
        else if (myRigidbody2D.velocity.x < 0)
        {
            myRigidbody2D.velocity -= friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody2D.velocity = Vector2.up * forceJump;
        }
    }
}
