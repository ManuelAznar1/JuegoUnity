using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
[SerializeField]
    public float sensitivity = 5.0f;
    [SerializeField]
    public float smoothing = 2.0f;
    // Nuevas variables para limitar el ángulo vertical
    [SerializeField]
    public float minY = -90f; // Límite inferior (mirar hacia abajo)
    [SerializeField]
    public float maxY = 90f;  // Límite superior (mirar hacia arriba)
    
    // the chacter is the capsule
    public GameObject character;
    // get the incremental value of mouse moving
    private Vector2 mouseLook;
    // smooth the mouse moving
    private Vector2 smoothV;

	// Use this for initialization
	void Start () {
        character = this.transform.parent.gameObject;
        // Opcional: Ocultar y bloquear el cursor
        Cursor.lockState = CursorLockMode.Locked; 
	}
	
	// Update is called once per frame
	void Update () {
        // md is mosue delta
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        
        // the interpolated float result between the two float values
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        
        // incrementally add to the camera look
        mouseLook += smoothV;

        // **PASO CLAVE: Limitar el ángulo vertical (eje Y de mouseLook)**
        mouseLook.y = Mathf.Clamp(mouseLook.y, minY, maxY); //

        // vector3.right means the x-axis (rotación vertical de la cámara)
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        
        // Rotación horizontal del personaje/cuerpo (eje X de mouseLook)
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
