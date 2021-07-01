using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartCo(int mapIndex)
    {
        StartCoroutine(GameManager.Instance.LoadStage(mapIndex));
        GameManager.Instance.mapIndex = mapIndex;
    }
}
