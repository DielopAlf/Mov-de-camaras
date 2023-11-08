using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaracontrol : MonoBehaviour
{
    Vector3 initPos;
    Vector3 targetPos;

    bool travStatus;
    bool panStatus;
    bool dollyStatus;

    Camera cmr;
    float f;
    float initialFocalLength = 50f; // Establece aquí el valor inicial de focalLength

    void Start()
    {
        cmr = gameObject.GetComponent<Camera>();
        cmr.usePhysicalProperties = true;
        f = initialFocalLength; // Utiliza el valor inicial

        initPos = gameObject.transform.position;
        targetPos = GameObject.Find("Cube").transform.position + new Vector3(0f, 0f, 0f);
        travStatus = true;
        panStatus = false;
        dollyStatus = false;
        
        cmr.usePhysicalProperties = true;
        cmr.focalLength = 50f;

        // Comienza la secuencia de acciones
        StartCoroutine(PerformActions());
    }

    IEnumerator PerformActions()
    {
        // Realiza el "travelling"
        while (travStatus)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 2 * Time.deltaTime);
            if (transform.position == targetPos)
            {
                travStatus = false;
                panStatus = true;
            }
            yield return null;
        }

        // Realiza el "panning" hasta Z -13
        Vector3 panTarget = new Vector3(transform.position.x, transform.position.y, -13f);

        while (panStatus && transform.position.z > panTarget.z)
        {
            transform.position = Vector3.MoveTowards(transform.position, panTarget, 2 * Time.deltaTime);
            yield return null;
        }

        // Realiza la rotación gradual
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
        float rotationDuration = 2.0f; // Duración de la rotación en segundos
        float rotationTimer = 0f;
       

        while (rotationTimer < rotationDuration)
        {
            rotationTimer += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, rotationTimer / rotationDuration);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        // Activa el "dolly inverso"
        dollyStatus = true;
        StartCoroutine(PerformDollyInverso());

        // Implementa lógica adicional si es necesario después del dolly
    
    
    } //end of performActions

    IEnumerator PerformDollyInverso()
    {
        // while (f > 200) // Cambiar el valor según tus necesidades

       
        while (f <= 200)
        {
            // f = f - 14f * 2.0f * Time.deltaTime; // Disminuye la focalLength para alejar la cámara
            //  cmr.focalLength = f;
            f = f + 60f * Time.deltaTime;
            cmr.focalLength = f;
            print(cmr.focalLength);

            yield return null;
        }

        // Finaliza el dolly inverso
       // dollyStatus = false;
    }

    void Update()
    {

       /* print(dollyStatus);

        // Implementa lógica adicional si es necesario durante el dolly
        if (dollyStatus)
        {   

            f = f + 0.05f * Time.deltaTime;
            cmr.focalLength = f;
            print(cmr.focalLength); */

            // Agrega aquí tu lógica adicional durante el dolly
       // }
    }
}
