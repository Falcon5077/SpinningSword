using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SparkSystem : MonoBehaviour
{
    public GameObject redSpark;
    public GameObject blueSpark;
    public GameObject feather;
    public static SparkSystem instance;
    public bool isBlue = false;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void genRedSpark(Vector3 pos)
    {
        if(isBlue)
            return;
            
        GameObject temp = Instantiate(redSpark,pos,Quaternion.identity);
        Destroy(temp,0.5f);
    }
    public void genBlueSpark(Vector3 pos)
    {
        StopCoroutine("blueOn");
        StartCoroutine("blueOn");
        GameObject temp = Instantiate(blueSpark,pos,Quaternion.identity);
        Destroy(temp,0.5f);
    }

    IEnumerator blueOn()
    {
        isBlue = true;
        yield return new WaitForSeconds(1f);
        isBlue = false;
    }
    public void genFeather(Vector3 pos)
    {
        int r = Random.Range(2,5);

        for(int i = 0; i < r; i++)
        {
            CreateFeather(pos);
        }
    }

    public void CreateFeather(Vector3 pos)
    {
        int[] choices = { -1, 1 };

        Vector2 randomVector = new Vector2(choices[Random.Range(0,2)],Random.Range(0f,-1f));

        GameObject temp = Instantiate(feather,pos,Quaternion.identity);
        temp.GetComponent<SpriteRenderer>().DOColor(new Color(1,1,1,0),1f);
        temp.AddComponent<Rigidbody2D>().AddTorque(Random.Range(200,500) * choices[Random.Range(0,2)], ForceMode2D.Force);
        temp.GetComponent<Rigidbody2D>().AddForce(randomVector,ForceMode2D.Impulse);

        Destroy(temp,1f);

    }
}
