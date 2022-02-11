using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;

public class Import_games : MonoBehaviour
{

    public Canvas parent;

    public GameObject bttn_prefab;

    public string key;



    // Start is called before the first frame update
    void Start()
    {
        bttn_prefab.transform.SetParent(parent.transform);
        //string bttn_prefab.filepath = "test";


        string gamepath = Path.Combine(@"P:", "Python");
        string[] a_ext = { ".py", ".txt" };
        List<string> allowed_ext = new List<string>(a_ext);
        

        int posx = -100;
        int posy = 10;

        //UnityEngine.Debug.Log(gamepath);
        //UnityEngine.Debug.Log(Directory.GetDirectories(gamepath));
        //Button btn = bttn_prefab.GetComponent<Button>();
        //btn.onClick.AddListener(Test);

        foreach (string folder in Directory.GetDirectories(gamepath)){
            //UnityEngine.Debug.Log(Path.Combine(gamepath, folder));
            foreach(string file in Directory.GetFiles(Path.Combine(gamepath, folder)))
            {
                string ext = Path.GetExtension(file);
                if (!allowed_ext.Contains(ext))
                {
                    UnityEngine.Debug.Log("not valid ext: " + ext);
                    continue;
                }
                posy -= 6;
                if (posy < -100)
                {
                    UnityEngine.Debug.Log("new row");
                    posy = 10;
                    posx += 100;
                }
                //string path = Path.Combine(gamepath, folder, file);
                string[] directories = file.Split(Path.DirectorySeparatorChar);
                //var last = directories.Last();
                //UnityEngine.Debug.Log(directories[2]);
                //string filename = file[1];
                GameObject bttn = Instantiate(bttn_prefab, parent.transform);
                bttn.GetComponentInChildren<TextMeshProUGUI>().text = Path.GetFileNameWithoutExtension(directories[2]);
                //bttn.AddComponent(Button);
                Button btn = bttn.GetComponent<Button>();
                btn.onClick.AddListener(() => Start_Process(file));
                //btn.onClick.AddListener(Test);

                //var targetinfo = UnityEvent.GetValidMethodInfo(myScriptInstance, "OnButtonClick", new Type[] { typeof(GameObject) });
                //UnityAction methodDelegate = Delegate.CreateDelegate(typeof(UnityAction), this, targetInfo) as UnityAction;
                //UnityEditor.Events.UnityEventTools.AddPersistentListener(btn.onClick, methodDelegate);


                //UnityEngine.Debug.Log(btn);
                bttn.transform.position = new Vector3(0, posy, posx);
            }
            //string[] filePaths = Directory.GetFiles(Path.Combine(gamepath, folder));
            //UnityEngine.Debug.Log(posy);
            
        }

        



    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Debug.Log(EventSystem.current);
        //if (Input.GetKeyDown(key))
        //{
        //    EventSystem.current.SetSelectedGameObject(this.gameObject);
        //}
    }

    public UnityEngine.Events.UnityAction Start_Process(string path)
    {
        UnityEngine.Debug.Log("executing " + path);
        //var ext = Path.GetExtension(path);
        //string command = "";
        //switch (ext)
        //{
        //    case ".txt":
        //        command = "notepad";
        //        break;


        //}

        //ProcessStartInfo proc_start_info = new ProcessStartInfo("cmd", "/c " + command);
        //proc_start_info.CreateNoWindow = true;
        //Process proc = new Process();
        //proc.StartInfo = proc_start_info;
        Process.Start(@path);
        return null;
    }
    void Test()
    {
        UnityEngine.Debug.Log("bttn pressed");
        //return null;
    }



}
