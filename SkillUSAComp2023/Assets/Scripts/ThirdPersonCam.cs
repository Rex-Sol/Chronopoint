using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerObj;
    [SerializeField] private Rigidbody rb;
    
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Transform combatLookAt;

    [SerializeField] private GameObject basicCam;
    [SerializeField] private GameObject combatCam;

    [SerializeField] private cameraStyle currentStyle;
    [SerializeField] private enum cameraStyle
    {
        Basic,
        Combat
    }
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // switch camera styles
        if (Input.GetKeyDown(KeyCode.Alpha1)) switchCameraStyle(cameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.Alpha2)) switchCameraStyle(cameraStyle.Combat);

        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // rotate player object
        if (currentStyle == cameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        else if (currentStyle == cameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;
        }
    }

    private void switchCameraStyle(cameraStyle newStyle)
    {
        combatCam.SetActive(false);
        basicCam.SetActive(false);

        if (newStyle == cameraStyle.Basic) basicCam.SetActive(true);
        if (newStyle == cameraStyle.Combat) combatCam.SetActive(true);

        currentStyle = newStyle;
    }
}
