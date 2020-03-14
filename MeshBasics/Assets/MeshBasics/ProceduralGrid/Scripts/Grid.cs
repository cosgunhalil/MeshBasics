
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

        public void SetMaterial(Material gridMaterial)
        {
            gridRenderer.material = gridMaterial;
        }

        public void SetSize(Vector3 size)
        {
            xSize = (int)size.x;
            ySize = (int)size.y;
        }

        public void SetTangents(Vector4[] tangents)
        {
            mesh.tangents = tangents;
        }

        public int GetXSize()
        {
            return xSize;
        }

        public int GetYSize()
        {
            return ySize;
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

        public void SetMesh(Mesh mesh)
        {
            GetComponent<MeshFilter>().mesh = this.mesh = mesh;
        }

        public int GetVerticeCount()
        {
            return mesh.vertices.Length;
        }

        public void SetVertices(Vector3[] vertices)
        {
            mesh.vertices = vertices;
        }

        public void SetUV(Vector2[] uvVector)
        {
            mesh.uv = uvVector;
        }

        public void Configure()
        {
            mesh.RecalculateNormals();

            SetMaterialTiling();
        }

        public void SetTriangles(int[] triangles)
        {
            mesh.triangles = triangles;
        }

    }
}
