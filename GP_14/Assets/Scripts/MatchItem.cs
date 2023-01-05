using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MatchItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerEnterHandler, IPointerUpHandler {

    static MatchItem hoverItem;
    public GameObject linePrefab;
    [SerializeField] Image itemName;
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

    //void Start()
    //{
    //    apple.SetActive(true);
    //    Banana.SetActive(true);
    //    A.SetActive(true);
    //    B.SetActive(true);

    //    Dog.SetActive(false);
    //    Cat.SetActive(false);
    //    C.SetActive(false);
    //    D.SetActive(false);

    //}
    public void OnPointerDown (PointerEventData eventData) //2
    {
        Debug.Log("Lamia");
        line = Instantiate(linePrefab, transform.position, Quaternion.identity, transform.parent.parent);
        UpdateLine(eventData.position);
        

    }

    public void OnDrag(PointerEventData eventData) //6
                                                   { 
        UpdateLine(eventData.position);
        Debug.Log("OnDrag");
        
    }

    public void OnPointerUp(PointerEventData eventData) {
        counter++;

        if (!this.Equals(hoverItem) && itemName.Equals(hoverItem.itemName)) {

                UpdateLine(hoverItem.transform.position);
                Destroy(hoverItem);
                Destroy(this);
                MatchLogic.AddPoint();
            Debug.Log("OnPUp");//10

        }
        else {
            Debug.Log("else");
            Destroy(line);
        }
        Debug.Log("OnPointerUp");   //11 
        
        Debug.Log(counter);//4
       
    }
    
    public void OnPointerEnter(PointerEventData eventData)//1
    {
        hoverItem = this;
        Debug.Log("OnPointerEnter");
        
    }

    void UpdateLine(Vector3 position)//3 //5 //7
    {

        // update direction
        Vector3 direction = position - transform.position;
        line.transform.right = direction;
        // update scale
        line.transform.localScale = new Vector3(direction.magnitude, 1, 1);
       
        Debug.Log("Unity");
        if (counter == 2)
        {
            Debug.Log("Ruba");
            apple.SetActive(false);
            Banana.SetActive(false);
            A.SetActive(false);
            B.SetActive(false);

            Dog.SetActive(true);
            Cat.SetActive(true);
            C.SetActive(true);
            D.SetActive(true);

        }
       

    }
}