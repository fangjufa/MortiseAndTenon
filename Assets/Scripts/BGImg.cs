using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BGImg : MonoBehaviour {
    
    //当前被选中的部分，比如是桌腿还是桌面。
    private GameObject goComponent;

    private GameObject desk;
    private GameObject leg;
    private GameObject backrest;

    //隔一段时间如果没有操作的话，则返回到起初界面功能的变量
    //这个功能应该可以从这个脚本里抽出来。
    private int count;
    private float clockTimer;
    public float waitTime;
    /*------------------------------------------------*/

    public float audioVolume;
    //public float width = 2;
    //一个DOTween的缓动时间。
    public float time = 1;

    public GameObject boxCollider;


    /*----------------下面的UI在显示完整圈椅的界面显示的------------------*/

    public FurnitureUI furnitureUI;
    /*----------------------------------------------------------------------*/

    /*-----------------下面的UI是在有透明圆圈的界面才显示的-----------------------*/
    public ChooseFurnitureUI chooseFurnitureUI;
    public GameObject bgPanel;
    /*------------------------------------------------------------------------*/

    //椅子的每个小部件的父物体
    public Transform ChairFather;
    
    public Transform roundDeskFurniture;

    /*-------------在显示圈椅小部件时的界面出现的UI--------------------------*/
    public ComponentUI componentUI;
    /*-----------------------------------------------------------*/

    ///*--------------退出界面UI-------------------*/
    public GameObject escPanel;
    ///*---------------------------------------------*/


    public GameObject canvasOne;
    //public GameObject canvasTwo;

    public CameraController camController;

    public GameObject decomFather;
    public GameObject go;
    public GameObject startImg;
    

    public static GameObject currenObject;

    public static string transitName;

    public static bool isChange;
    public static bool isBackToBG;
    public static bool isInEsc;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    // Use this for initialization
    void Start () {
        isBackToBG = true;
        isChange = false;
        isInEsc = false;

        roundDeskFurniture.GetComponent<BoxCollider>().enabled = false;

        desk = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().desk;
        leg = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().leg;
        backrest = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().backrest;
    }

    // Update is called once per frame
    void Update()
    {
        //IdleWating();
        OnMoveComponent();
        
        PressEsc();
    }

    public void IntoComponentScene()
    {
        //进入家居小部件展示场景
        isChange = true;

        camController.GetComponent<CameraController>().IntoComponent();
        
        transitName = roundDeskFurniture.name;
        //Debug.Log(FurnitureList[0].name);
    }

    public void ShowComponentUI()
    {
        
        canvasOne.SetActive(false);

        componentUI.Show();
    }

    public void ShowBG()
    {
        ChairController.PlayHebing();
        //显示BG
        isBackToBG = true;
        chooseFurnitureUI.gameObject.SetActive(true);


        camController.GetComponent<CameraController>().BackToBG();

        Camera.main.GetComponent<AudioSource>().DOFade(1, 1);
        
    }

    public void OnTurnBack()
    {
        //返回整体家居界面
        //if (go) Destroy(go);
        camController.GetComponent<CameraController>().BackToOverall();
        //turnBackButton.enabled = false;
        
        isChange = false;

        canvasOne.SetActive(true);
        currenObject = null;
        transitName = null;
    }

    private void RefreshObject()
    {
        if (go) Destroy(go);

        go = Instantiate(goComponent, Vector3.zero, Quaternion.identity) as GameObject;
        go.transform.SetParent(decomFather.transform, false);
        go.name = "component";
        go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        currenObject = go;

        DecomController.CleanDictionary();
        DecomController.SearchComponent();
    }

    public void OnDeskButton()
    {
        goComponent = desk;
        transitName = roundDeskFurniture.name + "D_desk";
        //Debug.Log(transitName);

        RefreshObject();

    }

    public void OnBackrestButton()
    {
        goComponent = backrest;
        transitName = roundDeskFurniture.name + "D_backrest";
        //Debug.Log(transitName);

        RefreshObject();
    }

    public void OnLegButton()
    {
        goComponent = leg;
        transitName = roundDeskFurniture.name + "D_leg";
        //Debug.Log(transitName);

        RefreshObject();
    }

    private void OnReturnState()
    {
        DecomAnimatorController.Return();
    }

    /// <summary>
    /// 将原来的模块删掉，重新加载新的模块。
    /// </summary>
    public void Reload()
    {
        if (go)
        {
            Destroy(go);
            RefreshObject();
            currenObject.transform.localScale = Vector3.one;
        }
    }

    private void OnMoveComponent()
    {
        if (currenObject)
        {
            if (currenObject.transform.localScale.x < 0.99f) currenObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        }
    }

    /// <summary>
    /// 当用户没有任何输入超过一定时间之后，返回到最开始的界面。
    /// </summary>
    private void IdleWating()
    {
        if (Input.GetAxis("Mouse X") == 0 || Input.GetAxis("Mouse Y") == 0)
        {
            count++;
            //不移动鼠标时启动计时器  
            if (count == 1)
            {
                clockTimer = Time.time;
            }
            //当时间间隔满足要求时执行操作 
            if (Time.time - clockTimer >= waitTime)
            {
                boxCollider.SetActive(true);
                startImg.SetActive(true);
                startImg.GetComponent<Image>().DOFade(1, time);
            }
        }
        else
        {
            count = 0;
        }
    }

    /// <summary>
    /// 在键盘上按下了ESC键之后，显示出esc的界面。
    /// </summary>
    private void PressEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !escPanel.gameObject.activeSelf)
        {
            boxCollider.SetActive(true);
            escPanel.SetActive(true);
        }
        if (escPanel.gameObject.activeSelf)
        {
            isInEsc = false;
        }
        else
        {
            isInEsc = true;
            
            IdleWating();
        }

    }

    /// <summary>
    /// 点击了圆圈的椅子之后，进入到椅子的整体界面。
    /// </summary>
    public void ToFurnitureScene()
    {
        //进入整体家居展示界面
        isBackToBG = false;
        Camera.main.GetComponent<AudioSource>().DOFade(audioVolume, 1);
        ChairModel.Instance.Target = roundDeskFurniture;// FurnitureList[0];
        //给家具加ChairController脚本，并且附上爆炸动画和合并动画
        if (roundDeskFurniture.gameObject.GetComponent<ChairController>() == null)
        {
            roundDeskFurniture.gameObject.AddComponent<ChairController>();
        }
        ChairModel.Instance.Baozha = ClipNameManager.Instance.GetClipName(roundDeskFurniture.name);
        //ChangeScenceObject();
    }
}
