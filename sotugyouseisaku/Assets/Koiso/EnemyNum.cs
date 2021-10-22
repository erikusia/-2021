using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyNum : MonoBehaviour
{
    private int num=0;

    public int Num
    {
        get { return num; }
        set { num = value; }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void DeathNum()
    {
        num++;
        Debug.Log(num);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (num==12)
        {
            SceneManager.LoadScene("GameClear");
        }
    }

    
}
