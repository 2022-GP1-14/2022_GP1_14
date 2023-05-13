using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ClickObject : MonoBehaviour
{
    public UnityEvent OnMouseDownEvent;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }
    void OnMouseDown()
    {
       
        OnMouseDownEvent.Invoke();
        Debug.Log("Click!");
        _renderer.material.color =
            _renderer.material.color = Color.green;
    }

    
}
