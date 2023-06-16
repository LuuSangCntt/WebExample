using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace S_Tools
{
    public class S_Tool_Window : EditorWindow
    {
        public static S_Tool_Window window;
        private List<GameObject> DeactiveGameObjects;
        private GameObject targetObject;
        private bool isIsolated = false;

        [MenuItem("UniTech/S_Tool_Window")]
        public static void Init()
        {
            OpenWindow();
        }

        private static void OpenWindow()
        {
            window = EditorWindow.GetWindow(typeof(S_Tool_Window), false, "S Tools") as S_Tool_Window;
            window.minSize = new Vector2(10, 20);
            window.Show();
        }

        void OnGUI()
        {
            #region //dòng 1
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Activate"))
            {
                ActivateSelectedObject();
            }
            if (GUILayout.Button("DeActivate"))
            {
                DeActivateSelectedObject();
            }
            if (GUILayout.Button("Unpack Prefab"))
            {
                UnpackPrefabCompletely();
            }
            GUILayout.EndHorizontal();
            #endregion

            #region dòng 2
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Isolate"))
            {
                IsolateObject();
            }
            //GUILayout.Space(3);
            if (GUILayout.Button("  Back  "))
            {
                DeIsolateObject();
            }
            if (GUILayout.Button("MemoryFree"))
            {
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
                Debug.Log("Resources.UnloadUnusedAssets");
            }
            GUILayout.EndHorizontal();
            #endregion
        }

        private void UnpackPrefabCompletely()
        {
            GameObject prefab = Selection.gameObjects[0];
            PrefabUtility.UnpackPrefabInstance(prefab, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
        }

        void ActivateSelectedObject()
        {
            if (Selection.gameObjects.Length > 0)
            {
                for (int i = 0; i < Selection.gameObjects.Length; i++)
                {
                    Selection.gameObjects[i].SetActive(true);
                }
            }
            else
            {
                Debug.Log("No object selected");
            }
        }
        void DeActivateSelectedObject()
        {
            if (Selection.gameObjects.Length > 0)
            {
                for (int i = 0; i < Selection.gameObjects.Length; i++)
                {
                    Selection.gameObjects[i].SetActive(false);
                }
            }
            else
            {
                Debug.Log("No object selected");
            }
        }
        private void IsolateObject()
        {
            if (isIsolated == true) return;

            if (Selection.gameObjects.Length > 0)
            {
                DeactiveGameObjects = new List<GameObject>();
                targetObject = Selection.gameObjects[0];
                DeactiveOtherChildren(targetObject);
                isIsolated = true;
            }
            else
            {
                Debug.Log("No object selected");
            }
        }

        private void DeactiveOtherChildren(GameObject targetObject)
        {
            var parent = targetObject.transform.parent;
            if (parent != null)
            {
                for (int i = 0; i < parent.childCount; i++)
                {
                    var obj = parent.GetChild(i).gameObject;
                    if (obj != targetObject)
                    {
                        if (obj.activeSelf == true)
                        {
                            Deactive(obj);
                        }
                        else
                        {
                            //Debug.Log(obj.name);
                        }
                    }
                }
                DeactiveOtherChildren(parent.gameObject);
            }
            else
            {
                var objectList = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects().ToList();
                for (int i = 3; i < objectList.Count; i++)
                {
                    GameObject obj = objectList[i];
                    if (obj != targetObject)
                    {
                        if (obj.activeSelf == true)
                        {
                            Deactive(obj);
                        }
                        else
                        {
                            //Debug.Log(obj.name);
                        }
                    }
                }
            }
        }
        private void Deactive(GameObject obj)
        {
            DeactiveGameObjects.Add(obj);
            obj.SetActive(false);
        }

        private void DeIsolateObject()
        {
            if (isIsolated == false) return;

            foreach (GameObject obj in DeactiveGameObjects)
            {
                Active(obj);
            }
            DeactiveGameObjects = null;
            isIsolated = false;
        }
        private void Active(GameObject obj)
        {
            obj.SetActive(true);
            //DeactiveGameObjects.Remove(obj);
        }

    }
}
