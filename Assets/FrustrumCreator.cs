using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FrustrumCreator : MonoBehaviour
{
    public float fov;
    private float vFov;

    public float farDist;
    public float nearDist;

    public int screenWidth;
    public int screenHeight;
    private float aspectRatio;

    public GameObject objectToCull;

    //El offset desde la posicion en la que esta
    private Vector3 nearCenter;
    private Vector3 farCenter;

    private Vector3 farUpRightV;
    private Vector3 farUpLeftV;
    private Vector3 farDownRightV;
    private Vector3 farDownLeftV;

    private Vector3 nearUpRightV;
    private Vector3 nearUpLeftV;
    private Vector3 nearDownRightV;
    private Vector3 nearDownLeftV;

    private List<Vector3> vertexList = new List<Vector3>();

    void Start()
    {
        AddVerticesToList();
    }

    void Update()
    {
        UpdateVertex();

        aspectRatio = (float)screenWidth / (float)screenHeight;

        vFov = fov / aspectRatio;

        nearCenter = transform.position + transform.forward * nearDist;
        farCenter = transform.position + transform.forward * farDist;

        //Calculo las posiciones de los vértices del near plane
        nearUpLeftV = new Vector3(Mathf.Tan((-fov / 2) * Mathf.Deg2Rad) * nearDist + nearCenter.x, Mathf.Tan((vFov / 2) * Mathf.Deg2Rad) * nearDist + nearCenter.y, nearCenter.z);
        nearUpRightV = new Vector3(Mathf.Tan((fov / 2) * Mathf.Deg2Rad) * nearDist + nearCenter.x, Mathf.Tan((vFov / 2) * Mathf.Deg2Rad) * nearDist + nearCenter.y, nearCenter.z);
        nearDownLeftV = new Vector3(Mathf.Tan((-fov / 2) * Mathf.Deg2Rad) * nearDist + nearCenter.x, Mathf.Tan((-vFov / 2) * Mathf.Deg2Rad) * nearDist + nearCenter.y, nearCenter.z);
        nearDownRightV = new Vector3(Mathf.Tan((fov / 2) * Mathf.Deg2Rad) * nearDist + nearCenter.x, Mathf.Tan((-vFov / 2) * Mathf.Deg2Rad) * nearDist + nearCenter.y, nearCenter.z);

        //Calculo las posiciones de los vértices del far plane
        farUpLeftV = new Vector3(Mathf.Tan((-fov / 2) * Mathf.Deg2Rad) * farDist + farCenter.x, Mathf.Tan((vFov / 2) * Mathf.Deg2Rad) * farDist + farCenter.y, farCenter.z);
        farUpRightV = new Vector3(Mathf.Tan((fov / 2) * Mathf.Deg2Rad) * farDist + farCenter.x, Mathf.Tan((vFov / 2) * Mathf.Deg2Rad) * farDist + farCenter.y, farCenter.z);
        farDownLeftV = new Vector3(Mathf.Tan((-fov / 2) * Mathf.Deg2Rad) * farDist + farCenter.x, Mathf.Tan((-vFov / 2) * Mathf.Deg2Rad) * farDist + farCenter.y, farCenter.z);
        farDownRightV = new Vector3(Mathf.Tan((fov / 2) * Mathf.Deg2Rad) * farDist + farCenter.x, Mathf.Tan((-vFov / 2) * Mathf.Deg2Rad) * farDist + farCenter.y, farCenter.z);
    }

    void AddVerticesToList()
    {
        //Triangulo superior
        vertexList.Add(transform.position);
        vertexList.Add(farUpRightV);
        vertexList.Add(farUpLeftV);

        //Triangulo derecho
        vertexList.Add(transform.position);
        vertexList.Add(farUpRightV);
        vertexList.Add(farDownRightV);

        //Triangulo inferior
        vertexList.Add(transform.position);
        vertexList.Add(farDownRightV);
        vertexList.Add(farDownLeftV);

        //Triangulo izquierdo
        vertexList.Add(transform.position);
        vertexList.Add(farUpLeftV);
        vertexList.Add(farDownLeftV);

        //Triangulo del far plane
        vertexList.Add(farUpRightV);
        vertexList.Add(farDownRightV);
        vertexList.Add(farDownLeftV);

        //Triangulo del near plane
        vertexList.Add(nearUpRightV);
        vertexList.Add(nearDownRightV);
        vertexList.Add(nearDownLeftV);
    }
   
    void UpdateVertex()
    {
        //Update triangles
        vertexList[0] = transform.position;
        vertexList[1] = farUpRightV;
        vertexList[2] = farUpLeftV;

        vertexList[3] = transform.position;
        vertexList[4] = farUpRightV;
        vertexList[5] = farDownRightV;

        vertexList[6] = transform.position;
        vertexList[7] = farDownRightV;
        vertexList[8] = farDownLeftV;

        vertexList[9] = transform.position;
        vertexList[10] = farUpLeftV;
        vertexList[11] = farDownLeftV;

        vertexList[12] = farUpRightV;
        vertexList[13] = farDownRightV;
        vertexList[14] = farDownLeftV;

        vertexList[15] = nearUpRightV;
        vertexList[16] = nearDownRightV;
        vertexList[17] = nearDownLeftV;
    }

    void DrawFrustum()
    {
        Gizmos.DrawLine(nearUpRightV, farUpRightV);
        Gizmos.DrawLine(nearUpLeftV, farUpLeftV);
        Gizmos.DrawLine(farUpRightV, farUpLeftV);
        Gizmos.DrawLine(nearUpRightV, nearUpLeftV);

        Gizmos.DrawLine(nearDownRightV, farDownRightV);
        Gizmos.DrawLine(nearDownLeftV, farDownLeftV);
        Gizmos.DrawLine(farDownRightV, farDownLeftV);
        Gizmos.DrawLine(nearDownRightV, nearDownLeftV);

        Gizmos.DrawLine(nearDownRightV, nearUpRightV);
        Gizmos.DrawLine(nearDownLeftV, nearUpLeftV);
        Gizmos.DrawLine(farDownRightV, farUpRightV);
        Gizmos.DrawLine(farDownLeftV, farUpLeftV);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        DrawFrustum();
    }
}
