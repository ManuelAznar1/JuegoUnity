using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScriptMenu : MonoBehaviour
{
    public static ScriptMenu instance;

    // Aquí definimos fijo el nombre de tu escena para evitar errores
    private const string MainGameSceneName = "Nivel1";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- FUNCIÓN PRINCIPAL (Conectar esta al Botón Jugar) ---

    public void StartGame()
    {
        // Forzamos que cargue "Nivel1" directamente
        StartCoroutine(LoadAsyncScene(MainGameSceneName));
    }

    // --- FUNCIÓN SALIR ---

    public void QuitGame()
    {
        Debug.Log("Saliendo del Juego...");
#if UNITY_STANDALONE || UNITY_WEBGL
        Application.Quit();
#elif UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // --- LÓGICA INTERNA DE CARGA (No tocar) ---

    IEnumerator LoadAsyncScene(string sceneToLoad)
    {
        Debug.Log("Iniciando carga asíncrona de: " + sceneToLoad);

        // Verificación de seguridad: ¿Existe la escena en Build Settings?
        if (Application.CanStreamedLevelBeLoaded(sceneToLoad))
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

            while (!operation.isDone)
            {
                yield return null;
            }

            // Al terminar, nos autodestruimos para no molestar en el Nivel 1
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("ERROR CRÍTICO: La escena '" + sceneToLoad + "' no está añadida en 'Build Settings'.");
        }
    }
}