using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
  [SerializeField] int health = 50;
  [SerializeField] ParticleSystem hitEffect;

  [SerializeField] bool applyCameraShake;
  CameraShake cameraShake;
  AudioPlayer audioPlayer;

  void Awake()
  {
    cameraShake = Camera.main.GetComponent<CameraShake>();
    audioPlayer = FindObjectOfType<AudioPlayer>();
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    DamageDealer damageDealer = other.GetComponent<DamageDealer>();

    if (damageDealer != null)
    {
      TakeDamage(damageDealer.GetDamage());
      PlayHitEffect();
      audioPlayer.PlayExplosionClip();
      ShakeCamera();
      damageDealer.Hit();
    }
  }

  void TakeDamage(int damage)
  {
    health -= damage;
    if (health <= 0)
    {
      Destroy(gameObject);
    }
  }

  void PlayHitEffect()
  {
    if (hitEffect != null)
    {
      ParticleSystem effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
      Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
    }
  }

  void ShakeCamera()
  {
    if (cameraShake != null && applyCameraShake)
    {
      cameraShake.Play();
    }
  }
}
