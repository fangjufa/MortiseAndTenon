using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickMove : MonoBehaviour {

    public static bool canRotate = true;
    public static bool canMoveComponent = true;
    public static bool isChooseAComponent; 

    public static string chooseObjectName;

    public static float cameraAngle;  

    public static Vector3 currentPosition;

    private float posX;
    private float posY;
    private float posZ;

    private float rotX;
    private float rotY;
    private float rotZ;

    private float mouseX;
    private float mouseY;

    private bool canReadLocalPosition = false;
    private bool canReadGlobalPosition = false;
    private bool reChange;
    private bool isReady;
    private bool readReady;
    private bool isRotation = false;
    private bool isPosition = false;

    //private Vector3 changePosition;
    private Vector3 startLocalPosition;
    private Vector3 startGlobalPosition;
    private Vector3 readLocalPosition;
    private Vector3 readGlobalPosition;

    private Vector3 startLocalRotation;
    private Vector3 readLocalRotation;

    private bool canReadLocalRotation = false;

    public bool CanReadLocalPosition
    {
        get
        {
            return canReadLocalPosition;
        }

        set
        {
            canReadLocalPosition = value;
        }
    }

    public bool CanReadGlobalPosition
    {
        get
        {
            return canReadGlobalPosition;
        }

        set
        {
            canReadGlobalPosition = value;
        }
    }

    public float MouseX
    {
        get
        {
            return mouseX;
        }

        set
        {
            mouseX = value;
        }
    }

    public float MouseY
    {
        get
        {
            return mouseY;
        }

        set
        {
            mouseY = value;
        }
    }

    public bool IsReady
    {
        get
        {
            return isReady;
        }

        set
        {
            isReady = value;
        }
    }

    public Vector3 StartLocalRotation
    {
        get
        {
            return startLocalRotation;
        }

        set
        {
            startLocalRotation = value;
        }
    }

    public Vector3 ReadLocalRotation1
    {
        get
        {
            return readLocalRotation;
        }

        set
        {
            readLocalRotation = value;
        }
    }

    public bool CanReadLocalRotation
    {
        get
        {
            return canReadLocalRotation;
        }

        set
        {
            canReadLocalRotation = value;
        }
    }

    // Use this for initialization
    void Start () {
        startLocalPosition = transform.localPosition;
        startGlobalPosition = transform.position;
        StartLocalRotation = transform.localEulerAngles;

        IsReady = false;
        readReady = true;
        DecomController.ready = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if(CanReadLocalPosition == true)
        {
            if (readLocalPosition - startLocalPosition != Vector3.zero)
            {
                isPosition = true;
            }
            else
            {
                isPosition = false;
            }
            readLocalPosition = transform.localPosition;
            posX = readLocalPosition.x - startLocalPosition.x;
            posY = readLocalPosition.y - startLocalPosition.y;
            posZ = readLocalPosition.z - startLocalPosition.z;
            CanReadLocalPosition = false;
        }

        if (CanReadGlobalPosition == true)
        {
            readGlobalPosition = transform.position;
            CanReadGlobalPosition = false;
        }

        if(CanReadLocalRotation == true)
        {
            if (ReadLocalRotation1 - StartLocalRotation != Vector3.zero)
            {
                isRotation = true;
            }
            else
            {
                isRotation = false;
            }
            ReadLocalRotation1 = transform.localEulerAngles;
            rotX = ReadLocalRotation1.x - StartLocalRotation.x;
            rotY = ReadLocalRotation1.y - StartLocalRotation.y;
            rotZ = ReadLocalRotation1.z - StartLocalRotation.z;
            CanReadLocalRotation = false;
        }

        if (isPosition)
        {
            if (transform.localPosition == startLocalPosition && IsReady == true)
            {
                DecomController.ready += 1;
                IsReady = false;
            }
        }

        if (isRotation)
        {
            if (transform.localEulerAngles == StartLocalRotation && IsReady == true)
            {
                DecomController.ready += 1;
                IsReady = false;
            }
        }    
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (CameraController.isChangeCameraAngle == true)
            {
                MouseX -= Input.GetAxis("Mouse X") * Time.deltaTime * 2;
                MouseY -= Input.GetAxis("Mouse Y") * Time.deltaTime * 2;
                //print("----");
            }
            else if(CameraController.isChangeCameraAngle == false)
            {
                MouseX += Input.GetAxis("Mouse X") * Time.deltaTime * 2;
                MouseY += Input.GetAxis("Mouse Y") * Time.deltaTime * 2;
                //print("++++");
            }     
        }
        else
        {
            MouseX = 0;
            MouseY = 0;
        }

    }

    IEnumerator OnMouseDown()
    {
        reChange = true;
        canRotate = false;
        if (posZ != 0 && reChange)
        {
            MouseX = transform.position.z;
            reChange = false;
        }
        if (posY != 0 && reChange)
        {
            MouseX = transform.position.y;
            reChange = false;
        }
        if (posX != 0 && reChange)
        {
            MouseX = transform.position.x;
            reChange = false;
        }
        if (rotX != 0 && reChange)
        {
            MouseX = transform.localEulerAngles.x;
            reChange = false;
        }
        if (rotY != 0 && reChange)
        {
            MouseX = transform.localEulerAngles.y;
            reChange = false;
        }
        if (rotZ != 0 && reChange)
        {
            MouseX = transform.localEulerAngles.z;
            reChange = false;
        }
        while (Input.GetMouseButton(0)) 
        {
            if (isPosition)
            {
                currentPosition = transform.localPosition;
                //判断平移
                if (posZ > 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, ClampAngle(MouseX, startGlobalPosition.z, readGlobalPosition.z + 0.5f));
                }
                if (posZ < 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, ClampAngle(MouseX, readGlobalPosition.z - 0.5f, startGlobalPosition.z));
                }
                if (posY > 0)
                {
                    transform.position = new Vector3(transform.position.x, ClampAngle(MouseX, startGlobalPosition.y, readGlobalPosition.y + 0.5f), transform.position.z);
                }
                if (posY < 0)
                {
                    transform.position = new Vector3(transform.position.x, ClampAngle(MouseX, readGlobalPosition.y - 0.5f, startGlobalPosition.y), transform.position.z);
                }
                if (posX > 0)
                {
                    transform.position = new Vector3(ClampAngle(MouseX, startGlobalPosition.x, readGlobalPosition.x + 0.5f), transform.position.y, transform.position.z);
                }
                if (posX < 0)
                {
                    transform.position = new Vector3(ClampAngle(MouseX, readGlobalPosition.x - 0.5f, startGlobalPosition.x), transform.position.y, transform.position.z);
                }

                if (Vector3.Distance(startLocalPosition, currentPosition) > 0.005f)
                {
                    IsReady = false;
                    canMoveComponent = true;
                }
                else canMoveComponent = false;

                if (Vector3.Distance(startLocalPosition, currentPosition) <= 0.005f && canMoveComponent == false)
                {
                    transform.localPosition = startLocalPosition;
                    canMoveComponent = false;
                    canRotate = true;
                    if (readReady)
                    {
                        IsReady = true;
                        readReady = false;
                    }
                }
            }

            //判断旋转
            if (isRotation)
            {
                if (StartLocalRotation.x < ReadLocalRotation1.x)
                {
                    transform.localEulerAngles = new Vector3(ClampAngle(MouseX, StartLocalRotation.x, ReadLocalRotation1.x + 20), transform.localEulerAngles.y, transform.localEulerAngles.z);
                }
                if (StartLocalRotation.y < ReadLocalRotation1.y)
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, ClampAngle(MouseX, StartLocalRotation.y, ReadLocalRotation1.y + 20), transform.localEulerAngles.z);
                }
                if (StartLocalRotation.z < ReadLocalRotation1.z)
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, ClampAngle(MouseX, StartLocalRotation.z, ReadLocalRotation1.z + 20));
                }

                if (Mathf.Abs(Mathf.Abs(ReadLocalRotation1.x) - Mathf.Abs(StartLocalRotation.x)) > 5)
                {
                    IsReady = false;
                    canMoveComponent = true;
                }
                if (Mathf.Abs(Mathf.Abs(ReadLocalRotation1.x) - Mathf.Abs(StartLocalRotation.x)) <= 5)
                {
                    transform.localEulerAngles = StartLocalRotation;
                    canMoveComponent = false;
                    if (readReady)
                    {
                        IsReady = true;
                        readReady = false;
                    }
                }
            }
                   
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnMouseUp()
    {
        isChooseAComponent = false;
        canRotate = true;
        MouseX = 0;
        MouseY = 0;
    }

    private static float ClampAngle(float distance, float min, float max)
    {
        return Mathf.Clamp(distance, min, max);
    }
}
