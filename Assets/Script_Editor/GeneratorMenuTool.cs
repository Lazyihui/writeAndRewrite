#if UNITY_EDITOR
using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using GameFunctions;
using GameFunctions.Editors;

public static class GeneratorMenuTool {
    [MenuItem("Tools/Generate Save Model")]

    public static void Generate() {

        string dir = Path.Combine(Application.dataPath, "Scripts", "SaveModel");
        GFEBufferEncoderGenerator.GenModel(dir);

    }

}

#endif