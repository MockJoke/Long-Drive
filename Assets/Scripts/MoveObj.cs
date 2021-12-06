using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObj : MonoBehaviour
{
    public float speed = 0.01f;
    GenerateObjects GeneratedObj;
    // Start is called before the first frame update
    void Start()
    {
        GeneratedObj = FindObjectOfType<GenerateObjects>();
        speed = GeneratedObj.speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);  //to move the enemy car on road by decreasing its y component gradually 
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DownBarrier")
        {
            Destroy(this.gameObject);
        }
    }

}
