using UnityEngine;

public static class CameraRaycastHandler
{
    private static GameObject camera;

    private static void Setup()
    {
        camera = GameObject.FindGameObjectWithTag("ParentCam").gameObject;
    }

    public static Ray InvokeRay() => CraftRay(Input.mousePosition);
    public static Ray InvokeRay(Vector2 pos) => CraftRay(pos);

    private static Ray CraftRay(Vector2 mousePos)
    {
        // Setupping this ROFL class ))) 
        // ssry for this i siju v 5 utra blyat and ne vduplayu nixuya :(
        if (camera == null) Setup();

        Ray ray = new Ray();
        // crafting ray
        ray.origin = camera.transform.TransformPoint(new Vector3((mousePos.x - Screen.width / 2) / Screen.width * camera.GetComponent<Camera>().orthographicSize * 2 * 16 / 9,
                                                                 (mousePos.y - Screen.height / 2) / Screen.height * camera.GetComponent<Camera>().orthographicSize * 2,
                                                                 0));
        ray.direction = camera.transform.TransformDirection(Vector3.forward);
        return ray;
    }
}
