using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
  [SerializeField] GameObject projectilePrefab;
  [SerializeField] float projectileSpeed = 10f;
  [SerializeField] float projectileLifetime = 5f;
  [SerializeField] float firingRate = 0.1f;

  public bool isFiring;
  Coroutine firingCoroutine;

  void Start()
  {

  }

  void Update()
  {
    Fire();
  }

  void Fire()
  {
    if (isFiring && firingCoroutine == null)
    {
      firingCoroutine = StartCoroutine(FireContinuously());
    }
    else if (!isFiring && firingCoroutine != null)
    {
      StopCoroutine(firingCoroutine);
      firingCoroutine = null;
    }
  }

  IEnumerator FireContinuously()
  {
    while (true)
    {
      GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
      Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
      if (rb != null)
      {
        rb.velocity = transform.up * projectileSpeed;
      }
      Destroy(projectile, projectileLifetime);
      yield return new WaitForSeconds(firingRate);
    }
  }
}
