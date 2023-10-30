using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movjugador : MonoBehaviour

    

{
    public float moveSpeed = 5.0f; // Velocidad de movimiento del jugador.
    public Transform cameraTransform; // Referencia a la cámara.
    Vector3 init_pos; // posición inicial cámara (en plano general)
    Quaternion init_rot; // orientación inicial de cámara (en plano general)
    bool cmr_status;


    void Start()
    {
        
        init_pos = GameObject.Find("camara_1").transform.position;
        init_rot = GameObject.Find("camara_1").transform.rotation;
        cmr_status = true;
       
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        movement.Normalize();

        transform.Translate(movement * moveSpeed * Time.deltaTime);

       

        if (Input.GetKeyDown(KeyCode.E)) cmr_status = !cmr_status;

        if (cmr_status == false) 
        {
           
            GameObject.Find("camara_1").transform.position = gameObject.transform.position + new Vector3(0f, 0.4f, -1.2f);
        
        
        }

        

        if (cmr_status == true) 
            
            {

                GameObject.Find("camara_1").transform.position = init_pos;
                GameObject.Find("camara_1").transform.rotation = init_rot;
            }
        
    } // end of udpate()
}