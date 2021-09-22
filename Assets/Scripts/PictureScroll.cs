using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PictureScroll : MonoBehaviour
{
    public ScrollRect pictureScrollRect;

    public Button leftButton;

    public Button rightButton;

    public List<Image> pics = new List<Image>();

    public Transform phoneLeftButtonTarget;
    public Transform phoneRightButtonTarget;


    int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SimulateButtonPress(Button button)
	{
        ExecuteEvents.Execute(button.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
        //button.onClick.Invoke();
	}


    public void ScrollToItem(int index)
	{
        index = Mathf.Clamp(index, 0, pics.Count - 1);
        float normalizedScrollValue = (1f / pics.Count) * index;
       
        pictureScrollRect.DOHorizontalNormalizedPos(normalizedScrollValue, 0.5f);
	}


    public void ScrollLeft()
	{
        ScrollToItem(currentIndex--);
        Debug.Log("Scrolled Left");
	}

    public void ScrollRight()
	{
        ScrollToItem(currentIndex++);
        Debug.Log("Scrolled Right");
	}
}
