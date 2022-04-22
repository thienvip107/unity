using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landcore : MonoBehaviour
{
    public int maxHealth = 3;

    public int health { get { return currentHealth; } }
    int currentHealth;
    public int dame = 1;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentHealth = currentHealth - dame;
        Debug.Log(currentHealth);
        if(currentHealth==0)
        {
            Destroy(gameObject);
        }
    }
}
