using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Player : MonoBehaviour
{
    public event Action OnPlayerDead;

    public Rigidbody2D myRigidbody2D;
    public HealthBase healthBase;

    [Header("SO Player setup")]
    public SOPlayerSetup soPlayerSetup;

    [Header("Jump Collision Check")]
    public Collider2D collider2d;
    public float distToGround;
    public float spaceToGround = .05f;

    [Header("VFXs")]
    public ParticleSystem walkVFX;
    public ParticleSystem runVFX;

    [Header("Audio")]
    public AudioSource audioJump;

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

        if (collider2d != null)
        {
            distToGround = collider2d.bounds.extents.y;
        }
    }

    private void Update()
    {
        IsGrounded();
        HandleJump();
        HandleMoviment();
        HandleInAir();
    }


    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, distToGround + spaceToGround);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void OnPlayerKill()
    {
        OnPlayerDead?.Invoke();
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
            myRigidbody2D.velocity = new Vector2(-_currentSpeed, myRigidbody2D.velocity.y);
            if (myRigidbody2D.transform.localScale.x != -1)
            {
                myRigidbody2D.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            }
            _currentAnimator.SetBool(soPlayerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
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

        /* Executar o VFX apenas se estiver no ch�o */
        if (IsGrounded())
        {
            if (_currentSpeed == soPlayerSetup.speedRun)
            {
                walkVFX.Stop();
                runVFX.Play();
            }
            else
            {
                walkVFX.Play();
                runVFX.Stop();
            }
        }
        else
        {
            walkVFX.Stop();
            runVFX.Stop();
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _inAir = true;
            myRigidbody2D.velocity = Vector2.up * soPlayerSetup.forceJump;

            /* Mata as anima��es em andamento */
            DOTween.Kill(myRigidbody2D.transform);

            PlayJumpVFX();
            audioJump?.Play();
        }
    }

    private void HandleAnimationJump()
    {
        if (myRigidbody2D.velocity.y > 0)
        {
            _currentAnimator.SetTrigger("JumpUp");
        }
        else if (myRigidbody2D.velocity.y < 0 && !IsGrounded())
        {
            _currentAnimator.SetTrigger("JumpDown");
        }
        else
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

    private void PlayJumpVFX()
    {
        VFXManager.Instance.PlayVFXType(VFXManager.VFXType.JUMP, transform.position);
    }
}
