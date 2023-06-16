using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace S_Tools
{
    [Serializable]
    public class Component_S
    {
        public string Name;
        public Transform Obj;
    }


    public class ImportantReferences : MonoBehaviour
    {

        //private void Awake()
        //{
        //    Debug.Log("Test Awake");
        //}

        //private void Start()
        //{
        //    Debug.Log("Test Start");
        //}

        //private void OnValidate()
        //{
        //    Debug.Log("Test Onvalidate");
        //}

        //private void Update()
        //{
        //    Debug.Log("Test Update");
        //}

        //private void LateUpdate()
        //{
        //    Debug.Log("Test LateUpdate");
        //}

        //private void FixedUpdate()
        //{
        //    Debug.Log("Test FixedUpdate");
        //}

        public List<Component_S> components;
        
    }
}
