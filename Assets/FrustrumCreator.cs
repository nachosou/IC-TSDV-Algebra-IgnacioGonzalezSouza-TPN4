using System.Collections;
using System.Collections.Generic;
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

    public Vector3 nearCenter;
    public Vector3 farCenter;

    private Vector3 farUpRightV;
    private Vector3 farUpLeftV;
    private Vector3 farDownRightV;
    private Vector3 farDownLeftV;

    private Vector3 nearUpRightV;
    private Vector3 nearUpLeftV;
    private Vector3 nearDownRightV;
    private Vector3 nearDownLeftV;

    public List<Vector3> vertexList = new List<Vector3>();

    void Start()
    {
        
    }

    void Update()
    {

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
}
