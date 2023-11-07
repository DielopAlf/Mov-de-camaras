using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camaracontrol2 : MonoBehaviour
{

    public Transform targetObject;
    public float rotationSpeed = 2.0f;
    public float moveSpeed = 2.0f;
    public float dollySpeed = 2.0f;
    public float panSpeed = 2.0f;
    public float waitTime = 2.0f; // Tiempo de espera en segundos

    private enum CinematicPhase
    {
        Travel,
        LookForward, // Nueva fase para mirar hacia adelante antes de Dolly y Paneos
        Dolly,
        PanRight,
        PanLeft,
        Done
    }

    private CinematicPhase cinematicPhase = CinematicPhase.Travel;
    private Camera cmr;
    private Vector3 initialPosition;
    private float f;
    private float timeElapsed = 0.0f;
    private bool hasRotated = false;

    void Start()
    {
        cmr = gameObject.GetComponent<Camera>();
        cmr.usePhysicalProperties = true;
        f = cmr.focalLength;
        initialPosition = gameObject.transform.position;

    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        switch (cinematicPhase)
        {
            case CinematicPhase.Travel:
                TravelToTarget();
                break;

            case CinematicPhase.LookForward:
                LookForward();
                break;

            case CinematicPhase.Dolly:
                DollyEffect();
                break;

            case CinematicPhase.PanRight:
                PanRight();
                break;

            case CinematicPhase.PanLeft:
                PanLeft();
                break;

            case CinematicPhase.Done:
                // La cinemática ha terminado, realiza cualquier acción necesaria.
                break;

            default:
                break;
        }
    }

    void TravelToTarget()
    {
        Vector3 targetDirection = targetObject.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        Transform trans;

        //  target.rotation = gameObject.transform.rotaton;
        //Quaternion front_wall = GameObject.Find("pared_frontal").transform.rotation;
        // Transform trans = target.LookAt(GameObject.Find("pared_frontal").transform);
        //Transform trans = GameObject.Find("parad_frontal").GetComponent<Transform>();
        // print(GameObject.Find("pared_frontal").transform.rotation);
        // Transform trans = gameObject.transform;
        // Quaternion qtn = trans.rotation = trans.LookAt(GameObject.Find("pared_frontal").transform);

        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        // transform.rotation = Quaternion.Slerp(transform.rotation, qtn, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetObject.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetObject.position, moveSpeed * Time.deltaTime);
            //  transform.rotation = Quaternion.RotateTowards(transform.rotation, front_wall,rotationSpeed * Time.deltaTime);
            trans = gameObject.transform;
            trans.LookAt(GameObject.Find("pared_frontal").transform);
        }
        else if (timeElapsed >= waitTime)
        {
            timeElapsed = 0.0f;
            cinematicPhase = CinematicPhase.LookForward;
            // cinematicPhase = CinematicPhase.Dolly;
        }
    }

    void LookForward()
    {
        if (!hasRotated)
        {
            // Gira la cámara hacia arriba para mirar hacia adelante

            transform.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime);
            if (Vector3.Angle(transform.forward, targetObject.position - transform.position) < 5)
            {
                hasRotated = true;
            }
        }

        if (hasRotated && timeElapsed >= waitTime)
        {
            timeElapsed = 0.0f;
            cinematicPhase = CinematicPhase.Dolly;
        }
    }

    void DollyEffect()
    {
        if (f < 500)
        {
            f = f + 14f * dollySpeed * Time.deltaTime;
            cmr.focalLength = f;

            if (timeElapsed >= waitTime)
            {
                timeElapsed = 0.0f;
                cinematicPhase = CinematicPhase.PanRight;
            }
        }
    }

    void PanRight()
    {
        Vector3 rightDirection = Quaternion.Euler(0, 90, 0) * (transform.forward); // Calcula la dirección hacia la derecha
        transform.position += rightDirection * panSpeed * Time.deltaTime;

        if (timeElapsed >= waitTime)
        {
            timeElapsed = 0.0f;
            cinematicPhase = CinematicPhase.PanLeft;
        }
    }

    void PanLeft()
    {
        Vector3 leftDirection = Quaternion.Euler(0, -90, 0) * (transform.forward); // Calcula la dirección hacia la izquierda
        transform.position += leftDirection * panSpeed * Time.deltaTime;

        if (timeElapsed >= waitTime)
        {
            timeElapsed = 0.0f;
            cinematicPhase = CinematicPhase.Done;
        }
    }
}
