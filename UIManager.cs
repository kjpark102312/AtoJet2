using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public Slider slider;
    public GameObject valueSlider;

    public GameObject option;

    public GameObject panel;

    public Transform dashPanel;
    public GameObject dashImage;

    public Text dashText;

    public GameObject exitPanel;

    private int minusValue;

    bool isOption = false;

    void Start()
    {
        slider.maxValue = GameManager.Instance.jetpackGage;
        ProduceDashImage();
    }

    void Update()
    {
        JetpackGage();
        WarningText();


        if(isOption && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            option.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() => {
                panel.SetActive(false);
                option.SetActive(false);
            });
        }
    }

    public void ProduceDashImage()
    {
        while(true)
        {
            if(GameManager.Instance.async.isDone)
            {
                for(int i = 0; i < GameManager.Instance.dashCount; i++)
                {
                    Debug.Log("3");
                    GameObject dashimage = Instantiate(dashImage);
                    dashimage.transform.parent = dashPanel.transform;
                    GameManager.Instance.dashImageList.Add(dashimage);
                }
                break;
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
        
    }

    public void GoStage()
    {
        SceneManager.LoadScene("SelectStage");
        Time.timeScale = 1;
    }

    public void Return()
    {
        GameManager.Instance.LoadScene();
        Time.timeScale = 1;
    }
    
    void JetpackGage()
    {
        slider.value = GameManager.Instance.jetpackGage;

        if(slider.value == 0)
        {
            valueSlider.SetActive(false);

            GameManager.Instance.LoadScene(); 
        }
    }

    void WarningText()
    {
        if(GameManager.Instance.dashCount <= 0 && Input.GetButtonDown("Jump"))
        {
            dashText.DOFade(1f, 0.1f);
            dashText.DOFade(0f, 0.7f);
        }
    }

    public void SettingBtn()
    {
        panel.SetActive(true);
        option.SetActive(true);
        option.transform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() => {
            isOption = true;
            Time.timeScale = 0;
        });
    }
}
