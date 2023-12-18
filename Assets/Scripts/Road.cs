using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private Renderer road;
    [SerializeField] private float speed = 0.1f;
    private Vector2 offset;
    // private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    void Awake()
    {
        if (road == null)
            road = GetComponent<Renderer>();
    }

    void Update()
    {
        offset = new Vector2(0, Time.time * speed);
        road.material.mainTextureOffset = offset;
        
        // textureOffset += Time.deltaTime;
        // Vector2 offset = new Vector2(0, textureOffset);     //set offset of road 
        // Roads.material.SetTextureOffset(MainTex, offset);    //move road by changing the texture
    }

    public void SetSpeed(float s)
    {
        speed = s;
    }
}
