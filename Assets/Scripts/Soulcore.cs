using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Soulcore : MonoBehaviour, IPointerClickHandler
{
    private Image img ;

    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<Image>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Bird player = GameObject.Find("Player").GetComponent<Bird>();
        player.setItem(img.sprite, gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
