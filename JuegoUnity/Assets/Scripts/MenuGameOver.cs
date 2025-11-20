using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGameOver : MonoBehaviour
{
    // Conexión al componente Text (Legacy) de la UI que muestra el tiempo.
    public Text textoTiempo;

    // Referencia al objeto Panel (GameObject) de la pantalla de Game Over.
    public GameObject pantallaGameOver;

    // Función para actualizar el texto en pantalla en formato MM:SS.FF
    void ActualizarDisplayTiempo(float tiempoActual)
    {
        if (textoTiempo == null) return;

        float minutos = Mathf.FloorToInt(tiempoActual / 60);
        float segundos = Mathf.FloorToInt(tiempoActual % 60);
        float milisegundos = Mathf.FloorToInt((tiempoActual * 100) % 100);

        // Formato: MM:SS.FF
        string textoFormateado = string.Format("{0:00}:{1:00}.{2:00}", minutos, segundos, milisegundos);

        textoTiempo.text = textoFormateado;
    }
    public void ReiniciarJuego()
    {
        // 1. Descongelar el juego. Es fundamental para que la nueva escena corra.
        Time.timeScale = 1f;

        // 2. Recargar la escena actual, lo que reinicia el juego.
        int escenaActualIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(escenaActualIndex);

        // 3. Bloquear el ratón si la escena lo requiere.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void SalirAlMenuPrincipal()
    {
        Debug.Log("Saliendo al menú principal...");

        // 1. Descongelar el tiempo.
        Time.timeScale = 1f;

        // 2. Cargar la escena del menú principal.
        SceneManager.LoadScene("MenuPrincipal");

        // 3. Liberar el ratón para interactuar con el menú.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
