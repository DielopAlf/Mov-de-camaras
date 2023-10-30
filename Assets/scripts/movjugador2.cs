using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movjugador2 : MonoBehaviour


{
    public float moveSpeed = 5.0f; // Velocidad de movimiento del jugador.
    public Transform cameraTransform; // Referencia a la c�mara.
    Vector3 initPos; // posici�n inicial c�mara (en plano general)
    bool cmrStatus;
    float cameraTransitionSpeed = 5.0f; // Velocidad de transici�n de la c�mara.
    float dollySpeed = 2.0f; // Velocidad del efecto dolly.

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
            // Efecto dolly: mover la c�mara hacia adelante o hacia atr�s.
            float dollyInput = Input.GetAxis("Mouse ScrollWheel"); // Captura el desplazamiento del mouse.

            Vector3 targetPosition = GameObject.Find("camara_1").transform.position + cameraTransform.forward * dollyInput * dollySpeed;
            GameObject.Find("camara_1").transform.position = Vector3.MoveTowards(GameObject.Find("camara_1").transform.position, targetPosition, Time.deltaTime * cameraTransitionSpeed);
        }

        if (cmrStatus == true)
        {
            // Transici�n suave de la c�mara hacia la posici�n inicial.
            GameObject.Find("camara_1").transform.position = Vector3.MoveTowards(GameObject.Find("camara_1").transform.position, initPos, Time.deltaTime * cameraTransitionSpeed);
        }
    }
}
