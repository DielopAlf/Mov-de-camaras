using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movcamara : MonoBehaviour
{
    public Transform player; // Referencia al jugador.
    public Transform targetPosition; // La posición a la que se moverá la cámara al pulsar el botón.
    public float smoothSpeed = 5.0f; // Controla la suavidad del movimiento.

    private bool isMoving = false;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isMoving)
        {
            StartCoroutine(MoveCamera());

            GameObject.Find("camara_1").transform.position = initialPosition + new Vector3(0f, 0f, -1f);

            GameObject.Find("bolita").transform.position = GameObject.Find("Player").transform.position + new Vector3(0f, 0f, -1f);

            Debug.Log(GameObject.Find("Player").transform.position);



            //  GameObject.Find("bolita").transform.position.x = gameObject.transform.position.x;
            //  GameObject.Find("bolita").transform.position.z = gameObject.transform.position.z-0.7f;

        }
    }

    IEnumerator MoveCamera()
    {
        isMoving = true;
        float elapsedTime = 0;
        Vector3 target;

        // Determina si la cámara debe moverse hacia la posición del jugador o volver a su posición original.
        if (transform.position == initialPosition)
        {
            target = targetPosition.position;
        }
        else
        {
            target = initialPosition;
        }

        while (elapsedTime < smoothSpeed)
        {
            transform.position = Vector3.Lerp(transform.position, target, (elapsedTime / smoothSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
    }
}