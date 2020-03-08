
namespace MeshBasics.ProceduralGrid
{
    using System;
    using System.Collections;
    using UnityEngine;

    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Grid : MonoBehaviour
    {
        private int xSize;
        private int ySize;
        private Mesh mesh;
        private Vector3[] vertices;
        private Renderer gridRenderer;

        void Awake()
        {
            gridRenderer = GetComponent<Renderer>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Build();
            }
        }

        private void Build()
        {
            SetSize(UnityEngine.Random.Range(1,10),
                UnityEngine.Random.Range(1,10));

            BuildMesh();
            SetVertices();
            SetUV();
            SetTangents();
            SetTriangles();

            mesh.RecalculateNormals();

            SetMaterialTiling();
        }

        private void SetMaterialTiling()
        {
            this.gridRenderer.sharedMaterial.SetTextureScale("_MainTex", new Vector2(xSize, ySize));
        }

        public void SetSize(int xSize, int ySize)
        {
            this.xSize = xSize;
            this.ySize = ySize;
        }

        private void BuildMesh()
        {
            GetComponent<MeshFilter>().mesh = mesh = new Mesh();
            mesh.name = "Procedural Grid";
        }

        private void SetVertices()
        {
            GenerateVertices();
            mesh.vertices = vertices;
        }

        private void GenerateVertices()
        {
            vertices = new Vector3[(xSize + 1) * (ySize + 1)];

            for (int i = 0, y = 0; y <= ySize; y++)
            {
                for (int x = 0; x <= xSize; x++, i++)
                {
                    vertices[i] = new Vector3(x, y);
                }
            }
        }

        private void SetUV()
        {
            mesh.uv = GenerateUV();
        }

        private Vector2[] GenerateUV()
        {
            Vector2[] uv = new Vector2[vertices.Length];

            for (int i = 0, y = 0; y <= ySize; y++)
            {
                for (int x = 0; x <= xSize; x++, i++)
                {
                    uv[i] = new Vector2((float)x / xSize, (float)y / ySize);
                }
            }

            return uv;

        }

        private void SetTangents()
        {
            mesh.tangents = GenerateTangents();
        }

        private Vector4[] GenerateTangents()
        {
            Vector4[] tangents = new Vector4[vertices.Length];
            Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

            for (int i = 0, y = 0; y <= ySize; y++)
            {
                for (int x = 0; x <= xSize; x++, i++)
                {
                    tangents[i] = tangent;
                }
            }

            return tangents;
        }

        private void SetTriangles()
        {
            mesh.triangles = TriangulateVertices();
        }

        private int[] TriangulateVertices()
        {
            int[] triangles = new int[xSize * ySize * 6];
            for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
            {
                for (int x = 0; x < xSize; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                    triangles[ti + 5] = vi + xSize + 2;
                }
            }

            return triangles;
        }
    }
}
