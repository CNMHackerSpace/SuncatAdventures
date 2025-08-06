using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ReactiveTarget1 : MonoBehaviour
{

    public void ReactToHit()
    {
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {                              
            behavior.SetAlive(false);
        }
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {               
        this.transform.Rotate(-75, 0, 0);

        yield return new WaitForSeconds(1.5f);


        Destroy(this.gameObject);               
    }

}
