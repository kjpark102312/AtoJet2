using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class MainSceneUI : MonoBehaviour
{
    public GameObject Title;
    public GameObject stageSelectPanel;

    public GameObject Btn;

    public GameObject exitPanel;
    Image image;

    public GameObject helpPanel;

    public GameObject fristPanel;
    public GameObject secondPanel;

    bool isFirst = true;
    bool isSecond = false;

    bool isSelectStage;
    bool isHelpPanel;

    void Start()
    {
        Title.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        Title.transform.DOScale(new Vector3(1f,1f,1f), 1f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetEase(Ease.Linear);
        image = exitPanel.GetComponent<Image>();

    }

    void Update()
    {
        UIAnim();
    }

    void UIAnim()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isSelectStage)
            {
                stageSelectPanel.transform.DOScale(new Vector3(0,0,0), 0.5f);
                Btn.SetActive(true);
                Title.SetActive(true);
                isSelectStage = !isSelectStage;
            }
            else if(isHelpPanel)
            {
                helpPanel.transform.DOScale(new Vector3(0,0,0), 0.5f).OnComplete(()=>{
                    helpPanel.SetActive(false);
                    isHelpPanel = false;
                });
            }
            else
            {
                exitPanel.SetActive(true);
                image.DOFade(1f, 1f);
            }
        }
    }

    public void rightBtn()
    {
        if(isFirst)
        {
            fristPanel.SetActive(false);
            secondPanel.SetActive(true);

            isSecond = !isSecond;
            isFirst = !isFirst;
        }
    }

    public void leftBtn()
    {
        if(isSecond)
        {
            secondPanel.SetActive(false);
            fristPanel.SetActive(true);

            isFirst = !isFirst;
            isSecond = !isSecond;
        }
    }

    public void YesBtn()
    {
        Application.Quit();
    }

    public void NoBtn()
    {
        exitPanel.SetActive(false);
    }

    public void HelpBtn()
    {
        helpPanel.SetActive(true);
        helpPanel.transform.DOScale(new Vector3(1,1,1), 0.5f);
        isHelpPanel = true;
    }

    public void StageSelectPanel()
    {
        isSelectStage = true;
        Btn.SetActive(false);
        Title.SetActive(false);
        stageSelectPanel.transform.DOScale(new Vector3(1,1,1), 0.5f);
    }
}
