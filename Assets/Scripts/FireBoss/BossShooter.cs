using System.Collections;
using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [SerializeField] private GameObject darkFlameBallPrefab;
    [SerializeField] private Transform firePoint;
    //[SerializeField] private float shootInterval = 2f;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;

    


    // private void Start()
    // {
    //     StartCoroutine(ShootRepeatedly());
    // }
    
    // private IEnumerator ShootRepeatedly()
    // {
    //     while (true)
    //     {
    //         Shoot();
    //         yield return new WaitForSeconds(shootInterval);
    //     }
    // }

    public void Shoot()
    {
        GameObject projectile = Instantiate(darkFlameBallPrefab, firePoint.position, Quaternion.identity);
        ProjectileEnemy projScript = projectile.GetComponent<ProjectileEnemy>();

        if (projScript != null)
        {
            float direction = -Mathf.Sign(transform.localScale.x);  // reverse it because left = default
            projScript.SetDirection(direction);
        }

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

}
