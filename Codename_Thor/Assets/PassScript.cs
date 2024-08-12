using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassScript : MonoBehaviour
{
    // Start is called before the first frame update
    public LogicScript logic;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) 
        {
            logic.AddScore(1);
            Debug.Log("Score incremented!");
        }
       
    }
}
