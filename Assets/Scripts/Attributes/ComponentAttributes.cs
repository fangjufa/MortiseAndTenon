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
    /// 在合并动画时，动画播放的顺序
    /// </summary>
    //private enum CheckedLevel
    //{
    //    I,
    //    II,
    //    III,
    //    IV,
    //    V,
    //    VI
    //}
    //private CheckedLevel checkLevel;

    /// <summary>
    /// 小部件合并动画，当前应该合并的部分索引。
    /// </summary>
    private int curCheckIndex = 0;

    //private LittleComponentAttributes.Level level;

    //private List<GameObject> level6 = new List<GameObject>();
    //private List<GameObject> level5 = new List<GameObject>();
    //private List<GameObject> level4 = new List<GameObject>();
    //private List<GameObject> level3 = new List<GameObject>();
    //private List<GameObject> level2 = new List<GameObject>();
    //private List<GameObject> level1 = new List<GameObject>();
    private LittleComponentAttributes[] componentsList;// = new List<GameObject>();

    /// <summary>
    /// 小部件集合，小部件在合并的时候是有顺序的，该集合就是以它的顺序为键值的。
    /// </summary>
    //private Dictionary<CheckedLevel, List<LittleComponentAttributes>> componentsList = new Dictionary<CheckedLevel, List<LittleComponentAttributes>>();


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
            if (item.index == 0)//.level == LittleComponentAttributes.Level.I)
                item.GetComponent<MouseClickAnim>().isReady = true;
            else
                item.GetComponent<MouseClickAnim>().isReady = false;
        }
        //foreach (Transform child in this.transform)
        //{
        //    if (child.GetComponent<LittleComponentAttributes>())
        //    {
        //        componentsList.Add(child.gameObject);
        //    }
        //    ForeachCheck(child);
        //}

        nambersOfComponent = componentsList.Length;
    }


    public void UpdateList()
    {
        //level1.Clear();
        //level2.Clear();
        //level3.Clear();
        //level4.Clear();
        //level5.Clear();
        //level6.Clear();

        //foreach (Transform child in this.transform)
        //{
        //    ForeachCheck(child);
        //}

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

    public void CheckLevel()
    {
        //if (level1.Count == 0)
        //{
        //    checkLevel = CheckedLevel.II;
        //    if (level2.Count == 0)
        //    {
        //        checkLevel = CheckedLevel.III;
        //        if (level3.Count == 0)
        //        {
        //            checkLevel = CheckedLevel.IV;
        //            if (level4.Count == 0)
        //            {
        //                checkLevel = CheckedLevel.V;
        //                if (level5.Count == 0)
        //                {
        //                    checkLevel = CheckedLevel.VI;
        //                }
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    checkLevel = CheckedLevel.I;
        //}

        //switch (checkLevel)
        //{
        //    case CheckedLevel.I:
        //        break;
        //    case CheckedLevel.II:
        //        foreach (GameObject g in level2)
        //        {
        //            g.GetComponent<MouseClickAnim>().isReady = true;
        //        }
        //        break;
        //    case CheckedLevel.III:
        //        foreach (GameObject g in level3)
        //        {
        //            g.GetComponent<MouseClickAnim>().isReady = true;
        //        }
        //        break;
        //    case CheckedLevel.IV:
        //        foreach (GameObject g in level4)
        //        {
        //            g.GetComponent<MouseClickAnim>().isReady = true;
        //        }
        //        break;
        //    case CheckedLevel.V:
        //        foreach (GameObject g in level5)
        //        {
        //            g.GetComponent<MouseClickAnim>().isReady = true;
        //        }
        //        break;
        //    case CheckedLevel.VI:
        //        foreach (GameObject g in level6)
        //        {
        //            g.GetComponent<MouseClickAnim>().isReady = true;
        //        }
        //        break;
        //    default:
        //        break;
        //}
    }


    /// <summary>
    /// 初始化CheckLeve list数组，并将第一个Level设为true。
    /// </summary>
    /// <param name="child"></param>
    //private void ForeachCheck(Transform child)
    //{
    //    if (child)
    //    {
    //        if (child.GetComponent<LittleComponentAttributes>())
    //        {
    //            level = child.GetComponent<LittleComponentAttributes>().level;
    //            switch (level)
    //            {
    //                case LittleComponentAttributes.Level.I:
    //                    level1.Add(child.gameObject);
    //                    if(child.GetComponent<MouseClickAnim>())
    //                         child.GetComponent<MouseClickAnim>().isReady = true;
    //                    break;
    //                case LittleComponentAttributes.Level.II:
    //                    level2.Add(child.gameObject);
    //                    if (child.GetComponent<MouseClickAnim>())
    //                        child.GetComponent<MouseClickAnim>().isReady = false;
    //                    break;
    //                case LittleComponentAttributes.Level.III:
    //                    level3.Add(child.gameObject);
    //                    if (child.GetComponent<MouseClickAnim>())
    //                        child.GetComponent<MouseClickAnim>().isReady = false;
    //                    break;
    //                case LittleComponentAttributes.Level.IV:
    //                    level4.Add(child.gameObject);
    //                    if (child.GetComponent<MouseClickAnim>())
    //                        child.GetComponent<MouseClickAnim>().isReady = false;
    //                    break;
    //                case LittleComponentAttributes.Level.V:
    //                    level5.Add(child.gameObject);
    //                    if (child.GetComponent<MouseClickAnim>())
    //                        child.GetComponent<MouseClickAnim>().isReady = false;
    //                    break;
    //                case LittleComponentAttributes.Level.VI:
    //                    level6.Add(child.gameObject);
    //                    if (child.GetComponent<MouseClickAnim>())
    //                        child.GetComponent<MouseClickAnim>().isReady = false;
    //                    break;
    //                default:
    //                    break;
    //            }
    //        }
    //    }
    //}
}
