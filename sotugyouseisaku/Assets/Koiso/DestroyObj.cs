using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Destroytimer", 0.3f);
    }

    IEnumerator Destroytimer(int time)
    {
        while (time >= 0)
        {
            yield return new WaitForSeconds(1);
            --time;
        }
        Destroy(this.gameObject);
    }
}
