using System.Collections;
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
    float initialFocalLength = 50f;

    void Start()
    {
        cmr = gameObject.GetComponent<Camera>();
        cmr.usePhysicalProperties = true;
        f = initialFocalLength;

        initPos = gameObject.transform.position;
        targetPos = GameObject.Find("Cube").transform.position + new Vector3(0f, 0f, 0f);
        travStatus = true;
        panStatus = false;
        dollyStatus = false;

        cmr.usePhysicalProperties = true;
        cmr.focalLength = 50f;

        StartCoroutine(PerformActions());
    }

    IEnumerator PerformActions()
    {
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

        Vector3 panTarget = new Vector3(transform.position.x, transform.position.y, -13f);

        while (panStatus && transform.position.z > panTarget.z)
        {
            transform.position = Vector3.MoveTowards(transform.position, panTarget, 2 * Time.deltaTime);
            yield return null;
        }

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
        float rotationDuration = 2.0f;
        float rotationTimer = -0.4f;

        while (rotationTimer < rotationDuration)
        {
            rotationTimer += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, rotationTimer / rotationDuration);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        dollyStatus = true;
        yield return StartCoroutine(PerformDollyInverso());

        // Panning hacia el lado derecho lentamente
        Quaternion startRotationPanningRight = transform.rotation;
        Quaternion targetRotationPanningRight = Quaternion.Euler(0, 25, 0);
        float rotationDurationPanningRight = 4.0f;

        float rotationTimerPanningRight = -0.8f;
        while (rotationTimerPanningRight < rotationDurationPanningRight)
        {
            rotationTimerPanningRight += Time.deltaTime;
            float tPanningRight = Mathf.SmoothStep(0, 1, rotationTimerPanningRight / rotationDurationPanningRight);
            transform.rotation = Quaternion.Slerp(startRotationPanningRight, targetRotationPanningRight, tPanningRight);
            yield return null;
        }

        // Panning hacia el lado izquierdo lentamente
        Quaternion startRotationPanningLeft = transform.rotation;
        Quaternion targetRotationPanningLeft = Quaternion.Euler(0, -25, 0);
        float rotationDurationPanningLeft = 2.0f;

        float rotationTimerPanningLeft = -0.3f;
        while (rotationTimerPanningLeft < rotationDurationPanningLeft)
        {
            rotationTimerPanningLeft += Time.deltaTime;
            float tPanningLeft = Mathf.SmoothStep(0, 1, rotationTimerPanningLeft / rotationDurationPanningLeft);
            transform.rotation = Quaternion.Slerp(startRotationPanningLeft, targetRotationPanningLeft, tPanningLeft);
            yield return null;
        }

        // Vuelve a mirar al frente lentamente
        Quaternion finalRotation = Quaternion.Euler(0, 0, 0);
        float finalRotationDuration = 10.0f; // Aumenta la duración para que sea más lento
        float finalRotationTimer = 0.6f;

        while (finalRotationTimer < finalRotationDuration)
        {
            finalRotationTimer += Time.deltaTime;
            float tFinalRotation = Mathf.SmoothStep(0, 1, finalRotationTimer / finalRotationDuration);
            transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, tFinalRotation);
            yield return null;
        }

        // Puedes agregar más acciones después del último ajuste de rotación
    }

    IEnumerator PerformDollyInverso()
    {
        // Ajusta el valor según sea necesario para permitir que la cámara se aleje más
        while (f <= 100)
        {
            f = f + 40f * Time.deltaTime;
            cmr.focalLength = f;
            print(cmr.focalLength);

            yield return null;
        }

        // Finaliza el dolly inverso
        dollyStatus = false;
    }

    void Update()
    {
        // Puedes agregar lógica adicional durante el dolly si es necesario
    }
}
