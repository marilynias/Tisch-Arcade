using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Import_games : MonoBehaviour
{

    public Canvas parent;

    public GameObject bttn_prefab;



    // Start is called before the first frame update
    void Start()
    {
        bttn_prefab.transform.SetParent(parent.transform);

        string gamepath = Path.Combine(@"D:","Projects");
        UnityEngine.Debug.Log(gamepath);
        UnityEngine.Debug.Log(Directory.GetDirectories(gamepath));


        foreach (string folder in Directory.GetDirectories(gamepath)){
            UnityEngine.Debug.Log(Path.Combine(gamepath, folder));
            string[] filePaths = Directory.GetFiles(Path.Combine(gamepath, folder));
            UnityEngine.Debug.Log(filePaths);
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void start_Process()
    {
        string command = "notepad";
        ProcessStartInfo proc_start_info = new ProcessStartInfo("cmd", "/c " + command);
        proc_start_info.CreateNoWindow = true;
        Process proc = new System.Diagnostics.Process();
        proc.StartInfo = proc_start_info;
        proc.Start();
    }
}
