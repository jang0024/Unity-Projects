using UnityEngine;
using UnityEditor;

public class MakeCubeUI : EditorWindow
{ 
    [Range(1, 10)]
    static int cubeSides = 1; 
    // Add menu item
    [MenuItem("Prefab Generate/Make NxNxN Cube")]
    static void Init()
    {
        EditorWindow window = EditorWindow.CreateInstance<MakeCubeUI>();
        window.Show();
        window.maxSize = new Vector2(230f, 150f);
    }

    Rect buttonRect;
    void OnGUI()
    {
        {
            //GUILayout.Label("Editor window with Popup example", EditorStyles.boldLabel);
            GUILayout.Label("Input cube length/side(s)", EditorStyles.boldLabel); 
            cubeSides = EditorGUILayout.IntField("Any integer from 1 to 10 (inclusive)", cubeSides); 
            if (GUILayout.Button("Make new cube", GUILayout.Width(200)))
            {
                if (cubeSides>10 || cubeSides<1) {
                    Debug.Log("invalid side value received");
                    return;
                }
                Debug.Log("making "+cubeSides+"-sided cube...");
                MakeCube.MakeNSidedRubik(cubeSides);
            }
            if (Event.current.type == EventType.Repaint) buttonRect = GUILayoutUtility.GetLastRect(); 
        }
    }
}
