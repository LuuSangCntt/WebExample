using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace S_Tools
{
    public class CreateTarballFromPackage : MonoBehaviour
    {

        public string packageFolder;
        public string saveFolder;


        void Start()
        {
            CreateTarballFile();
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                CreateTarballFile();
            }
        }
        void CreateTarballFile()
        {
            Client.Pack(packageFolder, saveFolder);
            Debug.Log("Save Folder: " + saveFolder);
        }
    }
}