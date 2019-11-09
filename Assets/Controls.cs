﻿using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(Controls))]
public class MapGenerationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var myScript = (Controls)target;
        GUILayout.Label("Editor Controls", EditorStyles.boldLabel);
        if(GUILayout.Button("Generate")) myScript.Generate();
        if(GUILayout.Button("Reset")) myScript.Reset();
    }
}

public class Controls : MonoBehaviour
{
    [SerializeField] Transform spriteParent;
    [SerializeField] GameObject spritePrefab;
    [SerializeField] SpriteGeneration spriteGeneration;
    [SerializeField] int imageGridSize;
    [SerializeField] int animationFrameCount = 1;
    [SerializeField] GridLayoutGroup gridLayoutGroup;

    public void Generate()
    {
        Reset();
        SetUpGridLayoutGroup();
        
        for (var i = 0; i < imageGridSize * imageGridSize; i++)
        {
            var sprite = Instantiate(spritePrefab, spriteParent);
            sprite.GetComponent<FrameAnimation>().Frames = spriteGeneration.Generate(animationFrameCount);
        }

        void SetUpGridLayoutGroup()
        {
            var maxSize = Screen.height / (float) imageGridSize;
            gridLayoutGroup.cellSize = new Vector2(maxSize, maxSize);
            gridLayoutGroup.constraintCount = imageGridSize;
        }
    }

    public void Reset()
    {
        for (var i = spriteParent.childCount - 1; i > -1; i--) 
            DestroyImmediate(spriteParent.GetChild(i).gameObject);
    }
}
