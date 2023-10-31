using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movjugadordolly : MonoBehaviour
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
        if (f > 5)
        {
            f = f - 14f*Time.deltaTime;
            cmr.focalLength = f;
            float x = cmr.transform.position.x;
            float y = cmr.transform.position.y;
            float z = cmr.transform.position.z;

            cmr.transform.position = new Vector3(x, y, z - 17f*0.25f* Time.deltaTime);

        }



    } // end of Update()
}
