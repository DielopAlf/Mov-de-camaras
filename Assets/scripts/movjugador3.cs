using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movjugador3 : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Velocidad de movimiento del jugador.
    public float dollySpeed = 2.0f; // Velocidad del efecto dolly.

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        movement.Normalize();

        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Efecto "dolly": mueve la cámara hacia adelante o hacia atrás según el desplazamiento del mouse.
        float dollyInput = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * dollyInput * dollySpeed);
    }
}