using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameMenuMono.Menu
{
    class BuildMenu
    {
        public static Texture2D img;

        public static List<Button> Build(Texture2D _img, Rectangle window_rect)
        {
            
            // vars
            string[] a_ext = { ".py", ".txt" };
            string bttnText;
            List<string> allowed_ext = new List<string>(a_ext);
            img = _img;

            List<string> filepaths = Get_files_to_exec(allowed_ext);

            List<Button>  buttons = Build_menu(filepaths, window_rect);

            //RectTransform rt_bttn = bttn_prefab.GetComponent<RectTransform>();

            //Build_menu(filepaths, button_height: rt_bttn.rect.height / 10f);

            //UnityEngine.Debug.Log(Path.GetFullPath("..\\games"));



            return buttons;
        }




        private static List<string> Get_files_to_exec(List<string> allowed_ext)
        {
            //string gamepath = Path.Combine(@"..\\games");
            //string gamepath = Path.Combine(Directory.GetCurrentDirectory(), "games");
            string gamepath = "P:\\Python";
            List<string> filepaths = new List<string>();


            foreach (string folder in Directory.GetDirectories(gamepath))
            {
                //UnityEngine.Debug.Log(Path.Combine(gamepath, folder));
                string[] files = Directory.GetFiles(folder);
                foreach (string file in Directory.GetFiles(folder))
                {
                    
                    string ext = Path.GetExtension(file);
                    if (allowed_ext.Contains(ext))
                    {
                        filepaths.Add(file);
                        

                    }
                    //if (filepaths.Count > 15)
                    //{
                    //    break;
                    //}
                }
            }
            return filepaths;
        }

        public void Start_Process(string path)
        {
            //UnityEngine.Debug.Log("executing " + path);
            Process.Start(@path);
            //return null;
        }

        static List<Button> Build_menu(List<string> filepaths, Rectangle window_rect, int max_rows = 10, int max_columns = 5, float button_height = 12f)
        {
            List<Button> buttons = new List<Button>();
            
            //int worldspace_to_pixel_ration = 10;
            //var window = GraphicsDevice.DisplayMode.Width;
            //RectTransform rt_canv = parent.GetComponent<RectTransform>();
            //float camera_height = 2f * camera.orthographicSize;

            float useable_height = window_rect.Height;
            float camera_width = window_rect.Width;
            int len = filepaths.Count;
            int used_colums = (len / max_rows) + 1;
            float x_intervall = camera_width / (used_colums + 1);
            //UnityEngine.Debug.Log(Directory.GetCurrentDirectory());
            float y_intervall = useable_height / (max_rows + 1);
            float posx, posy, cur_col, cur_row;
            //Vector3 pos;
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
                cur_row = i % max_rows + 1;
                // make sure its Ordered top to bottom left to right
                posx = (x_intervall * cur_col) - camera_width;
                posy = -(cur_row * y_intervall);

                //string[] directories = file.Split(Path.DirectorySeparatorChar);
                string bttnName = Path.GetFileName(file);
                //delegate Action<file> ();

                //bttn.GetComponentInChildren<TextMeshProUGUI>().text = Path.GetFileNameWithoutExtension(file);
                //Button btn = bttn.GetComponent<Button>();
                //btn.onClick.AddListener(() => Start_Process(file));
                Action showMethod = () => Process.Start(@file);
                var bttn = new Button { bttnName = bttnName, gamePath = file, img = img, position = new Vector2(posx, posy), onclick = showMethod };
                buttons.Add(bttn);
                //if (i == 0)
                //{
                //    EventSystem.current.SetSelectedGameObject(bttn);
                //}

                i++;
            }
            return buttons;

        }

    }
}
