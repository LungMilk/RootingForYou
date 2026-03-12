using UnityEngine;

public class AnxietyFog : MonoBehaviour
{
    public GameObject anxietyFogPrefab;
    public Transform gardenBoxPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(anxietyFogPrefab, gardenBoxPosition);
        Debug.Log("working");
    }

    private void OnCollisionExit(Collision collision)
    {
        Destroy(anxietyFogPrefab);
    }
}
