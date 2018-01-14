using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecomController : MonoBehaviour {

    public static int ready;

    //public static Dictionary<string, Vector3> componentStartPosition = new Dictionary<string, Vector3>();

    // Use this for initialization
    void Start () {
        //changePosition = new Vector3(0, 10, 0);
    }
	
	// Update is called once per frame
	void Update () {
        //print(ready);
    }

    private void LateUpdate()
    {
        
    }

    public static void SearchComponent()
    {
        if (BGImg.currenObject != null)
        {
            BGImg.currenObject.AddComponent<DecomAnimatorController>();
            foreach (Transform child in BGImg.currenObject.transform)
            {
                if (child.GetComponent<LittleComponentAttributes>())
                {
                    child.gameObject.AddComponent<MouseClickAnim>();
                }               
            }
        } 
    }

    public static void CleanDictionary()
    {
        //componentStartPosition.Clear();
        Destroy(BGImg.currenObject.GetComponent<DecomAnimatorController>());
        foreach (Transform child in BGImg.currenObject.transform)
        {
           Destroy(child.gameObject.GetComponent<MouseClickAnim>());
        }
    }
    
}
