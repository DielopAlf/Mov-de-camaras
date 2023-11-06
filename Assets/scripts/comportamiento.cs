using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comportamiento : MonoBehaviour
{
    public Transform targetObject; // El objeto hacia el cual la cámara dirigirá su vista.
    public Camera cmr;
    public float zoomInDuration = 5.0f; // Duración del efecto de acercamiento.
    public float zoomOutDuration = 5.0f; // Duración del efecto de alejamiento.

    private float initialFocalLength;
    private Vector3 initialPosition;

    void Start()
    {
        initialFocalLength = cmr.focalLength;
        initialPosition = cmr.transform.position;
        StartCoroutine(RealizarEfectos());
    }

    IEnumerator RealizarEfectos()
    {
        // Efecto de acercamiento
        yield return ZoomInEffect(zoomInDuration);

        // Efecto de alejamiento
        yield return ZoomOutEffect(zoomOutDuration);

        // Aquí puedes agregar más efectos o personalizar el control de tiempo.
    }

    IEnumerator ZoomInEffect(float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            float newFocalLength = Mathf.Lerp(initialFocalLength, 5f, t);
            float newPositionZ = Mathf.Lerp(initialPosition.z, initialPosition.z - 17f, t);

            cmr.focalLength = newFocalLength;
            cmr.transform.position = new Vector3(initialPosition.x, initialPosition.y, newPositionZ);

            yield return null;
        }
    }

    IEnumerator ZoomOutEffect(float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            float newFocalLength = Mathf.Lerp(5f, initialFocalLength, t);
            float newPositionZ = Mathf.Lerp(initialPosition.z - 17f, initialPosition.z, t);

            cmr.focalLength = newFocalLength;
            cmr.transform.position = new Vector3(initialPosition.x, initialPosition.y, newPositionZ);

            yield return null;
        }
    }
}