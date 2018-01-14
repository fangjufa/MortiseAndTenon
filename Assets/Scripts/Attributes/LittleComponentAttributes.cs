using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;

public class LittleComponentAttributes : MonoBehaviour {

    //public enum Level
    //{
    //    I,
    //    II,
    //    III,
    //    IV,
    //    V,
    //    VI,
    //    ZERO
    //}

    //public Level level;

    public int index;

    private Highlighter highlighter;

    // Use this for initialization
    void Start () {
        highlighter = this.gameObject.AddComponent<Highlighter>();
    }

    private void OnMouseEnter()
    {
        if (this.GetComponent<MouseClickAnim>().isReady) highlighter.ConstantOn(Color.yellow);
        else highlighter.ConstantOn(Color.red);
    }

    private void OnMouseExit()
    {
        highlighter.ConstantOff();
    }

    private void OnMouseDown()
    {
        if (this.GetComponent<MouseClickAnim>().isReady)
            highlighter.ConstantOff();
    }
}
