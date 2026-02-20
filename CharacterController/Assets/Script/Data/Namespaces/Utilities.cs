using UnityEngine;

namespace CustomNamespace.Utilities
{
    public static class Utilities {
         public static Vector3 GetMouseWorldPositionWithZ()
         {
             return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
         }

         public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
         {
             return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
         }
         public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
         {
             Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
             return worldPosition;
         }

        public static bool TryGetMouseWorldPositionXZ(out Vector3 worldPos,float y = 0f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0, y, 0));

            if (plane.Raycast(ray,out float distance))
            {
                worldPos = ray.GetPoint(distance);
                return true;
            }

            worldPos = default;
            return false;
        }
        public static Vector3 GetMouseWorldPositionXZ(float y = 0f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0, y, 0));

            if (plane.Raycast(ray, out float distance))
            {
                return ray.GetPoint(distance);
            }

            return Vector3.zero;
        }

        public static TextMesh CreateWorldTextObject(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 1, TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment textAlignment = TextAlignment.Center, int sortingOrder = 0)
         {
             Color color = Color.white;
             return WorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
         }
         public static TextMesh WorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
         {
             GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
             Transform transform = gameObject.transform;
             transform.SetParent(parent, false);
             transform.localPosition = localPosition;
             TextMesh textMesh = gameObject.GetComponent<TextMesh>();
             textMesh.anchor = textAnchor;
             textMesh.alignment = textAlignment;
             textMesh.text = text;
             textMesh.fontSize = fontSize;
             textMesh.color = color;
             textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
             return textMesh;
         }

    }
}
