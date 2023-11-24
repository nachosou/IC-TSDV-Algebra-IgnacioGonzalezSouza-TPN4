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
                DrawPlane(plane, mainCamera);
            }
        }
    }

    //void DrawPlane(Plane plane, Camera camera)
    //{
    //    Vector3 normal = plane.normal;
    //    float d = plane.distance;

    //    // Obtén la posición de la cámara
    //    Vector3 cameraPosition = camera.transform.position;

    //    // Ajusta las coordenadas del plano en función de la posición de la cámara
    //    Vector3 p0 = new Vector3(-10, (-d - normal.x * -10 - normal.y * -10) / normal.z, -10) + cameraPosition;
    //    Vector3 p1 = new Vector3(10, (-d - normal.x * 10 - normal.y * -10) / normal.z, -10) + cameraPosition;
    //    Vector3 p2 = new Vector3(10, (-d - normal.x * 10 - normal.y * 10) / normal.z, 10) + cameraPosition;
    //    Vector3 p3 = new Vector3(-10, (-d - normal.x * -10 - normal.y * 10) / normal.z, 10) + cameraPosition;

    //    // Dibuja el plano ajustado a la posición de la cámara
    //    Gizmos.DrawLine(p0, p1);
    //    Gizmos.DrawLine(p1, p2);
    //    Gizmos.DrawLine(p2, p3);
    //    Gizmos.DrawLine(p3, p0);
    //}

    void DrawPlane(Plane plane, Camera camera)
    {
        Vector3 normal = plane.normal;
        float d = plane.distance;

        // Obtén la posición de la cámara
        Vector3 cameraPosition = camera.transform.position;

        // Ajusta las coordenadas del plano en función de la posición de la cámara
        Vector3 p0 = new Vector3(-10, cameraPosition.y + (-d - normal.y * -10 - normal.z * -10) / normal.x, -10) + new Vector3(cameraPosition.x, 0, cameraPosition.z);
        Vector3 p1 = new Vector3(10, cameraPosition.y + (-d - normal.y * 10 - normal.z * -10) / normal.x, -10) + new Vector3(cameraPosition.x, 0, cameraPosition.z);
        Vector3 p2 = new Vector3(10, cameraPosition.y + (-d - normal.y * 10 - normal.z * 10) / normal.x, 10) + new Vector3(cameraPosition.x, 0, cameraPosition.z);
        Vector3 p3 = new Vector3(-10, cameraPosition.y + (-d - normal.y * -10 - normal.z * 10) / normal.x, 10) + new Vector3(cameraPosition.x, 0, cameraPosition.z);

        // Dibuja el plano ajustado a la posición de la cámara
        Gizmos.DrawLine(p0, p1); //bottom Line
        Gizmos.DrawLine(p1, p2); //right Line
        Gizmos.DrawLine(p2, p3); //top Line
        Gizmos.DrawLine(p3, p0); //left Line

        Vector3 p4 = new Vector3(10, p1.y, p3.z);
        Vector3 p5 = new Vector3(-10, p0.y, p2.z);

        Gizmos.DrawLine(p2, p4);

        Gizmos.DrawLine(p3, p5);

        //float halfWidth = Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad) * camera.nearClipPlane;
        //float halfHeight = halfWidth / camera.aspect;

        //Vector3 right = camera.transform.right * halfWidth;
        //Vector3 up = camera.transform.up * halfHeight;
        //Vector3 forward = camera.transform.forward * camera.nearClipPlane;

        //Vector3 topLeft = camera.transform.position + forward - right + up;
        //Vector3 topRight = camera.transform.position + forward + right + up;
        //Vector3 bottomLeft = camera.transform.position + forward - right - up;
        //Vector3 bottomRight = camera.transform.position + forward + right - up;

        //Gizmos.DrawLine(topLeft, bottomLeft); // Connect top-left with bottom-left
        //Gizmos.DrawLine(topRight, bottomRight); // Connect top-right with bottom-right
        //Gizmos.DrawLine(topLeft, topRight); // Connect top-left with top-right
        //Gizmos.DrawLine(bottomLeft, bottomRight); // Connect bottom-left with bottom-right

        //float nearDistance = camera.nearClipPlane;
        //float farDistance = camera.farClipPlane;

        //float aspectRatio = camera.aspect;
        //float verticalFOV = camera.fieldOfView * Mathf.Deg2Rad;
        //float horizontalFOV = 2f * Mathf.Atan(Mathf.Tan(verticalFOV * 0.5f) * aspectRatio);

        //float halfWidthNear = nearDistance * Mathf.Tan(horizontalFOV * 0.5f);
        //float halfHeightNear = nearDistance * Mathf.Tan(verticalFOV * 0.5f);

        //float halfWidthFar = farDistance * Mathf.Tan(horizontalFOV * 0.5f);
        //float halfHeightFar = farDistance * Mathf.Tan(verticalFOV * 0.5f);

        //Vector3 cameraForward = camera.transform.forward;
        //Vector3 cameraUp = camera.transform.up;
        //Vector3 cameraRight = camera.transform.right;

        //Vector3 topLeftNear = cameraPosition + (cameraForward * nearDistance) - (cameraRight * halfWidthNear) + (cameraUp * halfHeightNear);
        //Vector3 bottomLeftNear = cameraPosition + (cameraForward * nearDistance) - (cameraRight * halfWidthNear) - (cameraUp * halfHeightNear);
        //Vector3 topRightNear = cameraPosition + (cameraForward * nearDistance) + (cameraRight * halfWidthNear) + (cameraUp * halfHeightNear);
        //Vector3 bottomRightNear = cameraPosition + (cameraForward * nearDistance) + (cameraRight * halfWidthNear) - (cameraUp * halfHeightNear);

        //Vector3 topLeftFar = cameraPosition + (cameraForward * farDistance) - (cameraRight * halfWidthFar) + (cameraUp * halfHeightFar);
        //Vector3 bottomLeftFar = cameraPosition + (cameraForward * farDistance) - (cameraRight * halfWidthFar) - (cameraUp * halfHeightFar);
        //Vector3 topRightFar = cameraPosition + (cameraForward * farDistance) + (cameraRight * halfWidthFar) + (cameraUp * halfHeightFar);
        //Vector3 bottomRightFar = cameraPosition + (cameraForward * farDistance) + (cameraRight * halfWidthFar) - (cameraUp * halfHeightFar);

        //Gizmos.DrawLine(topLeftNear, bottomLeftNear);
        //Gizmos.DrawLine(topRightNear, bottomRightNear);
        //Gizmos.DrawLine(topLeftFar, bottomLeftFar);
        //Gizmos.DrawLine(topRightFar, bottomRightFar);

        //Gizmos.DrawLine(topLeftNear, topLeftFar);
        //Gizmos.DrawLine(bottomLeftNear, bottomLeftFar);
        //Gizmos.DrawLine(topRightNear, topRightFar);
        //Gizmos.DrawLine(bottomRightNear, bottomRightFar);
    }
}
