using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;

    [Header("Speed setup")]
    public Vector2 friction = new Vector2(0.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 25;

    [Header("Animation setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float animationDuration = 0.3f;

    [Header("Animation player")]
    public string boolRun = "Run";
    public Animator animator;
    public float playerSwipeDuration = .1f;

    private float _currentSpeed;
    private bool _inAir;

    private void Update()
    {
        HandleJump();
        HandleMoviment();
        HandleInAir();
    }

    private void HandleMoviment()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = speedRun;
            animator.speed = 2;
        }
        else
        {
            _currentSpeed = speed;
            animator.speed = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // BAD CODE: myRigidbody2D.MovePosition(myRigidbody2D.position - velocity * Time.deltaTime);
            myRigidbody2D.velocity = new Vector2(-_currentSpeed, myRigidbody2D.velocity.y);
            if (myRigidbody2D.transform.localScale.x != -1)
            {
                myRigidbody2D.transform.DOScaleX(-1, playerSwipeDuration);
            }
            animator.SetBool(boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // BAD CODE: myRigidbody2D.MovePosition(myRigidbody2D.position + velocity * Time.deltaTime);
            myRigidbody2D.velocity = new Vector2(_currentSpeed, myRigidbody2D.velocity.y);
            if (myRigidbody2D.transform.localScale.x != 1)
            {
                myRigidbody2D.transform.DOScaleX(1, playerSwipeDuration);
            }
            animator.SetBool(boolRun, true);
        }
        else
        {
            animator.SetBool(boolRun, false);
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
            _inAir = true;
            myRigidbody2D.velocity = Vector2.up * forceJump;
            // myRigidbody2D.transform.localScale = Vector2.one;

            // Mata as animações em andamento
            DOTween.Kill(myRigidbody2D.transform);

            // HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        myRigidbody2D.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo);
        myRigidbody2D.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo);
    }

    private void HandleAnimationJump()
    {
        if (myRigidbody2D.velocity.y > 0)
        {
            animator.SetTrigger("JumpUp");
        }
        else if (myRigidbody2D.velocity.y < 0)
        {
            animator.SetTrigger("JumpDown");
        }else
        {
            animator.SetTrigger("JumpLand");
            _inAir = false;
        }
    }

    private void HandleInAir()
    {
        if (_inAir)
        {
            Debug.Log(myRigidbody2D.velocity.y);
            HandleAnimationJump();
        }
    }
}
