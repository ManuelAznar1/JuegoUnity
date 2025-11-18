using UnityEngine;

public class CambiarCamara : MonoBehaviour
{
// Asigna tus cámaras en el Inspector de Unity
    public GameObject mainCamera;
    public GameObject thirdPersonCamera;

    // Tecla para alternar entre las cámaras
    public KeyCode switchKey = KeyCode.C; 

    // Estado inicial: la cámara principal está activa por defecto
    private bool isMainCameraActive = true; 

    void Start()
    {
        // Asegúrate de que las referencias a las cámaras están asignadas
        if (mainCamera == null || thirdPersonCamera == null)
        {
            Debug.LogError("¡ERROR! Asegúrate de asignar las cámaras en el Inspector.");
            enabled = false; // Desactiva el script si falta una cámara
            return;
        }

        // Establece el estado inicial de las cámaras
        mainCamera.SetActive(true);
        thirdPersonCamera.SetActive(false);
    }

    void Update()
    {
        // Comprueba si se pulsa la tecla de cambio
        if (Input.GetKeyDown(switchKey))
        {
            // Llama a la función para alternar
            SwitchCameras();
        }
    }

    void SwitchCameras()
    {
        // Cambia el estado de la cámara principal y la de tercera persona
        isMainCameraActive = !isMainCameraActive;

        mainCamera.SetActive(isMainCameraActive);
        thirdPersonCamera.SetActive(!isMainCameraActive);

        // Opcional: muestra un mensaje en la consola
        if (isMainCameraActive)
        {
            Debug.Log("Cámara: Principal activada.");
        }
        else
        {
            Debug.Log("Cámara: Tercera Persona activada.");
        }
    }
}
