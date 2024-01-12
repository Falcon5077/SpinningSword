using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    [SerializeField] private string SceneName;
    public void ReplayGame(){
        SceneManager.LoadScene(SceneName);
    }
}
