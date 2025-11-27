using UnityEngine;

public class AbrirCerrarPuerta : MonoBehaviour
{
    [Header("Condición de Llave")]
    [Tooltip("Arrastra aquí el objeto Llave físico. La puerta se abrirá si este objeto ya NO existe (es decir, es null).")]
    public GameObject Llave; // ¡Debes asignar el objeto Llave en el Inspector!
    // ------------------------------------------------------------------

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
    public void ToggleDoorState()
    {
        // Si la referencia 'Llave' es null, significa que el objeto ha sido destruido
        // (es decir, el jugador lo recogió).
        if (Llave == null)
        {
            // El jugador ha recogido la llave, ¡abre la puerta!
            estaAbierta = !estaAbierta;
            Debug.Log("Puerta toggled por Raycast. Estado: " + (estaAbierta ? "ABIERTA" : "CERRADA"));
        }
        else
        {
            // El objeto Llave todavía existe en la escena.
            Debug.LogWarning("¡Puerta Bloqueada! La llave aún está en la escena.");
        }
    }
}
