using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AllControl;


public class playerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private BoxCollider2D coll;
    //private int cherriesNum = GameManager.getInstance.score;
    private int thisCherriesNum = GameManager.getInstance().score;
    private int dieNum = GameManager.getInstance().dieNum;
    private int thisIsDie = 0;  //判断是否死亡

    //private GameObject UIobject;

    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource deathAudio;
    [SerializeField] private AudioSource cherryAudio;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private Text cherriesText;  //左上角得分字符串
    [SerializeField] private Text dieText;  //死亡后屏幕中间字符串
    [SerializeField] private GameObject ZongMenButtons;  //加入，退出宗门的按钮
    [SerializeField] private Text thisDieNum;  //加入，退出宗门的按钮


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        cherriesText.text = "得分:" + GameManager.getInstance().score;
        thisDieNum.text = "死亡次数：" +GameManager.getInstance().dieNum;
        if (GameManager.getInstance().isShowZongMen)
        {
            ZongMenButtons.SetActive(true);
        }
        //UIobject = GameObject.Find("UI");
        //UIobject = GameObject.Find("die");
        //Debug.Log(UIobject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("update函数正在执行");
        float x = Input.GetAxis("Horizontal");
        //若有左右移动的输入，则给一个向左/右的加速度使角色移动，并将跑步状态改为2触发跑步动画
        if (x != 0)
        {
            rb.velocity = new Vector2(x * 7f, rb.velocity.y);
            animator.SetInteger("state",2);
        }else  //否则关闭跑步动画
        {
            animator.SetInteger("state", 1);
        }

        if(rb.velocity.y > .1f)
        {
            animator.SetInteger("state", 3);
        }
        else if (rb.velocity.y < - .1f)
        {
            animator.SetInteger("state", 4);
        }

        //若输入方向为向左则翻转贴图
        if (x < 0f)
        {
            sr.flipX = true;
        }
        else if (x > 0f) 
        { 
            sr.flipX = false;
        }

        //若按下空格则跳跃
        if (  Input.GetKeyDown(KeyCode.Space) &&  (IsGrounded() || GameManager.getInstance().isZongMen  )  )
        {
            jumpAudio.Play();
            rb.velocity = new Vector2(rb.velocity.x, 23f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            cherryAudio.Play();
            Destroy(collision.gameObject);
            thisCherriesNum++;
            cherriesText.text = "得分:" + thisCherriesNum;

        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            //防抖
            if(0 == thisIsDie)
            {
                deathAudio.Play();
                animator.SetTrigger("die");
                thisIsDie = 1;
                thisCherriesNum = GameManager.getInstance().score;

                if (GameManager.getInstance().isZongMen)
                {
                    dieText.text = "菜";
                }
                dieText.enabled = true;


                GameManager.getInstance().dieNum++;
                Debug.Log("总共死亡次数是：" + GameManager.getInstance().dieNum);
                if(GameManager.getInstance().dieNum >= 7)
                {
                    Debug.Log("是否要拜入宗门？");
                    GameManager.getInstance().isShowZongMen = true;
                }



            }
        }
        if (collision.gameObject.CompareTag("check"))
        {
            GameManager.getInstance().score = thisCherriesNum;
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f , jumpableGround);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
