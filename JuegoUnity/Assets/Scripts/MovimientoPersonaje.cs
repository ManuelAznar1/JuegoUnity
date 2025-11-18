using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem; // Necesario para el nuevo sistema de entrada


public class MovimientoPersonaje : MonoBehaviour
{
    // Variables públicas de configuración (ajustables en el Inspector)
    public float speed = 6.0F;
    // Eliminamos jumpSpeed, ya no se usa.
    public float gravity = 20.0F; // Aumentamos la gravedad a un valor más estándar (20.0F)
    
    // Referencia a la cámara (Arrastra la Camera3P aquí)
    public Transform cameraTransform;
    
    // Variables privadas para los componentes
    private Vector3 moveDirection = Vector3.zero;
    private Animator anim;
    private CharacterController controller; 

    // Variables públicas para visualización (se muestran en el Inspector)
    public float x, y; // Velocidad local para la Blend Tree 2D

    void Start()
    {
        // 1. Obtiene el CharacterController del objeto actual (Player)
        controller = GetComponent<CharacterController>(); 
        
        // 2. Obtiene el Animator del objeto hijo 
        anim = GetComponentInChildren<Animator>(); 

        if (controller == null)
        {
            Debug.LogError("¡ERROR! Falta el CharacterController en el objeto " + gameObject.name + ". Movimiento deshabilitado.");
            enabled = false;
            return;
        }

        if (anim == null)
        {
            Debug.LogError("¡ERROR! No se encontró el componente Animator en los hijos.");
        }
    }
    
    void Update()
    {
        // Salida temprana si falta un componente crítico
        if (controller == null || anim == null) return;
        
        // 1. OBTENER LAS ENTRADAS DE MOVIMIENTO
        float inputH = Input.GetAxis("Horizontal"); // Movimiento lateral (A/D)
        float inputV = Input.GetAxis("Vertical");   // Movimiento adelante/atrás (W/S)

        // 2. LÓGICA DE MOVIMIENTO FÍSICO
        
        if (controller.isGrounded)
        {
            // Calcula la dirección de movimiento relativa a la cámara
            moveDirection = new Vector3(inputH, 0, inputV);
            moveDirection = cameraTransform.TransformDirection(moveDirection);
            
            // Previene que la velocidad vertical afecte al movimiento horizontal
            moveDirection.y = 0; 
            
            // Aplica la velocidad de movimiento
            moveDirection *= speed;
            
            // NOTA: La lógica de Salto (if (Input.GetButton("Jump"))) ha sido ELIMINADA.
        }
        
        // 3. APLICAR GRAVEDAD Y MOVIMIENTO FINAL
        
        // Aplica gravedad continuamente. Esto empujará al personaje hacia el suelo.
        moveDirection.y -= gravity * Time.deltaTime;
        
        // Mueve el CharacterController
        controller.Move(moveDirection * Time.deltaTime);

        // Bloquea el cursor
        Cursor.lockState = CursorLockMode.Locked;

        // 4. CONTROL DE ANIMACIONES
        
        // *** 4.1. Locomoción (Blend Tree 2D) ***
        
        // a) Obtiene la velocidad actual del CharacterController en el espacio del mundo.
        Vector3 velocity = controller.velocity;
        
        // b) Transforma la velocidad del mundo a espacio local del personaje.
        //    (Esto nos da la dirección de movimiento respecto al frente del personaje)
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        // c) Normaliza y limita los valores entre -1 y 1
        x = Mathf.Clamp(localVelocity.x / speed, -1f, 1f); // Velocidad lateral
        y = Mathf.Clamp(localVelocity.z / speed, -1f, 1f); // Velocidad adelante/atrás

        // d) Envía los valores a la Blend Tree 2D (Movimientos)
        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y); 

        // *** 4.2. Eliminación de variables de salto/agacharse ***
        
        // La variable verticalSpeed y la lógica de salto/caída han sido ELIMINADAS.
        
        // *** 4.3. Estado del suelo (IsGrounded) ***
        
        // Usamos controller.isGrounded para la transición de Aéreo a Movimiento
        anim.SetBool("IsGrounded", controller.isGrounded);
    }
}