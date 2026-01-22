using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    //DOES NOT WORK, ADDED FOR LATER HOPEFULLY
    //I am aware this does not work atm as it reguires a global instance as well as some render modifications
    //tutorial: https://www.youtube.com/watch?v=dulosHPl82A&list=PLzDRvYVwl53uhO8yhqxcyjDImRjO9W722&index=5


    //private Transform _visual;
    //private PlacedObjectTypeSO placedObjectTypeSO;

    //private void Start()
    //{
    //    RefreshVisual();
    //    GridBuildingSystem3D.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
    //}

    //private void Instance_OnSeletectChanged(object sender, System.EventArgs e)
    //{
    //    RefreshVisual();
    //}

    //private void LateUpdate()
    //{
    //    Vector3 targetPosition = GridBuildingSystem3D.Instance.GetMouseWorldSnappedPosition();
    //    targetPosition.y = 1f;
    //    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

    //    transform.rotation = Quaternion.Lerp(transform.rotation, GridBuildingSystem3D.Instance.GetPlacedObjectRotation(), Time.deltaTime *15f);
    //}

    //private void RefreshVisual()
    //{
    //    if (_visual != null)
    //    {
    //        Destroy(_visual.gameObject);
    //        _visual = null;
    //    }

    //    PlacedObjectTypeSO placedObjectTypeSO = GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO();

    //    if (placedObjectTypeSO != null)
    //    {
    //        _visual = Instantiate(placedObjectTypeSO._visual,Vector2.zero,Quaternion.identity);
    //        _visual.parent = transform;
    //        _visual.localPosition = Vector3.zero;
    //        _visual.localEulerAngles = Vector3.zero;
    //        SetLayerRecursive(_visual.gameObject, 11);
    //    }
    //}

    //private void SetLayerRecursive(GameObject targetGameObject, int layer)
    //{

    //}
}
