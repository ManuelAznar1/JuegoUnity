using UnityEngine;
using UnityEngine.UI;

public class InformacionJuego : MonoBehaviour
{
    // Asigna aquí el componente TextMeshProUGUI que quieres usar para mostrar mensajes
    public Text mensajeTexto; 

    // Referencia a la Corutina para detener mensajes anteriores
    private Coroutine mensajeActual;

    // Método que LogicaLlave llama
    public void ShowMessage(string message, float duration)
    {
        // Si hay un mensaje anterior, lo detenemos primero.
        if (mensajeActual != null)
        {
            StopCoroutine(mensajeActual);
        }
        
        // Iniciamos la nueva corutina para mostrar el mensaje
        mensajeActual = StartCoroutine(DisplayMessage(message, duration));
    }

    private System.Collections.IEnumerator DisplayMessage(string message, float duration)
    {
        // 1. Mostrar el texto.
        if (mensajeTexto != null)
        {
            mensajeTexto.text = message;
            mensajeTexto.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("La llave fue recogida, pero 'mensajeTexto' no está asignado en InformacionJuego.");
        }

        // 2. Esperar la duración especificada.
        yield return new WaitForSeconds(duration);

        // 3. Ocultar el texto.
        if (mensajeTexto != null)
        {
            mensajeTexto.gameObject.SetActive(false);
        }
    }

    public void SetMessage(string message, bool isVisible)
    {
        if (mensajeTexto != null)
        {
            mensajeTexto.text = message;
            mensajeTexto.gameObject.SetActive(isVisible);
        }
    }
}
