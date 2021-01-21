using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.MixedReality.Toolkit;
using UnityEngine; 
using UnityFBXExporter;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

public class ConverterFBX : MonoBehaviour
{

    object sync = new object();
    List<Action> actions = new List<Action>();
    Thread testThread = null;

    void Update()
    {
        lock (sync)
        {
            while (actions.Count != 0)
            {
                actions[0].Invoke();
                actions.RemoveAt(0);
            }
        }
    }

    public void Execute(Action action)
    {
        lock (sync)
        {
            actions.Add(action);
        }

        try
        {
            Thread.Sleep(10000);
        }
        catch (ThreadInterruptedException)
        {
        }
        finally
        {
        }
    }

    public void ClickSave()
    {
        Action threadAction = () =>
        {
            Debug.Log("Тык");
            GameObject mesh = null;
            string meshName = null;
            string path = null;

            Execute(() =>   //Будет выполено в мейнтреде
            {
                mesh = GameObject.Find("Spatial Awareness System");
                meshName = mesh.name;
                path = Path.Combine(Application.persistentDataPath, "data");
                testThread.Interrupt();
            });

            path = Path.Combine(path, meshName + ".fbx");

            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            
            Execute(() =>
            {
                FBXExporter.ExportGameObjToFBX(mesh, path, false, false);
                testThread.Interrupt();
            });
        };

        testThread = new Thread(new ThreadStart(threadAction));
        testThread.Start();
    }
}
