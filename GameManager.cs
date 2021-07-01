using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{

    public UIManager uIManager;

    public float jetpackGage = 100f;

    public List<GameObject> mapList;
    public List<GameObject> dashImageList;

    public int mapIndex = 0;
    public int dashCount = 3;
    
    public Vector3 originTr;

    public AsyncOperation async;

    private Slider slider;
    private float timer;

    public void LoadScene()
    {
        dashCount = 3;
        jetpackGage = 100f;
        uIManager = FindObjectOfType<UIManager>();
        SceneManager.LoadScene("LoadingScene");
        StartCoroutine(LoadAsynSceneCoroutine(mapIndex));
        uIManager.ProduceDashImage();
    }

    public IEnumerator LoadAsynSceneCoroutine(int _mapindex) 
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("InGame");

        dashImageList.Clear();

        operation.allowSceneActivation = false;

        while (true) 
        { 
            yield return null; 
            timer += Time.deltaTime; 

            Debug.Log(operation.allowSceneActivation);

            if(!operation.isDone)
            {
                if (operation.progress < 0.9f) 
                { 
                    slider.value = timer; 
                    if (slider.value >= operation.progress) 
                    { 
                        timer = 0f; 
                    } 
                }               
                else 
                { 
                    slider.value = timer; 
                    if (slider.value == 1) 
                    {
                        operation.allowSceneActivation = true;
                    } 
                }
            }

            if(operation.isDone)
            {
                operation.allowSceneActivation = true;

                Transform Parent = GameObject.Find("Grid").GetComponent<Transform>();

                GameObject grid = GameObject.Instantiate(GameManager.Instance.mapList[_mapindex]);
                grid.transform.parent = Parent.transform;
                grid.transform.localPosition = new Vector3(0,0,0);

                yield break;
            }
        }
    }

    public IEnumerator LoadStage(int _mapindex)
    {
        async = SceneManager.LoadSceneAsync("InGame");
        
        dashImageList.Clear();
        
        while(true)
        {
            yield return null;

            Debug.Log(_mapindex);
            Debug.Log(async.isDone);
            
            if(async.isDone)
            {
                Transform Parent = GameObject.Find("Grid").GetComponent<Transform>();

                GameObject grid = GameObject.Instantiate(GameManager.Instance.mapList[_mapindex]);
                grid.transform.parent = Parent.transform;

                grid.transform.localPosition = new Vector3(0,0,0);

                yield break;
            }
        }
    }
}
    

