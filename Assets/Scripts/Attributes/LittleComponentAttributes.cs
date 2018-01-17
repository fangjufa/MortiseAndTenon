using UnityEngine;

/// <summary>
/// 小部件的属性，比如桌面上的榫卯、桌腿的榫卯等。
/// </summary>
public class LittleComponentAttributes : MonoBehaviour {

    //动画播放的次序。0是最先播放的。
    public int index;

    private Material[] mats;

    // Use this for initialization
    void Start () {
        mats = GetComponent<MeshRenderer>().materials;
    }

    private void OnMouseEnter()
    {
        if (mats != null)
        {
            foreach (Material item in mats)
            {
                if (this.GetComponent<MouseClickAnim>().isReady)
                {
                    //可以播放合并动画时，黄色显示
                    item.color = Color.yellow;
                }
                else
                {
                    //否则红色显示
                    item.color = Color.red;
                }
            }
        }
    }

    private void OnMouseExit()
    {
        HighlightOff();
    }

    private void OnMouseDown()
    {
        if (this.GetComponent<MouseClickAnim>().isReady)
            HighlightOff();
    }

    /// <summary>
    /// 取消高光
    /// </summary>
    private void HighlightOff()
    {
        if (mats != null)
        {
            foreach (Material item in mats)
            {
                item.color = Color.gray;
            }
        }
    }
}
