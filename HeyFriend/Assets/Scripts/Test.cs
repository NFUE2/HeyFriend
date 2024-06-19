using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {

    }
    GameObject obj;
    void Start()
    {
        obj = transform.GetChild(0).gameObject;
        Debug.Log(obj);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
