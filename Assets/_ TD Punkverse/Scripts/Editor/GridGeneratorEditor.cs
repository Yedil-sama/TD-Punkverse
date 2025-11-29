using TD_Punkverse.Game.Grid;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridGenerator))]
public sealed class GridGeneratorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		GridGenerator generator = (GridGenerator)target;

		if (GUILayout.Button("Generate Grid"))
		{
			generator.Generate();
		}

		if (GUILayout.Button("Clear Grid"))
		{
			generator.ClearEditor();
		}
	}
}
