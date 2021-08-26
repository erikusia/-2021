using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadTime : MonoBehaviour
{
    public AddForceBullet addForceBullet;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Slider>().maxValue = addForceBullet.reloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Slider>().value = addForceBullet.rlTime;
    }
}
