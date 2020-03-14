namespace MeshBasics
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface MeshBuilder
    {
        void CreateObject();
        void SetSize(Vector3 size);
        void BuildMesh();
        void SetVertices(Vector3[] vertices);
        void SetUV(Vector2[] uvVector);
        void SetTangents(Vector4[] tangents);
        void SetTriangles(int[] triangles);
        void ConfigureMesh();
    }
}


