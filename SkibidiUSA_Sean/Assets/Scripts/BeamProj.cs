using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamProj : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform gunTip;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxShotDistance;
    public float ShootDelayTime;
    public float overshootYAxis;

    private Vector3 ShotPoint;

    [Header("Cooldown")]
    public float ShotCd;
    private float ShotCdTimer;

    [Header("Input")]
    public KeyCode ShotKey = KeyCode.Mouse0;

    private bool Shoot;

    

    private void Update()
    {
        if (Input.GetKeyDown(ShotKey)) StartGrapple();

        if (ShotCdTimer > 0)
            ShotCdTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (Shoot)
            lr.SetPosition(0, gunTip.position);
    }

    private void StartGrapple()
    {
        if (ShotCdTimer > 0) return;

        // deactivate active swinging
        GetComponent<SwingingDone>().StopSwing();

        Shoot = true;

        
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxShotDistance))
        {
            ShotPoint = hit.point;

            Invoke(nameof(ExecuteGrapple), ShootDelayTime);
        }
        else
        {
            ShotPoint = cam.position + cam.forward * maxShotDistance;

            Invoke(nameof(StopGrapple), ShootDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, ShotPoint);
    }

    private void ExecuteGrapple()
    {
        

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = ShotPoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        

        Shoot = false;

       ShotCdTimer = ShotCd;

        lr.enabled = false;
    }

    public bool IsGrappling()
    {
        return Shoot;
    }

    public Vector3 GetGrapplePoint()
    {
        return ShotPoint;
    }
}
