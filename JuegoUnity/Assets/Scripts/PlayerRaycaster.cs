using UnityEngine;

public class PlayerRaycaster : MonoBehaviour
{
    [Header("Configuración de Interacción")]
    public KeyCode teclaAccion = KeyCode.E;
    public float distanciaInteraccion = 3f; // Distancia máxima para abrir

    void Update()
    {
        // 1. Chequea si se presionó la tecla de acción (E)
        if (Input.GetKeyDown(teclaAccion))
        {
            // 2. Lanza un rayo desde la cámara hacia adelante
            // Nota: 'transform' aquí es la cámara, por eso usamos transform.forward
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, distanciaInteraccion))
            {
                // 3. Verifica si el objeto impactado es una puerta
                AbrirCerrarPuerta doorScript = hit.collider.GetComponent<AbrirCerrarPuerta>();
                
                if (doorScript != null)
                {
                    // 4. Si es una puerta, llama a su función pública
                    doorScript.ToggleDoorState();
                }
            }
        }
    }
}
