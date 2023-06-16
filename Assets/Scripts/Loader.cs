using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Loader : MonoBehaviour
{

    public TMP_InputField txtPath;
    //public string path = "http://localhost/Maps/terrain%2044x43";
    public string path = "http://192.168.31.30:12345/Models/bom_buom_m83";
    public Transform Parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadAssetBundle(txtPath.text));
        }
        if(Input.GetKeyUp(KeyCode.R))
        {
            foreach (Transform t in Parent.transform)
            {
                Destroy(t.gameObject);
            }
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
            Debug.Log("Resources.UnloadUnusedAssets");
        }    
    }

   
    private IEnumerator LoadAssetBundle(string assetBundleUrl)
    {
        using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(assetBundleUrl))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading AssetBundle: " + request.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
                GameObject loadedAsset = bundle.LoadAsset<GameObject>(name: bundle.GetAllAssetNames()[0]);
                Instantiate(loadedAsset, Vector3.zero, Quaternion.identity, Parent);
                bundle.Unload(false);
            }
        }
    }
}
