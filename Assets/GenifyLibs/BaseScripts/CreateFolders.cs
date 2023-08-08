using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GenifyLibs.BaseScripts
{
#if UNITY_EDITOR
    public class CreateFolders : EditorWindow
    {
        private static string projectName = "PROJECT_NAME";

        [MenuItem("Assets/Create Default Folders")]
        private static void SetUpFolders()
        {
            var window = CreateInstance<CreateFolders>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 150);
            window.ShowPopup();
        }

        private static void CreateAllFolders()
        {
            var folders = new List<string>
            {
                "3rdParty",
                "GenifyLibs",
                "Plugins",
                "Prefabs",
                "Scripts",
                "Scenes",
                "Anim",
                "Art",
                "UI"
            };
            foreach (string folder in folders)
            {
                if (!Directory.Exists("Assets/" + folder))
                {
                    Directory.CreateDirectory("Assets/" + folder);
                    // Directory.CreateDirectory("Assets/" + projectName + "/" + folder);
                }
            }

            var sceneSub = new List<string>
            {
                "SceneLoader",
                "Menu",
                "GamePlay",
                "GameOver"
            };
            foreach (string subfolder in sceneSub)
            {
                if (!Directory.Exists("Assets/" + "/Scenes/" + subfolder))
                {
                    Directory.CreateDirectory("Assets/" + "/Scenes/" + subfolder);
                }
            }
            
            var animSub = new List<string>
            {
                "Animations",
                "Animator"
            };
            
            foreach (string subfolder in animSub)
            {
                if (!Directory.Exists("Assets/" + "/Anim/" + subfolder))
                {
                    Directory.CreateDirectory("Assets/" + "/Anim/" + subfolder);
                }
            }
            
            var artSub = new List<string>
            {
                "Textures",
                "Shaders",
                "Materials",
                "Models",
                "Audio"
            };
            
            foreach (string subfolder in artSub)
            {
                if (!Directory.Exists("Assets/" + "/Art/" + subfolder))
                {
                    Directory.CreateDirectory("Assets/" + "/Art/" + subfolder);
                }
            }
            
            var uiSub = new List<string>
            {
                "Common",
                "Screen1",
                "Screen2",
                "SpriteAtlas"
            };
            
            foreach (string subfolder in artSub)
            {
                if (!Directory.Exists("Assets/" + "/UI/" + subfolder))
                {
                    Directory.CreateDirectory("Assets/" + "/UI/" + subfolder);
                }
            }

            AssetDatabase.Refresh();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Insert the Project name used as the root folder");
            projectName = EditorGUILayout.TextField("Project Name: ", projectName);
            this.Repaint();
            GUILayout.Space(70);
            if (GUILayout.Button("Generate!"))
            {
                CreateAllFolders();
                this.Close();
            }
        }
    }
#endif
}