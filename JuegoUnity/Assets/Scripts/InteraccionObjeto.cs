using UnityEngine;

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

    void Start()
    {
        nombreDelObjeto = gameObject.name;
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
            
            if (informacionJuego != null)
            {
                // ⭐️ CREAR MENSAJE DE INTERACCIÓN DINÁMICA ⭐️
                // Resultado: "Pulsa [E] para recoger el Objeto"
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
            
            if (informacionJuego != null)
            {
                informacionJuego.SetMessage("", false); 
            }
        }
    }

    private void RecogerObjeto()
    {
        Destroy(gameObject);
    }
    
}
