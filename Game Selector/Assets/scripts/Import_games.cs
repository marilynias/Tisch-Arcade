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

    public Camera camera;



    // Start is called before the first frame update
    void Start()
    {
        // vars
        string[] a_ext = { ".py", ".txt" };
        List<string> allowed_ext = new List<string>(a_ext);    
        //int posx = -100;
        //int posy = 10;


        // Main
        bttn_prefab.transform.SetParent(parent.transform);
        // get a List of all paths to Files that can be executed
        List<string> filepaths = Get_files_to_exec(allowed_ext);

        RectTransform rt_bttn = bttn_prefab.GetComponent<RectTransform>();
        
        Build_menu(filepaths, button_height: rt_bttn.rect.height/10f);





    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Start_Process(string path)
    {
        UnityEngine.Debug.Log("executing " + path);
        Process.Start(@path);
        //return null;
    }
    //void Test()
    //{
    //    UnityEngine.Debug.Log("bttn pressed");
    //}


    static List<string> Get_files_to_exec(List<string> allowed_ext)
    {
        string gamepath = Path.Combine(@"P:", "Python");
        List<string> filepaths = new List<string>();


        foreach (string folder in Directory.GetDirectories(gamepath))
        {
            //UnityEngine.Debug.Log(Path.Combine(gamepath, folder));
            foreach (string file in Directory.GetFiles(Path.Combine(gamepath, folder)))
            {
                string ext = Path.GetExtension(file);
                if (allowed_ext.Contains(ext))
                {
                    filepaths.Add(file);
                    
                }
                //else
                //{
                //    UnityEngine.Debug.Log("not valid ext: " + ext);
                //}
            }
        }
        return filepaths;
    }

    void Build_menu(List<string> filepaths, int max_rows = 10, int max_columns = 5, float button_height = 2f)
    {
        //int worldspace_to_pixel_ration = 10;
        RectTransform rt_canv = camera.GetComponent<RectTransform>();
        float camera_height = 2f * camera.orthographicSize;
        float useable_height = camera_height - camera_height / 10;
        float camera_width = camera_height * camera.aspect;
        int len = filepaths.Count;
        int used_colums = (len / max_rows)+1;
        float x_intervall = camera_width / (used_colums+1);
        UnityEngine.Debug.Log(camera_width);
        float y_intervall = useable_height / (max_rows+1);
        float posx, posy, cur_col, cur_row;
        Vector3 pos;
        //UnityEngine.Debug.Log("len: "+len+ " , float: "+(len / max_rows)+ " col: "+used_colums);
        if (used_colums > max_columns)
        {
            used_colums = max_columns;
        }
        
        
        int i = 0;
        //int curr_row = 1;

        foreach (string file in filepaths)
        {
            
            cur_col = (i / max_rows + 1);
            posx = x_intervall * cur_col - camera_width / 2;
            cur_row = i % max_rows + 1;
            posy = ((cur_row * y_intervall) - (camera_height));
            //UnityEngine.Debug.Log(posx);

            //posy -= 6;
            //if (posy < -100)
            //{
            //    UnityEngine.Debug.Log("new row");
            //    posy = 10;
            //    posx += 100;
            //}
            string[] directories = file.Split(Path.DirectorySeparatorChar);
            GameObject bttn = Instantiate(bttn_prefab, parent.transform);
            bttn.GetComponentInChildren<TextMeshProUGUI>().text = Path.GetFileNameWithoutExtension(directories[2]);
            Button btn = bttn.GetComponent<Button>();
            btn.onClick.AddListener(() => Start_Process(file));
            //pos = camera.ScreenToWorldPoint(new Vector3(posx, posy, 0));
            bttn.transform.position = new Vector3(posx, posy, -1);

            i++;
        }
    }

}
