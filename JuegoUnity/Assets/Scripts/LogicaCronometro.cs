using UnityEngine;
using UnityEngine.UI;

public class LogicaCronometro : MonoBehaviour
{
    // Conexión a la UI
    public Text textoTiempo; 
    public GameObject pantallaGameOver; 

    // --- ¡NUEVAS REFERENCIAS PARA CONGELAR! (Asignar en el Inspector) ---
    // Debes arrastrar los scripts de movimiento (MovimientoPersonaje, MovimientoCamara, etc.) aquí.
    public MovimientoPersonaje scriptMovimientoJugador; 
    public MovimientoCamara scriptMovimientoCamara; // Por ejemplo, el script de la cámara 1P
    public MovimientoCamara scriptMovimientoCamara3P; // Por ejemplo, el script de la cámara 3P
    // ----------------------------------------------------------------------
    
    // **TIEMPO DE EJEMPLO:** 120 segundos (2 minutos).
    private const float TIEMPO_MAXIMO = 10f; 
    
    private float tiempoRestante; 
    private bool estaCorriendo = false; 

    void Start()
    {
        tiempoRestante = TIEMPO_MAXIMO; 
        estaCorriendo = true; 
        
        // El juego comienza sin pausar y sin Game Over.
        Time.timeScale = 1f;
        if (pantallaGameOver != null)
        {
            pantallaGameOver.SetActive(false);
        }

        if (textoTiempo == null)
        {
            Debug.LogError("¡El objeto Text del cronómetro no ha sido asignado al script!");
            estaCorriendo = false; 
        }

        ActualizarDisplayTiempo(tiempoRestante);
    }

    void Update()
    {
        if (estaCorriendo)
        {
            if (tiempoRestante > 0)
            {
                // Resta el tiempo transcurrido desde el último frame.
                tiempoRestante -= Time.deltaTime;
                
                if (tiempoRestante < 0)
                {
                    tiempoRestante = 0;
                }
                
                ActualizarDisplayTiempo(tiempoRestante);
            }
            else
            {
                // Lógica de Game Over: se ejecuta solo una vez al llegar a cero.
                if (estaCorriendo)
                {
                    estaCorriendo = false; // Detener el cronómetro
                    Debug.Log("¡Tiempo terminado! Congelando juego y movimientos.");
                    
                    // 1. CONGELAR MOVIMIENTO DESACTIVANDO SCRIPTS (FREEZE)
                    if (scriptMovimientoJugador != null) scriptMovimientoJugador.enabled = false;
                    if (scriptMovimientoCamara != null) scriptMovimientoCamara.enabled = false;
                    if (scriptMovimientoCamara3P != null) scriptMovimientoCamara3P.enabled = false;

                    // 2. CONGELAR TIEMPO (Física, Animaciones, etc.)
                    Time.timeScale = 0f; 

                    // 3. Activar la pantalla de Game Over
                    if (pantallaGameOver != null)
                    {
                        pantallaGameOver.SetActive(true);
                    }
                    
                    // 4. Liberar el ratón para los botones
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
    }

    // Función para actualizar el texto en pantalla.
    void ActualizarDisplayTiempo(float tiempoActual)
    {
        if (textoTiempo == null) return; 

        // Calculamos minutos, segundos y milisegundos para el display.
        float minutos = Mathf.FloorToInt(tiempoActual / 60); 
        float segundos = Mathf.FloorToInt(tiempoActual % 60); 
        float milisegundos = Mathf.FloorToInt((tiempoActual * 100) % 100); 

        // Formato: MM:SS.FF (ej: 02:00.00)
        string textoFormateado = string.Format("{0:00}:{1:00}.{2:00}", minutos, segundos, milisegundos);

        textoTiempo.text = textoFormateado;
    }
}
