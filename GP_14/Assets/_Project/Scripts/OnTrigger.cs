using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnTrigger : MonoBehaviour
{
    public UnityEvent OnTriggerEnterEvent, OnTriggerExitEvent;
    public string Tag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tag)
        {
            OnTriggerEnterEvent.Invoke();
            Debug.Log("Isplayeing");
        }
        print(other.gameObject.name + "  OnTriggerEnter");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Tag)
        {
            OnTriggerExitEvent.Invoke();
            Debug.Log("ISpause");
        }
        print(other.gameObject.name+ "  OnTriggerExit");
    }
   
}
