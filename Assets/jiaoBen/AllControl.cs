using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllControl : MonoBehaviour
{
    public class GameManager
    {
        private static GameManager _instance;
        public int score = 0;  //玩家得分
        public int dieNum = 0;  //玩家死亡次数
        public bool isShowZongMen = false;  //宗门按钮是否展示
        public bool isZongMen = false;  //玩家是否拜入宗门

        public static GameManager getInstance()
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }

    }


}
