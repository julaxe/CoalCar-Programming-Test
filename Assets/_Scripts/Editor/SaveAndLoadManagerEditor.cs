using _Scripts.Managers;
using UnityEditor;
using UnityEngine;

namespace _Scripts
{
    
    [CustomEditor(typeof(SaveAndLoadManager))]
    public class SaveAndLLoadManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var manager = (SaveAndLoadManager)target;
            if (GUILayout.Button("Delete Data"))
            {
                manager.DeleteData();
            }
        }
    }
}