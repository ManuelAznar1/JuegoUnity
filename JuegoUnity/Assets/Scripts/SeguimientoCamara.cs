using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    // Asigna aquí el objeto transform de tu personaje
    public Transform target;

    // Esta es la compensación de altura (distancia por encima del personaje).
    // Por ejemplo, 10f para una vista aérea.
    public float heightOffset = 10f; // Renombrado a heightOffset para claridad

    void LateUpdate()
    {
        if (target != null)
        {
            // 1. Obtener la posición X y Z del personaje.
            Vector3 newPosition = target.position;

            // 2. Establecer la coordenada Y de la cámara:
            // La nueva altura (Y) es la altura del personaje (target.position.y) 
            // MÁS el desplazamiento (heightOffset).
            newPosition.y = target.position.y + heightOffset;

            // Mueve la cámara a la nueva posición (X, Y adaptativa, Z)
            transform.position = newPosition;

            // Opcional: Asegura que la cámara siempre mire hacia abajo
            // Esto es crucial para un minimapa.
            // (90 grados en X es mirar hacia abajo).
            transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0f);
        }
    }
}
