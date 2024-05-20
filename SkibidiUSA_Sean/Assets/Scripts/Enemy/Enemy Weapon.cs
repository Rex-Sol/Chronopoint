using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class EnemyWeapon : MonoBehaviour
{
    //bullet
    [SerializeField] private GameObject hypersonicProjectile;
    //bullet force
    [SerializeField] private float force, upwardForce;
    //bullet stats
    [SerializeField] private float timeBetweenReloading, spread, reloadTime, timeBetweenShots;
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private bool allowButtonHold;
        

    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot, reloading;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private Transform attackPoint;

    [SerializeField] private bool allowInvoke = true;
    // graphics
    /* adds flash if wanted 
      [SerializeField] private GameObject muzzleFlash;
     */
    [SerializeField] TextMeshProUGUI ammoDisplay;
    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }


    // Update is called once per frame
    void Update()
    {
        MyInput();

        //ammo display
        if (ammoDisplay != null)
            ammoDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        /* instantiate muzzleFlash 
         if (muzzleFlash != null) instantiate(muzzleflash, attackPoint.position, Quaternion.identity);
        */
        }
    private void MyInput()
    {
       
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
    }
    private void Shoot()
    {

        readyToShoot = false;
        //find the hit position
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        //check if ray hits
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //a point far from player
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
        // Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);
        GameObject currentBullet = Instantiate(hypersonicProjectile, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * force, ForceMode.Impulse);
        /*
        Adds upward force
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
         */
        

        bulletsLeft--;
        bulletsShot++;
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke = false;
        }
        //allows more than 1 bullet per shot
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0) Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
