using UnityEngine;

public class AbrirCerrarPuerta : MonoBehaviour
{
    [Header("Ángulos de Rotación Y")]
    // La puerta se cerrará en -90
    public float anguloCerrado = -90f;
    // La puerta se abrirá en -189.6
    public float anguloAbierto = -189.6f;
    
    [Header("Configuración General")]
    public float velocidad = 2f;        

    private bool estaAbierta = false;

    void Start()
    {
        // Inicializa la rotación de la puerta en el ángulo cerrado
        transform.localRotation = Quaternion.Euler(
            transform.localRotation.eulerAngles.x, 
            anguloCerrado, 
            transform.localRotation.eulerAngles.z
        );
    }

    void Update()
    {
        // 2. Lógica de movimiento: Rotar hacia el ángulo objetivo Y
        float targetY = estaAbierta ? anguloAbierto : anguloCerrado;

        // Creamos la rotación objetivo manteniendo los ángulos X y Z actuales
        Quaternion rotacionObjetivo = Quaternion.Euler(
            transform.localRotation.eulerAngles.x,
            targetY,
            transform.localRotation.eulerAngles.z
        );

        // Interpolación suave (Slerp)
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotacionObjetivo, Time.deltaTime * velocidad);
    }

    // FUNCIÓN PÚBLICA: Esto es lo que llamará el script del jugador
    public void ToggleDoorState()
    {
        estaAbierta = !estaAbierta; 
        Debug.Log("Puerta toggled por Raycast. Estado: " + (estaAbierta ? "ABIERTA" : "CERRADA"));
    }
}
