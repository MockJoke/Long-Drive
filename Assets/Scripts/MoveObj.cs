using UnityEngine;

public class MoveObj : MonoBehaviour
{
    public float speed = 0.01f;
    [SerializeField] private GenerateObjects GeneratedObj;
    
    void Start()
    {
        if (GeneratedObj == null)
            GeneratedObj = FindObjectOfType<GenerateObjects>();
        
        speed = GeneratedObj.speed;
    }
    
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);  //to move the enemy car on road by decreasing its y component gradually 
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DownBarrier"))
        {
            Destroy(this.gameObject);
        }
    }
}
