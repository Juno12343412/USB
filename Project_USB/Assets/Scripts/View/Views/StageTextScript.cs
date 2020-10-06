using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageTextScript : MonoBehaviour
{
    Text StageText;
    // Start is called before the first frame update
    void Start()
    {
        StageText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MainGame.instance.gameinstance.Score >= 10000)
        {
            StageText.text = "-성채3-";
        }
        else if (MainGame.instance.gameinstance.Score >= 5000)
        {
            StageText.text = "-성채2-";

        }
        else if (MainGame.instance.gameinstance.Score >= 2000)
        {
            StageText.text = "-성채1-";
        }
    }
}
