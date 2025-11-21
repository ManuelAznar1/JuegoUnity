using UnityEngine;
using UnityEngine.SceneManagement;

public class PasoDeEscena : MonoBehaviour
{
    // Nombre de la escena que contiene la barra de carga (Ej: "CargaIntermedia")
    public string nombreEscenaDeCarga; 

    // El objeto debe tener un Collider configurado como Trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Asegúrate de que solo el jugador pueda activar la transición
        if (other.CompareTag("Persona"))
        {
            CompletarNivel();
        }
    }

    void CompletarNivel()
    {
        Debug.Log("¡Objetivo alcanzado! Iniciando carga de siguiente nivel.");

        // Detener el cronómetro si existe y si lo deseas
        LogicaCronometro cronometro = FindObjectOfType<LogicaCronometro>();
        if (cronometro != null)
        {
            // Una función simple para detener el cronómetro (debe agregarse al script del cronómetro)
            // Aquí simplemente deshabilitamos el script si no quieres modificar el cronómetro.
            cronometro.enabled = false; 
        }

        // Asegurar que el tiempo de juego es normal para la transición
        Time.timeScale = 1f;
        
        // Cargar la escena de transición
        if (!string.IsNullOrEmpty(nombreEscenaDeCarga))
        {
            SceneManager.LoadScene(nombreEscenaDeCarga);
        }
        else
        {
            Debug.LogError("Nombre de Escena de Carga no asignado en el ObjetivoNivel.");
        }
    }
}
