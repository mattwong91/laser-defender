using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
  [Header("General")]
  [SerializeField] GameObject projectilePrefab;
  [SerializeField] float projectileSpeed = 10f;
  [SerializeField] float projectileLifetime = 5f;
  [SerializeField] float baseFiringRate = 0.1f;

  [Header("AI")]
  [SerializeField] bool useAI;
  [SerializeField] float firingRateVariance = 0f;
  [SerializeField] float minFiringRate = 0.1f;

  [HideInInspector] public bool isFiring;

  Coroutine firingCoroutine;
  AudioPlayer audioPlayer;

  void Awake()
  {
    audioPlayer = FindObjectOfType<AudioPlayer>();
  }

  void Start()
  {
    if (useAI)
    {
      isFiring = true;
    }
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

      float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
      timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);
      audioPlayer.PlayShootingClip();

      yield return new WaitForSeconds(timeToNextProjectile);
    }
  }
}
