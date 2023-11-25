using UnityEngine;

public class Model : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public bool IsPointInsideModel(Vector3 point)
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh; //Es una funcion que busca dentro del objeto donde esta este script y busca un componente "Mesh Filter"
                                                     //-que es una mesh-

        Vector3[] vertices = mesh.vertices; //Retorna todos los vertices de la figura, puntos en el espacio de 3 coordenadas 
        int[] triangles = mesh.triangles; //Indice de los vertices que conforman cada triangulo, cada 3 hay un triangulo diferente almacenado

        for (int i = 0; i < triangles.Length; i += 3) //Se va avanzando de a 3 numeros porque quiero avanzar de a 1 triangulo, almacena los 3 vertices que forman cada uno
        {                                             //La cantidad de triangulos que tengo es la cantidad de veces que quiero iterar

            //Accedo en el array de vértices a los vértices que me forman cada triangulo que estoy interando, ya que en triangles yo tengo alcamenado el
            //index de cada uno de esos vertices en el array de vertices

            //Necesito los vértices para poder calcular la normal
            Vector3 vertex1 = transform.TransformPoint(vertices[triangles[i]]); 
            Vector3 vertex2 = transform.TransformPoint(vertices[triangles[i + 1]]);
            Vector3 vertex3 = transform.TransformPoint(vertices[triangles[i + 2]]);

            Vector3 normal = Vector3.Cross(vertex2 - vertex1, vertex3 - vertex1).normalized; //Las normales estan hacia afuera de la figura, me definen "el exterior"
            //En el contexto de un triángulo la normal del plano se obtiene mediante el producto cruz de dos vectores que pertenecen al plano.
            //El orden de los factores dentro del producto cruz define hacia que lado se encuentra la normal porque es producto matricial. 

            if (Vector3.Dot(normal, point - vertex1) > 0) //Si el resultado del producto punto es mayor que 0, significa que los dos vectores están
                                                          //en el mismo lado de un plano, y como la normal esta fuera de la figura significa que el punto chequeado
                                                          //tambien lo esta. 
            {
                return false;
            }
        }

        return true; //Si es igual a 0, están en planos perpendiculares, y si es menor que 0 están en lados opuestos del plano, es decir, que esta dentro de
                     //la figura.
    }
}
