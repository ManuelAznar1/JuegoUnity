using UnityEngine;
using System.Collections; // Necesario para usar Coroutines

public class InteraccionObjeto : MonoBehaviour
{
// ⭐️ NUEVAS VARIABLES PÚBLICAS PARA CONFIGURAR EN EL INSPECTOR ⭐️
    // 1. Nombre del objeto que se mostrará en los mensajes (ej: "llave", "poción", "moneda")
    [Header("Configuración del Objeto")]
    private string nombreDelObjeto; 
    
    // 2. Duración del mensaje al recoger
    public float duracionMensaje = 3.0f; 

    private const string INTERACTION_PREFIX = "Pulsa [E] para recoger el/la "; // Prefijo para el mensaje de interacción
    
    private bool jugadorCerca = false; 
    private InformacionJuego informacionJuego;
    private Renderer objetoRenderer;
    public Material materialResaltado;
    private Material materialOriginal;
    public float velocidadAcercamiento;
    private Transform targetPlayer;

    void Start()
    {
        nombreDelObjeto = gameObject.name;

        objetoRenderer = GetComponent<Renderer>();
        if (objetoRenderer != null)
        {
            // Guardamos el material que tiene el objeto actualmente
            materialOriginal = objetoRenderer.material;
        }
        
        informacionJuego = FindObjectOfType<InformacionJuego>();

        if (informacionJuego == null)
        {
            Debug.LogError("Error: InformacionJuego no encontrado en la escena.");
        }
    }

    private void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            // Ocultar el mensaje de interacción antes de recoger.
            if (informacionJuego != null)
            {
                informacionJuego.SetMessage("", false); 
            }
            
            RecogerObjeto(); // Renombramos la función
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Persona"))
        {
            jugadorCerca = true;

            // OBTENER LA POSICIÓN DEL JUGADOR
            targetPlayer = other.transform;

            if (objetoRenderer != null && materialResaltado != null)
            {
                objetoRenderer.material = materialResaltado;
            }
            
            if (informacionJuego != null)
            {
                string mensajeCompleto = INTERACTION_PREFIX + nombreDelObjeto;
                informacionJuego.SetMessage(mensajeCompleto, true); 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Persona"))
        {
            jugadorCerca = false;

            if (objetoRenderer != null && materialOriginal != null)
            {
                objetoRenderer.material = materialOriginal;
            }
            
            if (informacionJuego != null)
            {
                informacionJuego.SetMessage("", false); 
            }
        }
    }

    private void RecogerObjeto()
{
        // ⭐️ NUEVO: Deshabilitamos el Collider para que no siga detectando al jugador ⭐️
        Collider objetoCollider = GetComponent<Collider>();
        if (objetoCollider != null)
        {
            objetoCollider.enabled = false;
        }

        // Si el jugador está cerca (targetPlayer está asignado), iniciamos el movimiento
        if (targetPlayer != null)
        {
            StartCoroutine(MoveToPlayer());
        }
}

    private IEnumerator MoveToPlayer()
    {
        // El bucle se ejecutará hasta que el objeto esté muy cerca del jugador
        while (Vector3.Distance(transform.position, targetPlayer.position) > 0.1f)
        {
            // Mueve el objeto hacia la posición del jugador usando Linear Interpolation (Lerp)
            transform.position = Vector3.Lerp(
                transform.position, 
                targetPlayer.position, 
                Time.deltaTime * velocidadAcercamiento
            );

            // Esto pausa la corutina por un frame, permitiendo que se mueva gradualmente
            yield return null; 
        }
        
        Destroy(gameObject);
    }
    
}
