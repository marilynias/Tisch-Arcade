using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
                //string path = Path.Combine(gamepath, folder, file);
                string[] directories = file.Split(Path.DirectorySeparatorChar);
                //var last = directories.Last();
                //UnityEngine.Debug.Log(directories[2]);
                //string filename = file[1];
                GameObject bttn = Instantiate(bttn_prefab, parent.transform);
                bttn.GetComponentInChildren<Text>().text = directories[2];
                //bttn.AddComponent(Button);
                Button btn = bttn.GetComponent<Button>();
                //btn.onClick.AddListener(() => Start_Process(file));
                //btn.onClick.AddListener(() => Test("aa"));

                var targetInfo = UnityEvent.GetValidMethodInfo(this, nameof(Test), new Type[0]);
                UnityAction methodDelegate = Delegate.CreateDelegate(typeof(UnityAction), this, targetInfo) as UnityAction;
                UnityEventTools.AddPersistentListener(BigExplosionEvent, methodDelegate);


                //UnityEngine.Debug.Log(btn);
                bttn.transform.position = new Vector3(0, posy, posx);
            }
            //string[] filePaths = Directory.GetFiles(Path.Combine(gamepath, folder));
            
        }

        



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UnityEngine.Events.UnityAction Start_Process(string path)
    {
        UnityEngine.Debug.Log("executing " + path);
        string command = "notepad";
        ProcessStartInfo proc_start_info = new ProcessStartInfo("cmd", "/c " + command);
        proc_start_info.CreateNoWindow = true;
        Process proc = new System.Diagnostics.Process();
        proc.StartInfo = proc_start_info;
        proc.Start();
        return null;
    }
    UnityAction Test(string a)
    {
        UnityEngine.Debug.Log("bttn pressed" + a);
        return null;
    }



}
