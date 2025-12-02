using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuFin : MonoBehaviour
{
    public FindeJuego scriptMenuFin; 
    private const string TagDelJugador = "Persona"; 

    private bool haSidoActivado = false; 

    private void OnTriggerEnter(Collider other)
    {
        // 1. Comprobamos si es el Jugador y si aún no hemos activado el menú
        if (other.CompareTag(TagDelJugador) && !haSidoActivado)
        {
            if (scriptMenuFin != null)
            {
                haSidoActivado = true;
                
                scriptMenuFin.MostrarMenuFin(); 
            }
            else
            {
                Debug.LogError("¡ERROR! Falta asignar el script FindeJuego al componente MenuFin del portal en el Inspector.");
            }
        }
    }
}
