using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    public GameObject[] GenerateObj = new GameObject[9];
    public GameObject Obstacles;
    public float speed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(EnemyCar[i], GenerateCars.transform); 
        InvokeRepeating("GenerateEnemyCar", 0f, 5f);

    }

    public void GenerateEnemyCar()
    {
        int i = Random.Range(0,GenerateObj.Length);
        Vector2 pos = new Vector2(Random.Range(-2.2f, 2.2f), 7.5f);   //set pos of generation of the car 
        GameObject GenerateCars = Instantiate(GenerateObj[i], pos, Quaternion.identity);    //instantiate the car on parent pos
        GenerateCars.transform.SetParent(Obstacles.transform);
    }

    // Update is called once per frame
    void Update()
    {
        //if (accelerating)
        //{
        //    AccelerateCar();
        //    print("speed" + speed);
        //}
        //else if (deaccelerating)
        //{
        //    DeaccelerateCar();
        //    print("speed" + speed);
        //}
  
    }

    public void AccelerateCar()
    {
        speed += 0.01f;
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<MoveObj>().speed = speed;  //to stop already generated enemy cars by making its speed 0
        }
    
    }

    public void DeaccelerateCar()
    {
        if(speed>0.01f)
        {
            speed -= 0.01f;
        }
        else
        {
            speed = 0f; 
        }

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in Enemies)
        {
            enemy.GetComponent<MoveObj>().speed = speed;  //to stop already generated enemy cars by making its speed 0
        }
    }

}
