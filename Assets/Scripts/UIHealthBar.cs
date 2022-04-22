using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{

    public static UIHealthBar instance { get; private set; }

    public Image Mask;
    float originalSize;

    // Start is called before the first frame update
    void Start()
    {
        originalSize = Mask.rectTransform.rect.width;
    }

    private void Awake()
    {
        instance = this;
    }

    public void SetValue(float value )
    {
        Mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
