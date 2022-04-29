using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;

    public Vector2 velocity;

    public float speed;


    void Update()
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
    }
}
