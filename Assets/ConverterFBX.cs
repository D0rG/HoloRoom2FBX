using System.IO;
using UnityEngine; 
using UnityFBXExporter;

public class ConverterFBX : MonoBehaviour
{
    [Header("For Spatial Awareness System")]
    [SerializeField] private GameObject objMeshToExport;   
    [SerializeField] private bool canGenerate = false;

    void Start()
    {
        if (canGenerate && objMeshToExport != null)
        {
            string path = Path.Combine(Application.persistentDataPath, "data");
            path = Path.Combine(path, objMeshToExport.name + ".fbx");

            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            FBXExporter.ExportGameObjToFBX(objMeshToExport, path, true, true);
            Debug.Log($"Файл {objMeshToExport.name}.fbx создан, в {path}");
        }
    }
}
