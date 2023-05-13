using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystem : MonoBehaviour
{
    public List<Star> stars = new List<Star>();
 
}
[System.Serializable]
public class Star
{
    public GameObject star;
    private UnityEngine.UI.Image inner;
    public IEnumerator Fill(float speed)
    {
        inner = star.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        while (inner.fillAmount != 1)
        {
            inner.fillAmount += speed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    public IEnumerator Empty(float speed)
    {
        inner = star.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();

        while (inner.fillAmount != 0)
        {
            inner.fillAmount += speed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

}
