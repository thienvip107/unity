using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class List : MonoBehaviour
{
    public GameObject ListItemPrefab;
    public List<Sprite> listSC;

    // Start is called before the first frame update
    void Start()
    {
        Sprite[] EnemyCarSprites = Resources.LoadAll<Sprite>("soulcores");
        int x = 1;
        List listG = GameObject.Find("List").GetComponent<List>();
        listSC = new List<Sprite>(EnemyCarSprites);
        foreach (Sprite animal in EnemyCarSprites)
        {
            x++;
            GameObject newS = Instantiate(ListItemPrefab) as GameObject;
            Image img = newS.GetComponent<Image>();
            img.sprite = animal;
            newS.transform.SetParent(listG.transform, true);
            newS.transform.position = new Vector2(40*x, -50);
        }
        Debug.Log(x);
    }

    public void AddSC(Sprite sp)
    {
        if (sp != null)
        {
            listSC.Add(sp);
        }
    }

    public void RemoveSC(Sprite sp)
    {
        if (sp != null)
        {
            listSC.Remove(sp);
        }
    }

    public Sprite GetLastSC()
    {
            return listSC[listSC.Count-1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
