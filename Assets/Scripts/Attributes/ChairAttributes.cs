using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 椅子的属性变量，挂载到椅子的整体部分物体上。
/// </summary>
public class ChairAttributes : MonoBehaviour {

    public string nameOfFurniture;
    public int numbersOfComponent;

    public Sprite deskSprite;
    public Sprite legSprite;
    public Sprite backrestSprite; //若无第三个按钮留空

    public GameObject desk;
    public GameObject leg;
    public GameObject backrest; //若无第三个部件留空
}
