using UnityEngine;
using System.Collections;
using UnityEditor;

namespace animationHelper
{
    

    [CustomEditor(typeof(cameraManager))]
    public class cameraManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            cameraManager myScript = (cameraManager)target;

            //myScript.CameraOrder;
            if(GUILayout.Button("Get list of all cameras"))
            {
                myScript.getlistCameras();
            }
        }
    }
}