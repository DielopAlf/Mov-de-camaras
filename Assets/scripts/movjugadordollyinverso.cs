using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movjugadordollyinverso : MonoBehaviour
{
    float f;
    Camera cmr;

    void Start()
    {
        cmr = GameObject.Find("Main Camera").GetComponent<Camera>();
        cmr.usePhysicalProperties = true;
        f = cmr.focalLength;
    }

    void Update()
    {
        if (f < 500) // Cambia el valor de 5 a un valor adecuado para tu escena
        {
            f = f + 14f * 2.0f* Time.deltaTime; // Cambia el signo + para alejar la cámara
            cmr.focalLength = f;
            float x = cmr.transform.position.x;
            float y = cmr.transform.position.y;
            float z = cmr.transform.position.z;

            cmr.transform.position = new Vector3(x, y, z + 17f * 0.25f * 2.0f*Time.deltaTime); // Cambia el signo + para alejar la cámara
        }
    }
}
