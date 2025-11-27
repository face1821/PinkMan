using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AllControl;

public class ZongMen : MonoBehaviour
{
    public void BaiRuZongMen2()
    {
        GameManager.getInstance().isZongMen = true;
    }
    public void tuiChuZongMen()
    {
        GameManager.getInstance().isZongMen = false;
    }
}
