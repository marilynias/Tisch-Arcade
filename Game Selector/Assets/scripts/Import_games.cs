using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Import_games : MonoBehaviour
{

    public Canvas parent;

    public GameObject bttn_prefab;



    // Start is called before the first frame update
    void Start()
    {
        bttn_prefab.transform.SetParent(parent.transform);
        //string bttn_prefab.filepath = "test";


        string gamepath = Path.Combine(@"P:", "Python");

        int posx = 0;
        int posy = 20;

        //UnityEngine.Debug.Log(gamepath);
        //UnityEngine.Debug.Log(Directory.GetDirectories(gamepath));


        foreach (string folder in Directory.GetDirectories(gamepath)){
            //UnityEngine.Debug.Log(Path.Combine(gamepath, folder));
            foreach(string file in Directory.GetFiles(Path.Combine(gamepath, folder)))
            {
                posy -= 6;
                string path = Path.Combine(gamepath, folder, file);
                UnityEngine.Debug.Log(file);
                GameObject bttn = Instantiate(bttn_prefab, parent.transform);
                bttn.GetComponentInChildren<Text>().text = file;
                Button btn = bttn.GetComponent<Button>();
                btn.onClick.AddListener(delegate { start_Process(file); });
                bttn.transform.position = new Vector3(0, posy, posx);
            }
            //string[] filePaths = Directory.GetFiles(Path.Combine(gamepath, folder));
            
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void start_Process(string path)
    {
        UnityEngine.Debug.Log("executing " + path);
        string command = "notepad";
        ProcessStartInfo proc_start_info = new ProcessStartInfo("cmd", "/c " + command);
        proc_start_info.CreateNoWindow = true;
        Process proc = new System.Diagnostics.Process();
        proc.StartInfo = proc_start_info;
        proc.Start();
    }
}
