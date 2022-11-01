using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    public Animator player;

    [Header("Speed setup")]
    public Vector2 friction = new Vector2(0.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 25;


    [Header("Animation setup")]
    public SOFloat soJumpScaleY;// = 1.5f;
    public SOFloat soJumpScaleX;// = .7f;
    public SOFloat soAnimationDuration;// = 0.3f;
    public Ease ease = Ease.OutBack;

    [Header("Animation player")]
    public string boolRun = "Run";
    public string triggerDeath = "Death";
    public float playerSwipeDuration = .1f;
}
