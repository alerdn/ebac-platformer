using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectileBase;
    public Transform positionToShoot;
    public float timeBetweenShoot = .3f;
    public Transform playserSideReference;

    private Coroutine _currentCoroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        } else if (Input.GetKeyUp(KeyCode.S))
        {
            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectileBase);
        projectile.transform.position = positionToShoot.position;
        projectile.side = playserSideReference.localScale.x;
    }
}
