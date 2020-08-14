using System;
using System.IO;
using UnityEngine; 
using UnityFBXExporter;

public class ConverterFBX : MonoBehaviour
{
    public void ClickSave()
    {
        try
        {
            GameObject roomMesh = GameObject.Find("Spatial Awareness System");
            if (roomMesh != null)
            {
                string path = Path.Combine(Application.persistentDataPath, "data");
                path = Path.Combine(path, roomMesh.name + ".fbx");

                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }

                FBXExporter.ExportGameObjToFBX(roomMesh, path, false, false);
                Debug.Log($"ASCII FBX file was created in {path}");
            }
            else
            {
                Debug.LogWarning("Mesh does not exist");
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
    }
}
