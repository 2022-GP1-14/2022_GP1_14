using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlay1 : MonoBehaviour
{
    public Material playButton;
    public VideoPlayer video;
    // Start is called before the first frame update

    public void Awake()
    {
        video=GetComponent<VideoPlayer>();
    }

    public void PlayVideo()
    {
        video.Play();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
