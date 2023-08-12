using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [Header("\n----------------Bullet Settings----------------\n")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] bulletSpawnPoints;
    [SerializeField] private Material bulletMaterial1;
    [SerializeField] private Material bulletMaterial2;
    [Header("\n----------------Bullet Properties----------------\n")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Vector3 bulletSize = new Vector3(0.2f, 0.2f, 1);
    [SerializeField] private Vector3 bulletDirection = Vector3.forward;
    [SerializeField] private float bulletLifeTime = 5f;
    [SerializeField] private float bulletDamage = 10f;
    [Header("\n----------------Shooting Settings----------------\n")]
    [SerializeField] private float shootingInterval = 0.5f;
    [SerializeField] private AudioSource shootingSound;
    [Range(-3f, 3f)] [SerializeField] private float soundPitch = 1f;
    [Header("\n----------------Debug Settings----------------\n")]
    [SerializeField] private bool debugMode = false;
    [Range(0f,1f)] [SerializeField] private float bulletOpacity = 0.5f;
    [Header("--------Bullet Direction--------")]
    [SerializeField] private bool bulletDirectionPreview = false;
    [SerializeField] private float bulletDirectionPreviewLength = 2f;

    private Coroutine shootingCoroutine;
    [SerializeField] private bool shootingAllowed = true;

    // Start is called before the first frame update
    void Start()
    {
        shootingSound.pitch = soundPitch;
    }

    // Update is called once per frame
    void Update()
    {
        // Start shooting if the space key is pressed and shooting is not allowed
        if (Input.GetKeyDown(KeyCode.Space) && shootingAllowed)
        {
            shootingCoroutine = StartCoroutine(ShootRepeatedly(shootingInterval));
        }
        // Stop shooting if the space key is pressed and shooting is allowed
        else if (Input.GetKeyUp(KeyCode.Space) && shootingAllowed)
        {
            StopCoroutine(shootingCoroutine);
            shootingAllowed = false;
            StartCoroutine(ShootReset(shootingInterval));
        }
    }

    IEnumerator ShootReset(float time)
    {
        yield return new WaitForSeconds(time);
        shootingAllowed = true;
    }

    IEnumerator ShootRepeatedly(float interval)
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(interval);
        }
    }

    void Shoot()
    {
        shootingSound.Play();
        foreach (Transform bulletSpawnPoint in bulletSpawnPoints)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = bulletDirection * bulletSpeed;
            bullet.GetComponent<BulletController>().destructionTime = bulletLifeTime;
            bullet.GetComponent<BulletController>().bulletDamage = bulletDamage;
        }
    }

    void OnDrawGizmos()
    {
        if (debugMode)
        {
            foreach (Transform bulletSpawnPoint in bulletSpawnPoints)
            {
                Gizmos.color = Color.green;
                if (bulletDirectionPreview) 
                    Gizmos.DrawCube(bulletSpawnPoint.position + bulletDirection * bulletDirectionPreviewLength, bulletSize);

                // Draw a semitransparent red cube at the transforms position
                Gizmos.color = new Color(1, 0, 0, bulletOpacity);
                Gizmos.DrawCube(bulletSpawnPoint.position, bulletSize);
            }
        }
    }
}
