using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentAttributes : MonoBehaviour {

    public static string nameOfComponent;

    public string Name;

    public int nambersOfComponent;

    /// <summary>
    /// 小部件在组装的时候是有顺序的。这个变量是总共多少步。
    /// </summary>
    public int mergeMaxIndex;

    /// <summary>
    /// 小部件合并动画，当前应该合并的部分索引。
    /// </summary>
    private int curCheckIndex = 0;

    private LittleComponentAttributes[] componentsList;// = new List<GameObject>();

    private void Awake()
    {
        nameOfComponent = Name;
    }

    // Use this for initialization
    void Start ()
    {
        ReadyForStart();
        //CheckLevel();
    }

    public void ReadyForStart()
    {
        componentsList = transform.GetComponentsInChildren<LittleComponentAttributes>();
        foreach (var item in componentsList)
        {
            //设置合并顺序为0的部分做好能播放动画的准备，并且鼠标放上去显示黄色。
            if (item.index == 0)
                item.GetComponent<MouseClickAnim>().isReady = true;
            else
                item.GetComponent<MouseClickAnim>().isReady = false;
        }

        nambersOfComponent = componentsList.Length;
    }


    public void UpdateList()
    {

        curCheckIndex++;
        foreach (var item in componentsList)
        {
            if (item.index == curCheckIndex)
                item.GetComponent<MouseClickAnim>().isReady = true;
        }
        if (curCheckIndex >= mergeMaxIndex)
            curCheckIndex = 0;

        //CheckLevel();
    }
}
