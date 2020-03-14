
namespace MeshBasics.ProceduralGrid
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GridBuilder : MonoBehaviour, MeshBuilder
    {
        public Material GridMaterial;
        private MeshBasics.ProceduralGrid.Grid grid;

        public void CreateObject()
        {
            var gridObject = new GameObject("Grid");
            gridObject.AddComponent<MeshFilter>();
            grid = gridObject.AddComponent<MeshBasics.ProceduralGrid.Grid>();

            grid.SetMaterial(GridMaterial);
        }

        public void BuildMesh()
        {
            grid.SetMesh(new Mesh());
        }

        public void SetSize(Vector3 size)
        {
            grid.SetSize(size);
        }

        public void SetTangents(Vector4[] tangents)
        {
            grid.SetTangents(tangents);
        }

        public void SetTriangles(int[] triangles)
        {
            grid.SetTriangles(triangles);
        }

        public void SetUV(Vector2[] uvVector)
        {
            grid.SetUV(uvVector);
        }

        public void SetVertices(Vector3[] vertices)
        {
            grid.SetVertices(vertices);
        }

        private Vector3[] GenerateVertices()
        {
            var vertices = new Vector3[(grid.GetXSize() + 1) * (grid.GetYSize() + 1)];

            for (int i = 0, y = 0; y <= grid.GetYSize(); y++)
            {
                for (int x = 0; x <= grid.GetXSize(); x++, i++)
                {
                    vertices[i] = new Vector3(x, y);
                }
            }

            return vertices;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                CreateObject();
                SetSize(new Vector3(UnityEngine.Random.Range(2,12),
                    UnityEngine.Random.Range(2, 12), 0));
                BuildMesh();
                SetVertices(GenerateVertices());
                SetUV(GenerateUVVector());
                SetTangents(GenerateTangents());
                SetTriangles(TriangulateVertices());
                ConfigureMesh();
            }
        }

        private Vector2[] GenerateUVVector()
        {
            var uv = new Vector2[grid.GetVerticeCount()];

            for (int i = 0, y = 0; y <= grid.GetYSize(); y++)
            {
                for (int x = 0; x <= grid.GetXSize(); x++, i++)
                {
                    uv[i] = new Vector2((float)x / grid.GetXSize(),
                        (float)y / grid.GetYSize());
                }
            }

            return uv;
        }

        private Vector4[] GenerateTangents()
        {
            Vector4[] tangents = new Vector4[grid.GetVerticeCount()];
            Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);

            for (int i = 0, y = 0; y <= grid.GetYSize(); y++)
            {
                for (int x = 0; x <= grid.GetXSize(); x++, i++)
                {
                    tangents[i] = tangent;
                }
            }

            return tangents;
        }

        private int[] TriangulateVertices()
        {
            int[] triangles = new int[grid.GetXSize() * grid.GetYSize() * 6];
            for (int ti = 0, vi = 0, y = 0; y < grid.GetYSize(); y++, vi++)
            {
                for (int x = 0; x < grid.GetXSize(); x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + grid.GetXSize() + 1;
                    triangles[ti + 5] = vi + grid.GetXSize() + 2;
                }
            }

            return triangles;
        }

        public void ConfigureMesh()
        {
            grid.Configure();
        }
    }
}


