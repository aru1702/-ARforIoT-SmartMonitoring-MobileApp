using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animationLoading : MonoBehaviour
{

    public Sprite[] animatedLoading;
    public Image animatedImageObj;
    public GameObject thisPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (thisPanel.activeSelf == true) {
        //     animatedImageObj.sprite = animatedLoading [ (int) (Time.time * 20) % animatedLoading.Length ];
        //     // Debug.Log("loading active");
        // }

        animatedImageObj.sprite = animatedLoading [ (int) (Time.time * 20) % animatedLoading.Length ];
    }
}
