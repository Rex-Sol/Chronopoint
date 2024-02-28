using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject explosion;
    [SerializeField] private LayerMask whatIsEnemy;
    //stats
    [Range(0f, 1f)]
    [SerializeField] private float bounciness;
    [SerializeField] private bool usegravity;
    //damage
    [SerializeField] private int explosionDamage;
    [SerializeField] private float explosionRange;
    //life
    [SerializeField] private int maxCollisions;
    [SerializeField] private float maxLifetime;
    [SerializeField] private bool explodeOnTouch = true;
    private Enemy enemy;
    int collisions;
    PhysicMaterial physicsMat;
    


    // Start is called before the first frame update
    void Start()
    {
        setup();
    }

    // Update is called once per frame
    private void Update()
    {
        if (collisions > maxCollisions) Explode();

        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Explode();
    }
    private void Explode()
    {
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        //enemies in explosion range
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemy);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(explosionDamage);
        }
        Invoke("Delay", 0.5f);
    }
    private void Delay()
    {
        Destroy(gameObject);
    }

        private void OnCollisionEnter(Collision collision)
        {
            collisions++;

            //explode if bullethts an enemy and explode is true
            if (collision.collider.CompareTag("Enemy") && explodeOnTouch) Explode();
        }

        private void setup()
        {
            physicsMat = new PhysicMaterial();
            physicsMat.bounciness = bounciness;
            physicsMat.frictionCombine = PhysicMaterialCombine.Minimum;
            physicsMat.frictionCombine = PhysicMaterialCombine.Maximum;
            GetComponent<CapsuleCollider>().material = physicsMat;

            rb.useGravity = usegravity;
        }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

} 



