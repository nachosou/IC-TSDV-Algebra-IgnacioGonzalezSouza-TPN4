using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustrumCreator : MonoBehaviour
{
    // Variables de la cámara
    private Camera mainCamera;
    private Plane[] frustumPlanes;

    // Objetos en la escena
    public GameObject objectToCull;

    void Start()
    {
        // Obtener la cámara principal
        mainCamera = Camera.main;

        // Inicializar el array de planos del frustum
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
    }

    void Update()
    {
        // Actualizar los planos del frustum si la cámara cambia
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(mainCamera);

        // Mover la cámara con las teclas de flecha (esto es solo un ejemplo, puedes ajustarlo según tus necesidades)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        mainCamera.transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime);

        // Realizar culling en cada objeto

        if (IsObjectVisible(objectToCull))
        {
            objectToCull.SetActive(true);
        }
        else
        {
            objectToCull.SetActive(false);
        }

    }

    bool IsObjectVisible(GameObject obj)
    {
        // Obtener la caja alineada a los ejes que contiene el objeto
        Bounds objectBounds = obj.GetComponent<Renderer>().bounds;

        // Comprobar si la caja está dentro del frustum
        return GeometryUtility.TestPlanesAABB(frustumPlanes, objectBounds);
    }

    void OnDrawGizmos()
    {
        // Dibujar los planos del frustum como líneas en la escena
        Gizmos.color = Color.red;

        Matrix4x4 matrix = Matrix4x4.identity;
        Gizmos.matrix = matrix;

        if (frustumPlanes != null)
        {
            foreach (var plane in frustumPlanes)
            {
                DrawPlane(plane);
            }
        }
    }

    void DrawPlane(Plane plane)
    {
        Vector3 normal = plane.normal;
        float d = plane.distance;

        Vector3 p0 = new Vector3(-10, (-d - normal.x * -10 - normal.y * -10) / normal.z, -10);
        Vector3 p1 = new Vector3(10, (-d - normal.x * 10 - normal.y * -10) / normal.z, -10);
        Vector3 p2 = new Vector3(10, (-d - normal.x * 10 - normal.y * 10) / normal.z, 10);
        Vector3 p3 = new Vector3(-10, (-d - normal.x * -10 - normal.y * 10) / normal.z, 10);

        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);
    }
}
