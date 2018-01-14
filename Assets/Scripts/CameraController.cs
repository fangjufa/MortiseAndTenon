using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

    public Transform lights;
    public Transform lightsUpTarget;
    public Transform cameraAssistCenter;
    public Transform cameraMoveTarget;
    public Transform cameraVerticalMoveTarget;
    public Transform furnitureStateTarget;
    public Transform componentStateTarget;
    public Transform zoomPoint;
    public Transform furnitureFather;
    public Transform decomFather;

    //public GameObject canvas;
    public BGImg bgImg;

    public static bool isChangeCameraAngle;
    public static bool returnAssistRotation;

    private bool canAction;
    private bool componentAction;

    public float xSpeed = 100;
    public float ySpeed = 100;
    public float zoomOutSpeed = 5.0f;
    public float zoomInSpeed = 5.0f;
    public float zoomInertia = 0.5f;
    public float rotationInertia = 1f;
    public float yMinLimit = -20;
    public float yMaxLimit = 80;

    private List<Transform> cameraTarget = new List<Transform>();

    private Vector3 startRotation;
    private Vector3 startPosition;
    private Vector3 transitPosition;
    private Vector3 cameraPosition;
    private Vector3 cameraStartRotation;
    private Vector3 cameraReturnPosition;
    private Vector3 lightsDownPosition;
    private Vector3 lightsUpPosition;
    private Vector3 velocity = Vector3.zero;
    private Vector3 chairFatherPosition;   
  
    private float x = 0.0f;
    private float y = 0.0f;
    private float mouseX;
    private float mouseY;
    private float verticalYPosition;

    Quaternion smooth;

    void Start()
    {
        startPosition = cameraAssistCenter.transform.position;
        startRotation = cameraAssistCenter.transform.eulerAngles;
        cameraPosition = Camera.main.transform.localPosition;
        lightsDownPosition = lights.position;
        lightsUpPosition = lightsUpTarget.position;

        canAction = true;
    }
    void Update()
    {

    }

    void FixedUpdate()
    {
        //旋转摄像机
        if (!BGImg.isBackToBG && canAction)
        {          
            if (Input.GetMouseButton(0))
            {
                mouseX = Input.GetAxis("Mouse X");
                mouseY = Input.GetAxis("Mouse Y");

                x += Input.GetAxis("Mouse X") * xSpeed * 0.04f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.04f;

                if (!BGImg.isChange) y = ClampAngle(y, yMinLimit, yMaxLimit);
                else y = ClampAngle(y, -85, 85);

                smooth = Quaternion.Euler(y, x, 0);

                cameraAssistCenter.DORotate(smooth.eulerAngles,rotationInertia);
            }
        }
        else
        {
            x = 0;
            y = 0;
        }

        ///<summary>
        ///摄像机缩放效果
        ///</summary>
        //摄像机永远做跟随缩放目标的动作
        Camera.main.transform.DOLocalMove(cameraMoveTarget.localPosition, zoomInertia);
        if (!BGImg.isBackToBG && canAction)
        {
            if (BGImg.isChange && cameraMoveTarget.localPosition.z < -3f)
            {
                cameraMoveTarget.localPosition = zoomPoint.localPosition;
            }
            //放大
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (BGImg.isChange && cameraMoveTarget.localPosition.z > -2.9f)
                {
                    cameraMoveTarget.Translate(Vector3.back * Time.deltaTime * zoomOutSpeed);
                }
                else if (!BGImg.isChange && Camera.main.transform.localPosition.z > -3.8f)
                {
                    cameraMoveTarget.Translate(Vector3.back * Time.deltaTime * zoomOutSpeed);
                }
            }
            //缩小
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (cameraMoveTarget.localPosition.z < -1f)
                {
                    cameraMoveTarget.Translate(Vector3.forward * Time.deltaTime * zoomInSpeed);
                } 
            }
        }

        ///<summary>
        ///进入家居部件界面后可以上下拖动相机旋转中心
        ///</summary>
        if (componentAction)
        {
            //相机旋转中心跟随垂直移动目标
            cameraAssistCenter.transform.DOLocalMove(cameraVerticalMoveTarget.localPosition, 0.5f);
            //当按下鼠标中键时
            if (Input.GetMouseButton(2))
            {
                //获取鼠标Y轴移动数据
                mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;
                //若鼠标Y轴输入大于0且垂直辅助物体未高出0.5
                if (Input.GetAxis("Mouse Y") > 0 && cameraVerticalMoveTarget.localPosition.y - verticalYPosition < 0.5f)
                {
                    //垂直辅助目标进行Y轴移动
                    cameraVerticalMoveTarget.Translate(new Vector3(0, mouseY, 0));
                }
                //若鼠标Y轴输入小于于0且垂直辅助物体未低出0.5
                if (Input.GetAxis("Mouse Y") < 0 && cameraVerticalMoveTarget.localPosition.y - verticalYPosition > -0.5f)
                {
                    //垂直辅助目标进行Y轴移动
                    cameraVerticalMoveTarget.Translate(new Vector3(0, mouseY, 0));
                }
            }
        }
        else
        {
            mouseY = 0;
        }
    }

    /// <summary>
    /// 在家具整体展示界面与家具部件展示界面间切换的视觉效果
    /// </summary>
    
    public void IntoComponent()
    {
        cameraVerticalMoveTarget.position = componentStateTarget.position;
        verticalYPosition = cameraVerticalMoveTarget.localPosition.y;
        cameraMoveTarget.localPosition = cameraPosition;
        //切换动作进行的时候不允许控制摄像机
        canAction = false;
        //重置相机旋转角度
        cameraAssistCenter.transform.DOLocalRotate(Vector3.zero, 0.5f).OnComplete(() =>
        {
            //记录摄像机缩放位置
            cameraReturnPosition = cameraMoveTarget.transform.localPosition;
            //上升灯光
            lights.DOMove(lightsUpPosition, 1f);
            //上升相机旋转中心
            cameraAssistCenter.DOMove(componentStateTarget.position, 1f).OnComplete(() =>
            {
                //下降隐藏家具父物体
                furnitureFather.position -= new Vector3(0, 10, 0);
                //重置动画效果
                ChairController.PlayHebing();
                //切换动作完成后可以控制摄像机
                canAction = true;
                //进入家居部件界面后可以拖动相机旋转中心
                componentAction = true;
            });
        });
    }

    public void BackToOverall()
    {
        //返回家具整体界面时不允许拖动相机旋转中心
        componentAction = false;
        //切换动作进行的时候不允许控制摄像机
        canAction = false;
        //复位摄像机缩放位置
        cameraMoveTarget.transform.DOLocalMove(cameraReturnPosition, 0.5f);
        //复位相机旋转角度
        cameraAssistCenter.transform.DOLocalRotate(Vector3.zero, 0.5f);
        //复位相机旋转中心垂直位置
        cameraAssistCenter.transform.DOMove(componentStateTarget.position, 0.5f).OnComplete(() =>
        {
            //下降灯光
            lights.DOMove(lightsDownPosition, 1f);
            //抬高家具部件防止在家具整体展示界面产生阴影
            if (bgImg.go)
            {
                bgImg.go.transform.DOMoveY(3, 0.5f);
            }
            //上升显示家具父物体
            furnitureFather.position += new Vector3(0, 10, 0);
            //下降相机旋转中心
            cameraAssistCenter.DOMove(furnitureStateTarget.position, 1f).OnComplete(() =>
            {
                //切换动作完成后可以控制摄像机
                canAction = true;
                //删除家具部件
                if (bgImg.go) Destroy(bgImg.go);
            });
        });
        x = 0;
        y = 0;
    }

    public void BackToBG(Button button)
    {
        cameraMoveTarget.localPosition = cameraPosition;
        cameraAssistCenter.transform.DORotate(startRotation, 0.5f).OnComplete(() =>
        {
            button.enabled = true;
        });
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
