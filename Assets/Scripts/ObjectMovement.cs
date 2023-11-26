using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [HideInInspector] public float speed = 0.01f;
    private ObjectSpawner objSpawner;
    
    void Start()
    {
        if (objSpawner == null)
            objSpawner = FindObjectOfType<ObjectSpawner>();
    }
    
    void Update()
    {
        //to move the enemy car on road by decreasing its y component gradually
        transform.position = new Vector3(transform.position.x, transform.position.y - (speed * Time.deltaTime), transform.position.z); 
    }

    private void OnCollisionEnter(Collision other)
    {
        //  If the generated obj collide with an another then destroy the one which was generated last
        if (!other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.transform.position.y > transform.position.y)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
