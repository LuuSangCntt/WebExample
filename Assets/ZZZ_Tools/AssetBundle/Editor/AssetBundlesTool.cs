using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace S_Tools
{
    public class AssetBundlesTool
    {
        [MenuItem("UniTech/AssetBundles/Create")]
        [MenuItem("Assets/Create AssetBundles")]
        public static void CreateAssetsBundles()
        {
            List<GameObject> objectList = Selection.gameObjects.ToList(); //Lấy mảng tên của tất cả các đối tượng đang được chọn
            if (objectList.Count == 0)
            {
                Debug.LogWarning("No Gameobject is selected");
                return;
            }
            objectList = objectList.OrderBy(obj => obj.name).ToList();
            ClearAllAssetBundleNames();
            foreach (GameObject target in objectList)
            {
                string assetBundleName = target.name;
                string assetPath = AssetDatabase.GetAssetPath(target);
                AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(assetBundleName, "");
            }
            BuildAllAssetBundles();
        }

        [MenuItem("UniTech/AssetBundles/Build All")]
        static void BuildAllAssetBundles()
        {
            string assetBundleDirectory = "Assets/StreamingAssets"; //Chỉ định thư mục để lưu assetbundle
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }
            //BuildPipeline.BuildAssetBundles(assetBundleDirectory,
            //                                BuildAssetBundleOptions.UncompressedAssetBundle & BuildAssetBundleOptions.AssetBundleStripUnityVersion,
            //                                EditorUserBuildSettings.activeBuildTarget);

            //Tạo assetbundle cho tất cả các bundle names
            BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                           BuildAssetBundleOptions.UncompressedAssetBundle,
                                           EditorUserBuildSettings.activeBuildTarget);
            //Trong BuildAssetBundleOption có nhiều lựa chọn, ví dụ cho phép nén hay ko nén dữ liệu

            Debug.Log("Asset created. Buildtarget= " + EditorUserBuildSettings.activeBuildTarget);
        }

        [MenuItem("UniTech/AssetBundles/Rebuild All")]
        static void ReBuildAllAssetBundles()
        {
            string assetBundleDirectory = "Assets/StreamingAssets";
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }
            //Tạo assetbundle cho tất cả các bundle names, tùy chọn ForceRebuild
            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.ForceRebuildAssetBundle, EditorUserBuildSettings.activeBuildTarget);
        }


        static void CreateAssetBundle(UnityEngine.Object target)
        {
            ClearAllAssetBundleNames();
            // var target = Selection.activeObject;
            string assetBundleName = target.name;
            string assetPath = AssetDatabase.GetAssetPath(target);
            AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(assetBundleName, ""); //Chỉ định assetbundle cho một đối tượng
            BuildAllAssetBundles();
            //Debug.Log("AssetBundle of " + assetBundleName + " is created");
        }



        [MenuItem("UniTech/AssetBundles/Clear all AssetBundle Names")]
        static void ClearAllAssetBundleNames()
        {
            var names = AssetDatabase.GetAllAssetBundleNames(); //lấy mảng các tên của assetbundle
            foreach (var name in names)
            {
                AssetDatabase.RemoveAssetBundleName(name, true); //Xóa tên của assetbundle trong danh sách
            }
            Debug.Log("All AssetBundle Names are deleted");
        }
    }
}