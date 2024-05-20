using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamProjectile : MonoBehaviour
{
    public float beamLength = 10.0f;
    GameObject m_launcher;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_launcher)
        {
            GetComponent<LineRenderer>().SetPosition(0, m_launcher.transform.position);
            GetComponent<LineRenderer>().SetPosition(1, m_launcher.transform.position + 
                (m_launcher.transform.forward * beamLength));
        }
    }

    public void FireProjectile(GameObject launcher, GameObject Enemy, int damage)
    {
        if (launcher)
        {
            m_launcher = launcher;
        }
    }
        
}

