using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SpawnerManager.ListCubeData))]
public class ListCubeDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var cubeListProp = property.FindPropertyRelative("<CubeData>k__BackingField");

        var displayLabel = "";

        if (cubeListProp is { isArray: true, arraySize: > 0 })
        {
            for (var i = 0; i < cubeListProp.arraySize; i++)
            {
                var element = cubeListProp.GetArrayElementAtIndex(i);
                var colorProp = element.FindPropertyRelative("<Color>k__BackingField");
                var sideProp = element.FindPropertyRelative("<Side>k__BackingField");

                displayLabel += $"{colorProp.enumDisplayNames[colorProp.enumValueIndex]} - {sideProp.enumDisplayNames[sideProp.enumValueIndex]} | ";

            }

            if (cubeListProp.arraySize == 2)
            {
                var time1 = cubeListProp?.GetArrayElementAtIndex(0)?.FindPropertyRelative("<TimeInBeats>k__BackingField");
                var time2 = cubeListProp?.GetArrayElementAtIndex(1)?.FindPropertyRelative("<TimeInBeats>k__BackingField");

                if (time1 != null && time2 != null && Mathf.Approximately(time1.floatValue, time2.floatValue))
                {
                    displayLabel += "Equal Time";
                }
            }
            
        }
        else
        {
            displayLabel = "(пусто)";
        }

        EditorGUI.PropertyField(position, property, new GUIContent(displayLabel), true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}