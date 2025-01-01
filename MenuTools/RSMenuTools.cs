using UnityEditor;
using UnityEngine;

public class RSContextMenu : MonoBehaviour
{
    // Add "Add Gravity Zone" to the GameObject context menu
    [MenuItem("GameObject/RS/Add Gravity Zone", false, 10)]
    public static void AddGravityZone()
    {
        GameObject obj = new GameObject("GravityZone");
        obj.AddComponent<BoxCollider>().isTrigger = true;
        obj.AddComponent<GravityZone>();
        Debug.Log("Gravity Zone Created!");
        Selection.activeGameObject = obj;
    }

    // Add "Add Realistic Physics Object" to the GameObject context menu
    [MenuItem("GameObject/RS/Add Realistic Physics Object", false, 11)]
    public static void AddPhysicsObject()
    {
        GameObject obj = new GameObject("PhysicsObject");
        obj.AddComponent<Rigidbody>();
        obj.AddComponent<RealisticPhysicsObject>();
        Debug.Log("Physics Object Created!");
        Selection.activeGameObject = obj;
    }

    // Add "Add Realistic Spring" to the GameObject context menu
    [MenuItem("GameObject/RS/Add Realistic Spring", false, 12)]
    public static void AddSpringObject()
    {
        GameObject obj = new GameObject("SpringObject");
        obj.AddComponent<Rigidbody>();
        obj.AddComponent<RealisticSpring>();
        Debug.Log("Spring Object Created!");
        Selection.activeGameObject = obj;
    }

    [MenuItem("GameObject/RS/ Add Black Hole WIP", false, 13)]
    public static void AddBlackHole(){
        GameObject obj = new GameObject("BlackHole");
        obj.AddComponent<SphereCollider>().isTrigger = true;
        Debug.Log("Black Hole Created");
        Selection.activeGameObject = obj;
    }
}
