using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairAnimatorSupport : MonoBehaviour {

    private bool isCanDo;

    private int i = 1;

    public bool IsCanDo
    {
        get
        {
            return isCanDo;
        }

        set
        {
            isCanDo = value;
        }
    }

    // Use this for initialization
    void Start () {
        isCanDo = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void PlayStart()
    {
        IsCanDo = false;
        gameObject.AddComponent<AudioSource>();
        gameObject.GetComponent<AudioSource>().loop = false;
        if (i == 1)
        {
            gameObject.GetComponent<AudioSource>().clip = gameObject.GetComponent<AudioClipS>().分散;
            i += 1;
        }
        else if(i == 2)
        {
            gameObject.GetComponent<AudioSource>().clip = gameObject.GetComponent<AudioClipS>().合并;
            i -= 1;
        }
        gameObject.GetComponent<AudioSource>().Play();
    }

    private void PlayOver()
    {
        IsCanDo = true;
        Destroy(gameObject.GetComponent<AudioSource>());
    }
}
