using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.Rendering.STP;

public class FvxSpawn : MonoBehaviour
{
    public GameObject vfx1;
    public GameObject vfx2;
    public Vector3 positionOffset = Vector3.zero;
    public Transform playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SpawnBothVFX()
    {
        Vector3 spawnPosition = playerTransform.position + positionOffset;
        Instantiate(vfx1, spawnPosition, Quaternion.identity);
        Instantiate(vfx2, spawnPosition, Quaternion.identity);
    }
   
}
