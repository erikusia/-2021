using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun_UI : MonoBehaviour
{
    public AddForceBullet addForceBullet;
    public GameObject slider, texture;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (addForceBullet.isReload)
        {
            slider.SetActive(true);
            texture.SetActive(false);
        }
        else
        {
            texture.SetActive(true);
            slider.SetActive(false);
        }
    }
}
