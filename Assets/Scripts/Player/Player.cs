using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidbody2D;
    public HealthBase healthBase;

    [Header("SO Player setup")]
    public SOPlayerSetup soPlayerSetup;

    private Animator _currentAnimator;
    private float _currentSpeed;
    private bool _inAir;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentAnimator = Instantiate(soPlayerSetup.player, transform);
    }

    private void Update()
    {
        HandleJump();
        HandleMoviment();
        HandleInAir();
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        _currentAnimator.SetTrigger(soPlayerSetup.triggerDeath);
    }

    private void HandleMoviment()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = soPlayerSetup.speedRun;
            _currentAnimator.speed = 2;
        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;
            _currentAnimator.speed = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // BAD CODE: myRigidbody2D.MovePosition(myRigidbody2D.position - velocity * Time.deltaTime);
            myRigidbody2D.velocity = new Vector2(-_currentSpeed, myRigidbody2D.velocity.y);
            if (myRigidbody2D.transform.localScale.x != -1)
            {
                myRigidbody2D.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            }
            _currentAnimator.SetBool(soPlayerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            // BAD CODE: myRigidbody2D.MovePosition(myRigidbody2D.position + velocity * Time.deltaTime);
            myRigidbody2D.velocity = new Vector2(_currentSpeed, myRigidbody2D.velocity.y);
            if (myRigidbody2D.transform.localScale.x != 1)
            {
                myRigidbody2D.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            _currentAnimator.SetBool(soPlayerSetup.boolRun, true);
        }
        else
        {
            _currentAnimator.SetBool(soPlayerSetup.boolRun, false);
        }

        if (myRigidbody2D.velocity.x > 0)
        {
            myRigidbody2D.velocity += soPlayerSetup.friction;
        }
        else if (myRigidbody2D.velocity.x < 0)
        {
            myRigidbody2D.velocity -= soPlayerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _inAir = true;
            myRigidbody2D.velocity = Vector2.up * soPlayerSetup.forceJump;
            // myRigidbody2D.transform.localScale = Vector2.one;

            // Mata as animações em andamento
            DOTween.Kill(myRigidbody2D.transform);

            // HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        myRigidbody2D.transform.DOScaleY(soPlayerSetup.soJumpScaleY.value, soPlayerSetup.soAnimationDuration.value).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigidbody2D.transform.DOScaleX(soPlayerSetup.soJumpScaleX.value, soPlayerSetup.soAnimationDuration.value).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
    }

    private void HandleAnimationJump()
    {
        if (myRigidbody2D.velocity.y > 0)
        {
            _currentAnimator.SetTrigger("JumpUp");
        }
        else if (myRigidbody2D.velocity.y < 0)
        {
            _currentAnimator.SetTrigger("JumpDown");
        }else
        {
            _currentAnimator.SetTrigger("JumpLand");
            _inAir = false;
        }
    }

    private void HandleInAir()
    {
        if (_inAir)
        {
            HandleAnimationJump();
        }
    }
}
