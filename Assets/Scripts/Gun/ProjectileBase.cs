using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public int damageAmount = 1;
    public Vector3 direction;
    public float timeToDestroy = 2f;
    public float side = 1f;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * side);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.transform.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}
