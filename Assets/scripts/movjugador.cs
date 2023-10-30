using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movjugador : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Velocidad de movimiento del jugador.
    public Transform cameraTransform; // Referencia a la cámara.
    Vector3 initPos; // posición inicial cámara (en plano general)
    bool cmrStatus;
    float cameraTransitionSpeed = 5.0f; // Velocidad de transición de la cámara.

    void Start()
    {
        initPos = GameObject.Find("camara_1").transform.position;
        cmrStatus = true;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        movement.Normalize();

        transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            cmrStatus = !cmrStatus;
        }

        if (cmrStatus == false)
        {
            // Transición suave de la cámara hacia la nueva posición.
            Vector3 targetPosition = gameObject.transform.position + new Vector3(0f, 0.4f, -1.2f);
            GameObject.Find("camara_1").transform.position = Vector3.MoveTowards(GameObject.Find("camara_1").transform.position, targetPosition, Time.deltaTime * cameraTransitionSpeed);
        }

        if (cmrStatus == true)
        {
            // Transición suave de la cámara hacia la posición inicial.
            GameObject.Find("camara_1").transform.position = Vector3.MoveTowards(GameObject.Find("camara_1").transform.position, initPos, Time.deltaTime * cameraTransitionSpeed);
        }
    }
}
