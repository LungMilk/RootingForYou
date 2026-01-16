using UnityEngine;
using CustomNamespace.Utilities;
public class TestingGrid : MonoBehaviour
{
    Grid grid;
    private void Start()
    {
        Grid grid = new Grid(4,2,1f,new Vector3(20,0));
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            grid.SetValue(GetMouseWorldPosition(), 56);
        } 
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(GetMouseWorldPosition()));
        }
    }

    //copied from the utils
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = Utilities.GetMouseWorldPositionWithZ(Input.mousePosition,Camera.main);
        vec.z = 0f;
        return vec;
    }
   
}
