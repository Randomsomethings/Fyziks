using UnityEditor;
using UnityEngine;

public class FyziksEditor : EditorWindow
{
    private static Texture2D icon;
private GUIStyle redTextStyle;
private GUIStyle orangeTextStyle;
    [MenuItem("RSUT/Fyziks SDK")]
    public static void ShowWindow()
    {
        // Open the Fyziks SDK window
        FyziksEditor window = GetWindow<FyziksEditor>();

         
    }

    private void OnGUI()
{
    // Initialize the redTextStyle if not already done
    if (redTextStyle == null)
    {
        redTextStyle = new GUIStyle(GUI.skin.label);
        redTextStyle.normal.textColor = Color.red; // Set the text color to red
        redTextStyle.fontStyle = FontStyle.Bold;
    }

    // Initialize the blueTextStyle if not already done
    if (orangeTextStyle == null)
    {
        orangeTextStyle = new GUIStyle(GUI.skin.label);
        orangeTextStyle.normal.textColor = new Color(1.0f, 0.65f, 0.0f); // RGB for orange (255, 165, 0)
    }

    // Add a red title
    GUILayout.Label("Fyziks SDK Tools", redTextStyle);

    // Add a blue descriptive label
    GUILayout.Label("Welcome to the Fyziks SDK! Use the buttons below to create physics objects.", orangeTextStyle);

    // Add buttons
    if (GUILayout.Button("Add Realistic Physics Object"))
    {
        CreatePhysicsObject();
    }

    if (GUILayout.Button("Add Realistic Spring"))
    {
        CreateSpringObject();
    }

    if (GUILayout.Button("Add Gravity Zone"))
    {
        CreateGravityZone();


    }
    if (GUILayout.Button("Add Black Hole"))
    {
        CreateBlackHole();

        
    }

    // Add a footer text
    GUILayout.Label("Created and Maintained By RS (Randomsomethings)", orangeTextStyle);
    GUILayout.Label("NOTE: Black Holes Are Work In Progress, Please, If You Use It Then Change the Sphere Collider Pretty Big. When You've Done That Test The Different Masses To Find Your Perfect One!", orangeTextStyle);
}

    private void CreatePhysicsObject()
    {
        GameObject obj = new GameObject("PhysicsObject");
        obj.AddComponent<Rigidbody>();
        obj.AddComponent<RealisticPhysicsObject>();
        Debug.Log($"Created Physics Object: {obj.name}");
        Selection.activeGameObject = obj;
    }

    private void CreateSpringObject()
    {
        GameObject obj = new GameObject("SpringObject");
        obj.AddComponent<Rigidbody>();
        obj.AddComponent<RealisticSpring>();
        Debug.Log($"Created Spring Object: {obj.name}");
        Selection.activeGameObject = obj;
    }

    private void CreateGravityZone()
    {
        GameObject obj = new GameObject("GravityZone");
        obj.AddComponent<BoxCollider>().isTrigger = true;
        obj.AddComponent<GravityZone>();
        Debug.Log($"Created Gravity Zone: {obj.name}");
        Selection.activeGameObject = obj;
    }

    private void CreateBlackHole()
    {
        GameObject obj = new GameObject("BlackHole");
        obj.AddComponent<SphereCollider>().isTrigger = true;
        obj.AddComponent<BlackHole>();
        Debug.Log($"Created Black Hole!: {obj.name}");
        Selection.activeGameObject = obj;
    }
}
