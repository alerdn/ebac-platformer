using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;
    public HealthBase healthBase;
    public Animator animator;
    public string triggerAttack = "Attack";
    public string triggerKill = "Death";
    public float timeToDestroy = 1f;
    public AudioSource audioSourceKill;

    [Header("Movement")]
    public float speed = 6;
    public float direction = 1;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnEnemyKill;
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(Movement), 0, Random.Range(1f, 10f));
    }

    private void Update()
    {
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);
    }

    private void Movement()
    {
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
        transform.DOScaleX(-1 * direction, .5f);
    }


    private void OnEnemyKill()
    {
        healthBase.OnKill -= OnEnemyKill;
        PlayKillAnimation();
        audioSourceKill?.Play();
        Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            var health = collision.gameObject.GetComponent<HealthBase>();

            if (health != null)
            {
                health.Damage(damage);
                PlayAttackAnimation();
            }
        }
        else if (collision.transform.CompareTag("Boundry"))
        {
            direction = -1 * direction;
            transform.DOScaleX(-1 * direction, .5f);
        }
    }

    private void PlayAttackAnimation()
    {
        animator.SetTrigger(triggerAttack);
    }

    private void PlayKillAnimation()
    {
        animator.SetTrigger(triggerKill);
    }

    public void Damage(int amount)
    {
        healthBase.Damage(amount);


    }
}
