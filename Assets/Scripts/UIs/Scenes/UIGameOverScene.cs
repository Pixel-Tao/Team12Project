using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOverScene : UIPopupBase
{
    enum Buttons
    {
        
        ReStartButton,
       
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        GetButton(Buttons.ReStartButton).gameObject.BindEvent(IntroSceneEvent);
        return true;
    }


 
    public void IntroSceneEvent()
    {
        Managers.Scene.LoadScene(Defines.SceneType.IntroScene);
    }
}
