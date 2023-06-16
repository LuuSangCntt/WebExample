using UnityEngine;
using System.Collections;
using TMPro;
using System.IO;
using Debug = UnityEngine.Debug;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Collections.Generic;

public class LoadImage : MonoBehaviour
{

    public TMP_InputField txtPath;
    //[DllImport("__Internal")]
    //private static extern void ImageUploaderCaptureClick();
    private Texture2D displayedTexture;
    public Renderer render;
    public List<string> ListAnhPath;
    public int index = 0;

    void Start()
    {
        displayedTexture = new Texture2D(1, 1);
        render.material.mainTexture = displayedTexture;
        render.enabled = true;

    }

    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            //StartCoroutine(LoadTexture(txtPath.text));
            Debug.Log("Load");
            index = index<(ListAnhPath.Count-1)? index+1 : 0;
            StartCoroutine(LoadTexture(ListAnhPath[index]));
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            Addressables.LoadAssetAsync<GameObject>(ListAnhPath[index]).Completed += OnLoadDone;
        }
        InitListAnhPath();
    }

    private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        Debug.Log("Load finish " + obj.DebugName);
        //Texture2D texture2D = obj.Result as Texture2D;
    }
    private void InitListAnhPath()
    {
        ListAnhPath = new List<string>();
        ListAnhPath.Add("http://192.168.31.30:12345/Models/915610.jpg");
        ListAnhPath.Add("http://192.168.31.30:12345/Models/pexels-james-wheeler-417074.jpg");
        ListAnhPath.Add("http://192.168.31.30:12345/Models/pexels-pixabay-50594.jpg");
        ListAnhPath.Add("http://192.168.31.30:12345/Models/Terrain 0x0r4096x4096.png");
        ListAnhPath.Add("http://192.168.31.30:12345/Models/Terrain 1x0r4096x4096.png");
        ListAnhPath.Add("http://192.168.31.30:12345/Models/DSC01614.jpg");
        ListAnhPath.Add("http://192.168.31.30:12345/Models/DSC01618.jpg");
        ListAnhPath.Add("http://192.168.31.30:12345/Models/DSC01621.jpg");
        ListAnhPath.Add("http://192.168.31.30:12345/Models/Terrain 1x1r4096x4096.png");
        ListAnhPath.Add("http://192.168.31.30:12345/Models/Terrain 0x1r4096x4096.png");
    }
    IEnumerator LoadTexture(string url)
    {
        WWW image = new WWW(url);
        yield return image;
        image.LoadImageIntoTexture(displayedTexture);
    }

//    void FileSelected(string url)
//    {
//        StartCoroutine(LoadTexture(url));
//    }

//    public void OnButtonPointerDown()
//    {
//#if UNITY_EDITOR
//        string path = UnityEditor.EditorUtility.OpenFilePanel("Open image", "", "jpg,png,bmp");
//        if (!System.String.IsNullOrEmpty(path))
//            FileSelected("file:///" + path);
//#else
//        ImageUploaderCaptureClick ();
//#endif
    //}

}
