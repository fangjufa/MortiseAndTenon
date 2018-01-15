using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 选择家具的UI界面。
/// </summary>
public class ChooseFurnitureUI : MonoBehaviour
{
    //带圆圈的透视UI Image。
    public Image BG;
    //该按钮是覆盖圆圈的，但是是全透明的。
    public Button ChairBtn;
    //在右下显示椅子名字的UI
    //public Text ChairName;
    public Text nameOfFurniture;

    public Image logoInBG;

    public BGImg bgImg;

    // Use this for initialization
    void Start ()
    {
        ChairBtn.onClick.AddListener(OnChairBtnClick);
    }

    private void OnChairBtnClick()
    {
        
        //ChairBtn.enabled = false;

        //HideGrphic(noteInFurniture.gameObject, true);
        HideGrphic(logoInBG, true);

        HideGrphic(ChairBtn.GetComponent<Image>(), true);
        HideGrphic(nameOfFurniture, true);

        

        BG.DOFade(0, 1).OnComplete(() =>
        {
            gameObject.SetActive(false);
            bgImg.roundDeskFurniture.GetComponent<BoxCollider>().enabled = true;
        });

        bgImg.ToFurnitureScene();

    }

    private void HideGrphic(Graphic graphic, bool isHide)
    {
        int time = 1;
        int alpha = isHide ? 0 : 1;
        graphic.DOFade(alpha, time);
    }
}
