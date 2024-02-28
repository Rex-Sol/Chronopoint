using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    public float fireRate;
    public bool beam;
    public List<Transform> projectileSpawns; // Changed to Transform type for easier access to position and rotation
    public GameObject projectile;
    public Transform target; // Changed to Transform for easier access to position and rotation
    public float fieldOfView;
    public int damage;

    List<GameObject> m_lastProjectiles = new List<GameObject>();
    float m_fireTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (beam && m_lastProjectiles.Count <= 0)
        {
            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(transform.forward - target.position));

            if (angle < fieldOfView)
            {
                SpawnProjectiles();
            }
        }
        else if (beam && m_lastProjectiles.Count > 0)
        {
            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(transform.forward - target.position));

            if (angle < fieldOfView)
            {
                foreach (GameObject projectile in m_lastProjectiles)
                {
                    Destroy(projectile);
                }
                m_lastProjectiles.Clear();
            }
        }
        else
        {
            m_fireTimer += Time.deltaTime;

            if (m_fireTimer >= fireRate)
            {
                float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(transform.forward - target.position));

                if (angle < fieldOfView)
                {
                    SpawnProjectiles();
                    m_fireTimer = 0.0f;
                }
            }
        }
    }

    void SpawnProjectiles()
    {
        if (!projectile)
        {
            return;
        }
        m_lastProjectiles.Clear();

        foreach (Transform spawnPoint in projectileSpawns)
        {
            if (spawnPoint)
            {
                GameObject proj = Instantiate(projectile, spawnPoint.position, Quaternion.LookRotation(spawnPoint.forward));
                proj.GetComponent<BeamProjectile>().FireProjectile(spawnPoint.gameObject, target.gameObject, damage);
                m_lastProjectiles.Add(proj);
            }
        }
    }
}
