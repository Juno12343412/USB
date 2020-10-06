using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using View;

public class TitleCanvas : BaseScreen<MainGame>
{
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            SceneManager.LoadScene("Game");
        }
    }

    //protected override sealed void Awake()
    //{
    //    
    //}

    public override sealed void ShowScreen()
    {
        base.ShowScreen();
    }

    public override sealed void HideScreen()
    {
        base.HideScreen();
    }
}
