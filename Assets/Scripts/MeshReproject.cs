using UnityEngine;
using UnityEngine.XR.WSA.WebCam;

public class MeshReproject : MonoBehaviour
{
    public Camera ProjectionPerspective;

    public float width = 0.2f;
    public float height = 0.2f;

    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs;

    void Start()
    {
        ProjectionPerspective = GameObject.Find("Main Camera").GetComponent<Camera>();
        ProjectionPerspective.enabled = false;
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.MarkDynamic();
    }

    void Update()
    {
        ProjectionPerspective.projectionMatrix = ManualProjectionMatrix(-width, width, -height, height, ProjectionPerspective.nearClipPlane, ProjectionPerspective.farClipPlane);

        vertices = mesh.vertices;
        uvs = new Vector2[vertices.Length];
        Vector2 ScreenPosition;
        for (int i = 0; i < uvs.Length; i++)
        {
            ScreenPosition = ProjectionPerspective.WorldToViewportPoint(transform.TransformPoint(vertices[i]));
            uvs[i].Set(ScreenPosition.x, ScreenPosition.y);
        }

        mesh.uv = uvs;
    }

    static Matrix4x4 ManualProjectionMatrix(float left, float right, float bottom, float top, float near, float far)
    {
        float x = 2.0F * near / (right - left);
        float y = 2.0F * near / (top - bottom);
        float a = (right + left) / (right - left);
        float b = (top + bottom) / (top - bottom);
        float c = -(far + near) / (far - near);
        float d = -(2.0F * far * near) / (far - near);
        float e = -1.0F;
        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = x;
        m[0, 1] = 0;
        m[0, 2] = a;
        m[0, 3] = 0;
        m[1, 0] = 0;
        m[1, 1] = y;
        m[1, 2] = b;
        m[1, 3] = 0;
        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = c;
        m[2, 3] = d;
        m[3, 0] = 0;
        m[3, 1] = 0;
        m[3, 2] = e;
        m[3, 3] = 0;
        return m;
    }
}