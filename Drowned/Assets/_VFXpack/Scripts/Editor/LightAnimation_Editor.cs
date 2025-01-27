using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEditor;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;


namespace SimpleVFXs
{

#if UNITY_EDITOR

    [CustomEditor(typeof(LightAnimation))]
    class LightAnimation_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            LightAnimation myScript = (LightAnimation)target;

            DrawDefaultInspector();

            GUILayout.Space(10);
            if (GUILayout.Button("Play"))
            {
                myScript.Play();
            }

            if (GUILayout.Button("Play in reverse"))
            {
                myScript.PlayInReverse();
            }

        }
    }

#endif


}