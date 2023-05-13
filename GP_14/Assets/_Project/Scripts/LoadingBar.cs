using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class LoadingBar : MonoBehaviour
{

    public GameObject bar;
    public int timer;

    

    // Start is called before the first frame update
    void Start()
    {
        AnimateBar();  
    }

   

    public void AnimateBar()
    {
        
        LeanTween.scaleX(bar, 1, timer);
    }
}
