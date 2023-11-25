using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private Renderer Roads;
    private float y;

    void Start()
    {
        if (Roads == null)
            Roads = GetComponent<Renderer>();
    }

    void Update()
    {
        y += Time.deltaTime;
        Vector2 offset = new Vector2(0, y);     //set offset of road 
        Roads.material.SetTextureOffset("_MainTex", offset);    //move road by changing the texture  
    }
}
