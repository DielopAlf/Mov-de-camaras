using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{

    Vector3 initPos;
    Vector3 targetPos;

    bool travStatus;
    bool panStatus;
    bool dollyStatus;

    Camera cmr;
    float f;

    void Start()
    {

        cmr = gameObject.GetComponent<Camera>();

        cmr.usePhysicalProperties = true;
        f = cmr.focalLength;


        initPos = gameObject.transform.position;
        targetPos = GameObject.Find("Cube").transform.position + new Vector3(0f, 0f, 0f);
        travStatus = true;
        panStatus = false;
        dollyStatus = false;

    }


    // Update is called once per frame
    void Update()

    {
        if (travStatus = true) transform.position = Vector3.MoveTowards(transform.position, targetPos, 2 * Time.deltaTime);
        if (transform.position == targetPos) { travStatus = false; dollyStatus = false; }

        if (dollyStatus == true)
        {
            if (f < 500) // Cambia el valor de 5 a un valor adecuado para tu escena
            {
                f = f + 14f * 2.0f * Time.deltaTime; // Cambia el signo + para alejar la cámara
                cmr.focalLength = f;
                float x = cmr.transform.position.x;
                float y = cmr.transform.position.y;
                float z = cmr.transform.position.z;



                cmr.transform.position = new Vector3(x, y, z + 17f * 0.25f * 2.0f * Time.deltaTime); // Cambia el signo + para alejar la cámara
            }
        }

    } 
}