using UnityEngine;

public class GardenBoxMaterials : MonoBehaviour
{
    [Header("Garden Box")]
    public MeshRenderer gardenBox;
    public Material boxMaterial;

    [Header("Dirt")]
    public MeshRenderer dirt;
    public Material dirtMaterial;
    void Start()
    {
        
    }
    [ContextMenu("Set Material")]
    public void SetMaterials()
    {
        gardenBox.material = boxMaterial;
        dirt.material = dirtMaterial;
    }
}
