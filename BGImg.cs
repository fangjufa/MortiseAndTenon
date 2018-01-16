using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BGImg : MonoBehaviour {

    //private Vector3 changePosition;
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
    //进入模块界面的按钮。
    //public Button EnterBtn;
    ////返回到整体界面的按钮
    //public Button BackBtn;
    //public Text noteInFurniture;
    //public Image logoInFurniture;

    public FurnitureUI furnitureUI;
    /*----------------------------------------------------------------------*/

    /*-----------------下面的UI是在有透明圆圈的界面才显示的-----------------------*/
    ////带圆圈的透视UI Image。
    //public Image BG;
    ////该按钮是覆盖圆圈的，但是是全透明的。
    //public Button ChairBtn;
    ////在右下显示椅子名字的UI
    ////public Text ChairName;
    //public Text nameOfFurniture;

    //public Image logoInBG;
    public ChooseFurnitureUI chooseFurnitureUI;
    public GameObject bgPanel;
    /*------------------------------------------------------------------------*/

    //椅子的每个小部件的父物体
    public Transform ChairFather;
    
    public Transform roundDeskFurniture;

    /*-------------在显示圈椅小部件时的界面出现的UI--------------------------*/
    //public Button turnBackButton;
    //public Button showDeskButton;
    //public Button showBackrestButton;
    //public Button showLegButton;
    //public Button resetButton;
    //public Text noteInComponent;
    //public Image logoInComponent;
    //public Text nameOfComponent;
    public ComponentUI componentUI;
    /*-----------------------------------------------------------*/

    ///*--------------退出界面UI-------------------*/
    //public Button reloadButton;
    //public Button continueButton;
    //public Button exitButton;
    public GameObject escPanel;
    //public ESCUI escUI;
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
        //changePosition = new Vector3(0, 10, 0);
        
        ////面板1--整体家居界面
        //EnterBtn.onClick.AddListener(ChangeScence);
        //BackBtn.onClick.AddListener(ShowBG);
        //ChairBtn.onClick.AddListener(OnChairBtnClick);

        //面板2--家居小部件界面
        //showDeskButton.onClick.AddListener(OnDeskButton);
        //showBackrestButton.onClick.AddListener(OnBackrestButton);
        //showLegButton.onClick.AddListener(OnLegButton);
        //resetButton.onClick.AddListener(OnReturnState);
        //turnBackButton.onClick.AddListener(OnTurnBack);

        ////esc面板
        //reloadButton.onClick.AddListener(ReloadScence);
        //continueButton.onClick.AddListener(ContinueGame);
        //exitButton.onClick.AddListener(ExitGame);

        roundDeskFurniture.GetComponent<BoxCollider>().enabled = false;
        //foreach (Transform furniture in FurnitureList) 
        //{
        //    furniture.GetComponent<BoxCollider>().enabled = false;
        //}
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

    //private void OnChairBtnClick()
    //{
    //    //进入整体家居展示界面
    //    isBackToBG = false;
    //    ChairBtn.enabled = false;
        
    //    HideImage(noteInFurniture.gameObject, true);
    //    HideImage(logoInBG.gameObject, true);

    //    HideText(ChairBtn.gameObject, true);
    //    HideText(nameOfFurniture.gameObject, true);

    //    Camera.main.GetComponent<AudioSource>().DOFade(audioVolume, 1);

    //    BG.DOFade(0, time).OnComplete(() =>
    //    {
    //        bgPanel.SetActive(false);
    //        roundDeskFurniture.GetComponent<BoxCollider>().enabled = true;
    //    });

    //    ChairModel.Instance.Target = roundDeskFurniture;// FurnitureList[0];
    //    //给家具加ChairController脚本，并且附上爆炸动画和合并动画
    //    if (roundDeskFurniture.gameObject.GetComponent<ChairController>() == null) { roundDeskFurniture.gameObject.AddComponent<ChairController>(); }
    //     ChairModel.Instance.Baozha = ClipNameManager.Instance.GetClipName(roundDeskFurniture.name);
    //    ChangeScenceObject();
    //}

    private void ChangeScenceObject()
    {
        //根据所选家居调整小部件场景
        //int i = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().numbersOfComponent;

        //if (i == 3)
        //{
        //    //调整家居小部件按钮外观和数量
        //    showBackrestButton.gameObject.SetActive(true);
        //    showDeskButton.image.sprite = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().deskSprite;
        //    showLegButton.image.sprite = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().legSprite;
        //    showBackrestButton.image.sprite = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().backrestSprite;

        //    //调整家居小部件对应的预设
        //desk = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().desk;
        //leg = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().leg;
        //backrest = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().backrest;
        //}
        //else if (i == 2)
        //{
        //    //调整家居小部件按钮外观和数量
        //    showDeskButton.image.sprite = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().deskSprite;
        //    showLegButton.image.sprite = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().legSprite;
        //    showBackrestButton.gameObject.SetActive(false);

        //    //调整家居小部件对应的预设
        //    desk = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().desk;
        //    leg = roundDeskFurniture.GetChild(0).GetComponent<ChairAttributes>().leg;
        //}
        //else return;
    }

    public void IntoComponentScene()
    {
        //进入家居小部件展示场景
        isChange = true;
        //EnterBtn.enabled = false;
        
        //HideImage(EnterBtn.gameObject, true);
        //HideImage(BackBtn.gameObject, true);
        //HideText(noteInFurniture.gameObject, true);

        camController.GetComponent<CameraController>().IntoComponent();
        
        //logoInFurniture.transform.GetComponent<Image>().DOFade(0, time).OnComplete(() =>
        //{
        //    turnBackButton.enabled = true;            
        //    canvasOne.SetActive(false);
        //    HideImage(resetButton.gameObject, false);
        //    HideImage(showLegButton.gameObject, false);
        //    HideImage(showBackrestButton.gameObject, false);
        //    HideImage(showDeskButton.gameObject, false);
        //    HideImage(turnBackButton.gameObject, false);
        //    HideImage(logoInComponent.gameObject, false);
        //    HideText(noteInComponent.gameObject, false);
        //});

        transitName = roundDeskFurniture.name;
        //Debug.Log(FurnitureList[0].name);
    }

    public void ShowComponentUI()
    {
        
        canvasOne.SetActive(false);

        componentUI.Show();
        //turnBackButton.enabled = true;
        //HideImage(resetButton.gameObject, false);
        //HideImage(showLegButton.gameObject, false);
        //HideImage(showBackrestButton.gameObject, false);
        //HideImage(showDeskButton.gameObject, false);
        //HideImage(turnBackButton.gameObject, false);
        //HideImage(logoInComponent.gameObject, false);
        //HideText(noteInComponent.gameObject, false);
    }

    public void ShowBG()
    {
        ChairController.PlayHebing();
        //显示BG
        isBackToBG = true;
        chooseFurnitureUI.gameObject.SetActive(true);
        //bgPanel.SetActive(true);
        ///*----------------显示带圈的UI界面----------------*/
        //HideImage(logoInBG.gameObject, false);

        //HideText(ChairBtn.gameObject, false);
        //HideText(nameOfFurniture.gameObject, false);
        ///*--------------------------------------------------------*/

        camController.GetComponent<CameraController>().BackToBG();

        Camera.main.GetComponent<AudioSource>().DOFade(1, 1);

//        BG.DOFade(1, time);//.OnComplete(() =>
    }

    public void OnTurnBack()
    {
        //返回整体家居界面
        //if (go) Destroy(go);
        camController.GetComponent<CameraController>().BackToOverall();
        //turnBackButton.enabled = false;
        
        isChange = false;

        canvasOne.SetActive(true);

        //HideImage(showLegButton.gameObject, true);
        //HideImage(showBackrestButton.gameObject, true);
        //HideImage(showDeskButton.gameObject, true);
        //HideImage(turnBackButton.gameObject, true);
        //HideImage(logoInComponent.gameObject, true);
        //HideText(noteInComponent.gameObject, true);
        //HideText(nameOfComponent.gameObject, true);

        //resetButton.transform.GetComponent<Image>().DOFade(0, time).OnComplete(() =>
        //{
        //    //HideImage(EnterBtn.gameObject, false);
        //    //HideImage(BackBtn.gameObject, false);           
        //    //HideImage(logoInFurniture.gameObject, false);
        //    //HideText(noteInFurniture.gameObject, false);
        //    nameOfComponent.text = null;
        //    furnitureUI.gameObject.SetActive(true);
        //    //EnterBtn.enabled = true;
        //});       

        //showDeskButton.interactable = true;
        //showBackrestButton.interactable = true;
        //showLegButton.interactable = true;

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

    //private void RefreshComponentName()
    //{
    //    if (nameOfComponent.text != null)
    //    {
    //        nameOfComponent.transform.GetComponent<Text>().DOFade(0, 0.5f).OnComplete(() =>
    //        {
    //            nameOfComponent.text = ComponentAttributes.nameOfComponent;
    //            nameOfComponent.transform.GetComponent<Text>().DOFade(1, 0.5f);
    //        });
    //    }
    //    else
    //    {
    //        nameOfComponent.text = ComponentAttributes.nameOfComponent;
    //        nameOfComponent.transform.GetComponent<Text>().DOFade(1, time);
    //    }
    //}

    public void OnDeskButton()
    {
        goComponent = desk;
        transitName = roundDeskFurniture.name + "D_desk";
        //Debug.Log(transitName);

        RefreshObject();
//        RefreshComponentName();

        //showDeskButton.interactable = false;
        //showBackrestButton.interactable = true;
        //showLegButton.interactable = true;
    }

    public void OnBackrestButton()
    {
        goComponent = backrest;
        transitName = roundDeskFurniture.name + "D_backrest";
        //Debug.Log(transitName);

        RefreshObject();
//        RefreshComponentName();

        //showDeskButton.interactable = true;
        //showBackrestButton.interactable = false;
        //showLegButton.interactable = true;
    }

    public void OnLegButton()
    {
        goComponent = leg;
        transitName = roundDeskFurniture.name + "D_leg";
        //Debug.Log(transitName);

        RefreshObject();
        //RefreshComponentName();

        //showDeskButton.interactable = true;
        //showBackrestButton.interactable = true;
        //showLegButton.interactable = false;
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

    private void HideImage(GameObject gameObject,bool isHide)
    {
        int time = 1;
        int alpha = isHide?0:1;
        
        gameObject.GetComponent<Image>().DOFade(alpha, time);
    }

    private void HideText(GameObject gameObject, bool isHide)
    {
        int time = 1;
        int alpha = isHide ? 0 : 1;
        gameObject.GetComponent<Text>().DOFade(alpha, time);
    }

    private void HideGrphic(Graphic graphic,bool isHide)
    {
        int time = 1;
        int alpha = isHide ? 0 : 1;
        graphic.DOFade(alpha, time);
    }

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
                startImg.gameObject.SetActive(true);
                startImg.GetComponent<Image>().DOFade(1, time);
            }
        }
        else
        {
            count = 0;
        }
    }

    private void PressEsc()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !escPanel.gameObject.activeSelf)
        {
            boxCollider.SetActive(true);
            //escUI.gameObject.SetActive(true);
            escPanel.SetActive(true);

            //continueButton.enabled = true;
            //continueButton.interactable = true;

            //reloadButton.enabled = true;
            //reloadButton.interactable = true;

            //exitButton.enabled = true;
            //exitButton.interactable = true;

            //escPanel.GetComponent<Image>().DOFade(1, 0.5f).OnComplete(() =>
            //{
            //    Time.timeScale = 0;
            //});
            //reloadButton.GetComponent<Image>().DOFade(1, 0.5f);
            //continueButton.GetComponent<Image>().DOFade(1, 0.5f);
            //exitButton.GetComponent<Image>().DOFade(1, 0.5f);
        }
        if (escPanel.gameObject.activeSelf)
        {
            isInEsc = false;
        }
        else
        {
            isInEsc = true;

            /*-------------由于远程的原因，暂时先注释掉这一句------------
            IdleWating();
            ------------------------------------------------------------*/
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

    //private void ReloadScence()
    //{
    //    reloadButton.interactable = false;
    //    exitButton.enabled = false;
    //    continueButton.enabled = false;
    //    SceneManager.LoadScene("Loading");
    //}

    //private void ContinueGame()
    //{
    //    exitButton.enabled = false;
    //    reloadButton.enabled = false;
    //    continueButton.interactable = false;
    //    Time.timeScale = 1;
    //    reloadButton.GetComponent<Image>().DOFade(0, 0.5f);
    //    continueButton.GetComponent<Image>().DOFade(0, 0.5f);
    //    exitButton.GetComponent<Image>().DOFade(0, 0.5f);
    //    escPanel.transform.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(() =>
    //    {
    //        boxCollider.SetActive(false);
    //        escPanel.SetActive(false);
    //    });

    //}

    //private void ExitGame()
    //{
    //    exitButton.interactable = false;
    //    Application.Quit();
    //}
}
