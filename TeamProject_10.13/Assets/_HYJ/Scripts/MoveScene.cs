using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void MoveToStart()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void MoveToStage1()
    {
        SceneManager.LoadScene("_Stage1");
    }
}
