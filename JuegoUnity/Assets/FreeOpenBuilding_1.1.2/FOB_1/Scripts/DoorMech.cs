using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMech : MonoBehaviour 
{
    
    public Vector3 OpenRotation, CloseRotation;

    // Aumentamos a 5f para que la velocidad sea visible
    public float rotSpeed = 5f; 

    public bool doorBool;

    // Bandera para saber si el jugador puede interactuar
    private bool canOpen = false; 


    void Start()
    {
        doorBool = false;
    }

    // Se llama UNA vez cuando el jugador ENTRA al trigger
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Persona")
        {
            canOpen = true; // El jugador está en rango
        }
    }

    // Se llama UNA vez cuando el jugador SALE del trigger
    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Persona")
        {
            canOpen = false; // El jugador NO está en rango
        }
    }

    void Update()
    {
        // 1. MANEJO DE INPUT (AHORA FIABLE):
        // Si el jugador puede abrir (canOpen) Y pulsa la E (Input.GetKeyDown)
        if (canOpen && Input.GetKeyDown(KeyCode.E)) 
        {
            // Alternar el estado (true <-> false)
            doorBool = !doorBool;
        }

        // 2. ROTACIÓN (Tu lógica original de movimiento):
        if (doorBool)
            transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (OpenRotation), rotSpeed * Time.deltaTime);
        else
            transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (CloseRotation), rotSpeed * Time.deltaTime);	
    }
}

