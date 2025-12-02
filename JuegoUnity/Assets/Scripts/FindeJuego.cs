using UnityEngine;
using UnityEngine.SceneManagement;

public class FindeJuego : MonoBehaviour
{
    public GameObject objetoMenuFinDeJuego; 
    public Behaviour ControlJugador; 
    public Behaviour Camara;         

    public void MostrarMenuFin()
    {
        // Muestra el menú
        if (objetoMenuFinDeJuego != null)
        {
            objetoMenuFinDeJuego.SetActive(true); 
        }
        
        // Congela el tiempo
        Time.timeScale = 0f;                  

        // Libera el ratón
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Detiene los scripts del jugador/cámara
        DesactivarScriptsDeMovimiento();

        Debug.Log("Juego Terminado. Menú de Fin de Juego mostrado.");
    }

    private void DesactivarScriptsDeMovimiento()
    {
        if (ControlJugador != null)
            ControlJugador.enabled = false; 

        if (Camara != null)
            Camara.enabled = false;
    }
    public void VolverAlMenuPrincipal()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MenuPrincipal"); 
    }
}
