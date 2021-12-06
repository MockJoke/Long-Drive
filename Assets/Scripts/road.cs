using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class road : MonoBehaviour
{
    Renderer Roads;
    float y;

    // Start is called before the first frame update
    void Start()
    {
        Roads = GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        y += Time.deltaTime;
        Vector2 offset = new Vector2(0, y);     //set offset of road 
        Roads.material.SetTextureOffset("_MainTex", offset);    //move road by changing the texture  
        
    }
}
