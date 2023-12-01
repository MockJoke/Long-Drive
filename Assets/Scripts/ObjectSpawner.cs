using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private ObjectMovement[] objPool = new ObjectMovement[9];
    [SerializeField] private float delayTimer = 5f;
    [SerializeField] private float leftBoundary = -2.2f;
    [SerializeField] private float rightBoundary = 2.2f;
    private float timer = 0f;
    
    void Start()
    {
        // InvokeRepeating(nameof(GenerateObj), 0f, 5f);
        timer = delayTimer;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GenerateObj();

            timer = delayTimer;
        }
    }

    private void GenerateObj()
    {
        int i = Random.Range(0, objPool.Length);
        Vector2 pos = new Vector2(Random.Range(leftBoundary, rightBoundary), transform.position.y);     //set pos of generation of the car 
        Instantiate(objPool[i].gameObject, pos, Quaternion.identity, transform);        //instantiate the car on parent pos
    }

    public void SetSpeed(float s)
    {
        foreach (var obj in objPool)
        {
            obj.speed = s;
        }
    }
}
