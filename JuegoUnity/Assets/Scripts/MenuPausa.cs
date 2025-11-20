using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject objetoMenuPausa; // Referencia al panel visual
    public bool juegoPausado = false;  // Variable para saber si estamos en pausa
    public MovimientoCamara scriptCamara;
    public MovimientoCamara scriptCamara3P;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        objetoMenuPausa.SetActive(false); // Oculta el menú
        Time.timeScale = 1f;              // El tiempo vuelve a la normalidad (1 = tiempo real)
        juegoPausado = false;

        // Opcional: Si tu juego oculta el ratón, actívalo de nuevo aquí
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Reactivar la cámara 1ª persona
        if (scriptCamara != null) 
            scriptCamara.enabled = true;

        // Reactivar la cámara 3ª Persona
        if (scriptCamara3P != null) 
            scriptCamara3P.enabled = true;
    }

    void Pausar()
    {
        objetoMenuPausa.SetActive(true);  // Muestra el menú
        Time.timeScale = 0f;              // Congela el tiempo (0 = parado)
        juegoPausado = true;

        // Libera el ratón para poder hacer clic en los botones
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Desactivar la cámara 1ª persona
        if (scriptCamara != null) {}
            scriptCamara.enabled = false;

        // Desactivar la cámara 3ª persona
        if (scriptCamara3P != null) {}
            scriptCamara3P.enabled = false;
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        // 1. Descongelamos el tiempo (¡Vital!)
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
