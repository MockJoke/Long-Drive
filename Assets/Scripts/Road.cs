using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private Renderer road;
    [SerializeField] private Material[] roadMaterials;
    [SerializeField] private float speed = 0.1f;
    // private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    // private float textureOffset = 0f;
    
    void Awake()
    {
        if (road == null)
            road = GetComponent<Renderer>();
    }

    void Start()
    {
        int roadIndex = PlayerPrefs.GetInt("CurrentRoad", 0);
        road.material = roadMaterials[roadIndex];
    }

    void Update()
    {
        // road.material.mainTextureOffset = new Vector2(0, Time.time * speed);

        road.material.mainTextureOffset += new Vector2(0, Time.deltaTime * speed);
        
        // textureOffset += Time.deltaTime * speed;
        // Vector2 offset = new Vector2(0, textureOffset);             //set offset of road 
        // road.material.SetTextureOffset(MainTex, offset);            //move road by changing the texture
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }
}
