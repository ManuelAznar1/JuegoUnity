using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaCargaEscena : MonoBehaviour
{
    public string sceneLoadName;       
    public TextMeshProUGUI textProgress; 
    public Slider sliderProgress; 

    private void Start()
    {
        // Paso 1: Descongelar el juego por si acaso
        Time.timeScale = 1f; 

        // Paso 2: Comprobar referencias
        if (sliderProgress == null || textProgress == null)
        {
            Debug.LogError("⛔ ERROR: ¡Arrastra el Slider y el Texto al script en el Inspector!");
            return;
        }

        StartCoroutine(LoadScene(sceneLoadName));
    }

    public IEnumerator LoadScene(string nameToLoad)
    {
        // Paso 3: Configurar Slider forzosamente
        sliderProgress.minValue = 0f;
        sliderProgress.maxValue = 1f;
        sliderProgress.value = 0f;
        textProgress.text = "Loading.. 0%";

        // Iniciamos carga en segundo plano (silenciosa)
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(nameToLoad);
        if (loadAsync == null)
        {
            Debug.LogError("⛔ ERROR: La escena '" + nameToLoad + "' no existe en Build Settings.");
            yield break;
        }
        loadAsync.allowSceneActivation = false; 

        // ********** MOVIMIENTO DE BARRA (SIMULADO) **********
        float tiempoTotal = 5f; // 5 Segundos
        float cronometro = 0f;

        Debug.Log("🟢 Iniciando animación de barra...");

        while (cronometro < tiempoTotal)
        {
            // Sumamos tiempo (usando unscaledDeltaTime por si hay lag o pausa)
            cronometro += Time.unscaledDeltaTime;

            // Calculamos porcentaje (0.0 a 1.0)
            float porcentaje = Mathf.Clamp01(cronometro / tiempoTotal);

            // Aplicamos valor
            sliderProgress.value = porcentaje;
            textProgress.text = "Loading.. " + (porcentaje * 100).ToString("F0") + "%";

            // MENSAJE DE DEPURACIÓN (Mira la consola de Unity)
            // Si ves esto, el código funciona y el fallo es el Slider visual.
            // Debug.Log("Progreso: " + porcentaje); 

            yield return null;
        }
        // ****************************************************

        // Finalizar
        sliderProgress.value = 1f;
        textProgress.text = "Loading.. 100%";
        
        Debug.Log("🟢 Tiempo cumplido. Esperando carga real...");

        // Esperar a que la carga real termine (si el PC es lento)
        while (loadAsync.progress < 0.9f)
        {
            yield return null;
        }

        // Activar escena
        loadAsync.allowSceneActivation = true;
    }
}
