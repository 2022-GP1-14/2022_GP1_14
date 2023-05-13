using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MatchItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler
{

    static MatchItem hoverItem;
    public GameObject linePrefab;
    [SerializeField] GameObject itemName;
    //public Image itemName;
    private GameObject line;
    public static int counter;
    public GameObject apple;
    public GameObject Banana;
    public GameObject Dog;
    public GameObject Cat;
    public GameObject A;
    public GameObject B;
    public GameObject D;
    public GameObject C;
    public AudioSource source;
    public AudioClip[] correct;
    public AudioClip incorrect;

    public void OnPointerDown(PointerEventData eventData)
    {
        line = Instantiate(linePrefab, transform.position, Quaternion.identity, transform.parent.parent);
        UpdateLine(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateLine(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!this.Equals(hoverItem) && itemName.Equals(hoverItem.itemName))
        {
            UpdateLine(hoverItem.transform.position);
            Destroy(hoverItem);
            Destroy(this);
            MatchLogic.AddPoint();
            source.clip = correct[Random.Range(0, correct.Length)];
            source.Play();
            MatchLogic.cal();
            // this.itemName.SetActive(false);
            //hoverItem.itemName.SetActive(false);
            //Destroy(line);
        }
        else
        {
            Destroy(line);
            MatchLogic.cal();
            source.clip = incorrect;
            source.Play();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverItem = this;
        // Debug.Log("OnPointerEnter");

        // counterCHECK();
    }

    void UpdateLine(Vector3 position)
    {

        // update direction
        Vector3 direction = position - transform.position;
        line.transform.right = direction;
        // update scale
        line.transform.localScale = new Vector3(direction.magnitude, 1, 1);


    }



}