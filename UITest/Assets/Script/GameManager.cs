using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void OnClickTestButton()
    {
        PopupWindow.instance.PopupCheckWindowOpen(() => {
            Debug.Log("tetstest");
        }, "test", PopupWindow.MsgType.notice, "msg");
    }
}
