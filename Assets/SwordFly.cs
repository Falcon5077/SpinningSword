using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SwordFly : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpPower;
    public float rotationPower;
    private bool blueCheck = false;
    public static bool gameStart = false;
    public static bool gameEnd = false;
    public GameObject Title;
    public GameObject TapImg;
    public GameObject KnifeImg;
    public GameObject KnifeImg_2;
    public GameObject SkinBtn;
    public GameObject scoreText;
    public RectTransform[] titleText;
    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_IOS || UNITY_ANDROID
            Application.targetFrameRate = 300;
        #else
            QualitySettings.vSyncCount = 1;
        #endif
        
        gameEnd = false;
        gameStart = false;

        rb = GetComponent<Rigidbody2D>();
    }

    public static int number = 0;
    public TextMeshProUGUI numberText;
    // Update is called once per frame
    void Update()
    {  
        if(gameEnd)
            return;
            
        if(Input.GetMouseButtonDown(0)){

            if(UIManager.isPause) return;
            if(EventSystem.current.IsPointerOverGameObject()) return;
            
            if(!gameStart)
            {
                float time = 0.9f;
                
                GetComponent<Rigidbody2D>().gravityScale = 1f;
                gameStart = true;

                Vector2 endPos = new Vector2(-145,1550);


                Title.GetComponent<RectTransform>().DOAnchorPos(endPos,1f,false).SetEase(Ease.OutSine);

                endPos = new Vector2(0,-1000);
                // //TapImg.GetComponent<RectTransform>().DOAnchorPos(endPos,1f,false).SetEase(Ease.OutSine);

                for(int i = 0; i < titleText.Length; i++)
                {
                    titleText[i].GetComponent<TextMeshProUGUI>().DOColor(new Color(1,1,1,0),time).SetEase(Ease.OutSine);
                }
                
                TapImg.GetComponent<Image>().DOColor(new Color(1,1,1,0),time).SetEase(Ease.OutSine);
                KnifeImg.GetComponent<Image>().DOColor(new Color(1,1,1,0),time).SetEase(Ease.OutSine);
                KnifeImg_2.GetComponent<Image>().DOColor(new Color(1,1,1,0),time).SetEase(Ease.OutSine);
                SkinBtn.GetComponent<Image>().DOColor(new Color(1,1,1,0),time).SetEase(Ease.OutSine);

                scoreText.GetComponent<TextMeshProUGUI>().DOColor(new Color(1,1,1,1),time).SetEase(Ease.OutSine);

                Destroy(TapImg,time);
                Destroy(KnifeImg,time);
                Destroy(KnifeImg_2,time);
                Destroy(SkinBtn,time);

                return;
            }
        
            GetComponent<AudioSource>().Play();
            jumpSword();
            TailController.instance.MakeTail();
        }
    }

    void FixedUpdate()
    {
        if (blueCheck)
        {
            if (transform.rotation.eulerAngles.z < 180f && transform.rotation.eulerAngles.z > 0f)
            {
                rb.angularDrag += 1.5f;
                if (rb.angularVelocity < 200)
                {
                    blueCheck = false;
                    Debug.Log("블루폴스");
                }
            }
        }
        else
        {
          rb.angularDrag = 5;
        }
    }

    public void jumpSword(int way = 1)
    {
        rb.angularVelocity = 0;
        float rotaPower = rotationPower + Random.Range(-30f, 0f);
        rb.AddTorque(rotaPower * way, ForceMode2D.Force);
        rb.velocity = Vector2.up * jumpPower;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(Score.score > Score.bestScore){
            Score.bestScore = Score.score;
            PlayerPrefs.SetInt("BestScore",Score.bestScore);
        }

        if(gameEnd == false)
            StartCoroutine("gameEndRoutine");
    }

    IEnumerator gameEndRoutine()
    {
        gameEnd = true;
        SoundManager.instance.playSound(0);
        SparkSystem.instance.genRedSpark(transform.position);

        Transform[] childs = GetComponentsInChildren<Transform>();
        for(int i = 0; i < childs.Length; i++)
        {
            if(childs[i].tag == "Hit")
            {
                childs[i].GetComponent<HitSystem>().enabled = false;
            }
            if(childs[i].tag == "Knife")
            {
                childs[i].GetComponent<BoxCollider2D>().enabled = false;
                childs[i].parent = null;
                boomEffect(childs[i].gameObject);
            }
        }

        boomEffect(this.gameObject);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("GameOverScene");
    }


    void boomEffect(GameObject target)
    {
        Vector2 randomVector = new Vector2(Random.Range(-1f,1f),Random.Range(1f,10f));

        if(target.GetComponent<Rigidbody2D>() == null)
        {
            target.AddComponent<Rigidbody2D>();
        }

        target.GetComponent<Rigidbody2D>().AddForce(randomVector,ForceMode2D.Impulse);

        target.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-200,200), ForceMode2D.Force);
    }

    public void hitJumpSword(int way = 1)
    {
        rb.angularVelocity = 0;
        blueCheck = true;
        Debug.Log("블루체크트루");
        float rotaPower = -50;
        rb.AddTorque(rotaPower * -1, ForceMode2D.Force);
        rb.velocity = Vector2.up * jumpPower;
    }
}
