using UnityEngine;

public class AbrirPuertaHingeJoint : MonoBehaviour
{
    private HingeJoint bisagra;

    void Start()
    {
        bisagra = GetComponent<HingeJoint>();
        // Aseguramos que no haya motor funcionando al inicio
        bisagra.useMotor = false;
    }

    // Llama a este método cuando el jugador presione el botón (ej. 'E')
    public void Abrir()
    {
        // Configuramos el motor de la bisagra para que gire
        JointMotor motorDeApertura = bisagra.motor;
        
        // Velocidad con la que se abre la puerta (ajusta esto)
        motorDeApertura.targetVelocity = 100f; 
        
        // Fuerza máxima que aplica el motor para vencer la inercia (ajusta esto)
        motorDeApertura.force = 50f; 

        // Aplicamos el motor y activamos su uso
        bisagra.motor = motorDeApertura;
        bisagra.useMotor = true;
    }

    // Llama a esto para que la puerta se quede quieta (o para que se cierre sola)
    public void Detener()
    {
        bisagra.useMotor = false;
    }
}
