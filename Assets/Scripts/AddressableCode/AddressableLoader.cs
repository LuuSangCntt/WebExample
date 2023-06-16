using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableLoader : MonoBehaviour
{
    public List<AssetReferenceGameObject> ListObject;

    public List<GameObject> spawnObjects;
    public List<AsyncOperationHandle<GameObject>> asyncOperations = new List<AsyncOperationHandle<GameObject>>();
    // Start is called before the first frame update
    void Start()
    {
        spawnObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Start " + Time.realtimeSinceStartup);
            LoadAllObjects();

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Release object");
            ReleaseMemory();


        }
    }

    private void ReleaseMemory()
    {
        //foreach (Transform t in transform)
        //{
        //    Destroy(t.gameObject);
        //}
        //if (spawnObjects != null)
        //{
        //    foreach (GameObject go in spawnObjects)
        //    {
        //        Addressables.ReleaseInstance(go);
        //    }
        //}
        UnloadAll();
        spawnObjects.Clear();
        Resources.UnloadUnusedAssets();
    }

    private void LoadAllObjects()
    {
        if (ListObject != null)
        {
            foreach (var obj in ListObject)
            {
                //obj.LoadAssetAsync().Completed += AddressableLoader_Completed;
                StartCoroutine(LoadObject(obj));
            }
        }
    }

    void UnloadAll()
    {
        foreach(var handle in asyncOperations)
        {
            Addressables.Release(handle);
        }
        asyncOperations.Clear();
    }

    private IEnumerator LoadObject(AssetReference asset)
    {
        AsyncOperationHandle<GameObject> handle = asset.InstantiateAsync();
        yield return handle;
        yield return new WaitForEndOfFrame();

        asyncOperations.Add(handle);
        spawnObjects.Add(handle.Result);
    }

    private void AddressableLoader_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        Instantiate(obj.Result, transform);
        spawnObjects.Add(obj.Result);
        Debug.Log("Finish loading " + obj.Result.name + ": " + Time.realtimeSinceStartup);
    }
}
