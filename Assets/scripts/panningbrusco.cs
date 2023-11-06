using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panningbrusco : MonoBehaviour
{
    public Transform targetObject; // El objeto hacia el cual la c�mara dirigir� su vista
    public float rotationSpeed = 2.0f;

    private bool isPanning = true;

    void Update()
    {
        if (isPanning)
        {
            // Calcula la direcci�n hacia el objetivo
            Vector3 targetDirection = targetObject.position - transform.position;

            // Calcula la rotaci�n hacia el objetivo
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            // Rota gradualmente hacia el objetivo
              transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
        }
    }
}