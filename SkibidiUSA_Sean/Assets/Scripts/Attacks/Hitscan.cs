using UnityEngine;
using System.Collections;
public class Hitscan : MonoBehaviour
{
    public Transform firePoint;
    public GameObject enemyPrefab;
    public LineRenderer lineRenderer;

    public int damage;
    public int range;

    private void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }
   
    void Shoot()
    {
        lineRenderer.enabled = true;
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject != null && hitObject.CompareTag("Enemy"))
            {
                Enemy enemy = hitObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    Debug.Log("TakenDamage");
                }
            }

            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hit.point);
            }
        }
        else
        {
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, firePoint.position + firePoint.forward * range);
            }
        }
    }
}