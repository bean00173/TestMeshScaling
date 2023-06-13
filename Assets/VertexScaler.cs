using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VertexScaler : MonoBehaviour
{
    public float minWidth;
    public float maxWidth;

    public bool recalculateNormals;

    float count;

    Vector3[] vertices;
    Mesh mesh;
    List<Vector3> posPoints = new List<Vector3>();
    List<Vector3> negPoints = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        mesh = this.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        SortVertices();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SortVertices()
    {
        //vertices = vertices.OrderBy(x => x.z).ToArray();

        foreach (Vector3 vector in vertices)
        {
            Debug.Log(vector);
        }

        //float a = Mathf.Abs(vertices[vertices.Count() - 1].x - vertices[0].x);
        //float b = minWidth = maxWidth;
        //float h = Mathf.Sqrt(Mathf.Pow(a, 2) + Mathf.Pow(b, 2));

        float h = (maxWidth - minWidth) / CountUnique();

        Debug.Log(h);

        foreach(Vector3 vertex in vertices)
        {
            if(vertex.x > 0)
            {
                StorePoint(vertex, true);
            }
            else if (vertex.x < 0)
            {
                StorePoint(vertex, false);
            }
            //Debug.Log("Working");
        }
        int a = 1;
        foreach (Vector3 posPoint in posPoints)
        {
            if (vertices.Contains(posPoint))
            {
                int index = System.Array.IndexOf(vertices, posPoint);
                vertices[index].z += (h * a);

                //Debug.Log("Working");
            }
        }
        int b = 1;
        foreach (Vector3 negPoint in negPoints)
        {
            if (vertices.Contains(negPoint) && negPoint.z < 0)
            {
                int index = System.Array.IndexOf(vertices, negPoint);
                vertices[index].z -= (h * b);

                //Debug.Log("Working");
            }
        }

        mesh.vertices = vertices;

        if (recalculateNormals)
        {
            mesh.RecalculateNormals();
        }
            
        mesh.RecalculateBounds();

    }

    private float CountUnique()
    {
        float[] counts = new float[0];
        foreach(Vector3 vertex in vertices)
        {
            if (!counts.Contains(vertex.z))
            {
                counts.Append(vertex.z);  // COUNT NOT PROPER BRUH
            }
        }

        Debug.Log(count);

        return count;

    }

    private void StorePoint(Vector3 point, bool con)
    {

        if (con)
        {
            posPoints.Add(point);
        }
        else
        {
            negPoints.Add(point);
        }
        

    }
}
