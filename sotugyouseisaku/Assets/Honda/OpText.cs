using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpText : MonoBehaviour
{
    public Text Optext;
    private float TimelineCount;

    // Start is called before the first frame update
    void Start()
    {
        Optext.text = "OPテキスト１";
        TimelineCount = 0;
    }

    void FixedUpdate()
    {
        TimelineCount += Time.deltaTime;
        if (TimelineCount > 6 && TimelineCount <= 7)
        {
            Optext.text = "OPテキスト2";
        }
        if (TimelineCount > 7 & TimelineCount <= 10)
        {
            Optext.text = "OPテキスト3";
        }
        if (TimelineCount > 10 & TimelineCount <= 14)
        {
            Optext.text = "OPテキスト4";
        }
        if (TimelineCount > 14 & TimelineCount <= 16)
        {
            Optext.text = "OPテキスト5";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
