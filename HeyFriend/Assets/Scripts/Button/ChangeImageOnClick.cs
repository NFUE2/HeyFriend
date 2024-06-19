using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageOnClick : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    public Image image1;
    public Image image2;
    public Sprite changeSprite;

    private bool isSelected = false;

    void Start()
    {
        btn1.onClick.AddListener(() => OnButtonClick(1));
        btn2.onClick.AddListener(() => OnButtonClick(2));        
    }

    void OnButtonClick(int btnNumber)
    {
        if (isSelected)
        {
            return;
        }

        if (btnNumber == 1)
        {
            image1.sprite = changeSprite;
            btn2.interactable = false;
        }
        else if (btnNumber == 2)
        {
            image2.sprite = changeSprite;
            btn1.interactable = false;
        }

        isSelected = true;
    }
}
