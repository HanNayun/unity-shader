/// <summary>
/// Author: Lele Feng 
/// </summary>

using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ColorPicker : MonoBehaviour
{
    private static Texture2D s_rectTexture;
    private static GUIStyle s_rectStyle;
    private static Vector3 s_pixelPosition = Vector3.zero;

    public BoxCollider pickerCollider;

    private bool _grab;
    private Camera _camera;
    private Texture2D _screenRenderTexture;
    private Color _pickedColor = Color.white;

    private void Awake()
    {
        // Get the Camera component
        _camera = GetComponent<Camera>();
        if (_camera == null)
        {
            Debug.LogError("You need to dray this script to a camera!");
            return;
        }

        // Attach a BoxCollider to this camera
        // In order to receive mouse events
        if (pickerCollider == null)
        {
            pickerCollider = gameObject.AddComponent<BoxCollider>();
            // Make sure the collider is in the camera's frustum
            pickerCollider.center = Vector3.zero;
            pickerCollider.center += _camera.transform.worldToLocalMatrix.MultiplyVector(_camera.transform.forward) *
                (_camera.nearClipPlane + 0.2f);
            pickerCollider.size = new Vector3(Screen.width, Screen.height, 0.1f);
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 120, 200), "Color Picker");
        GUIDrawRect(new Rect(20, 30, 80, 80), _pickedColor);
        GUI.Label(new Rect(10, 120, 100, 20),
            "R: " + Math.Round((double)_pickedColor.r, 4) + "\t(" + Mathf.FloorToInt(_pickedColor.r * 255) + ")");
        GUI.Label(new Rect(10, 140, 100, 20),
            "G: " + Math.Round((double)_pickedColor.g, 4) + "\t(" + Mathf.FloorToInt(_pickedColor.g * 255) + ")");
        GUI.Label(new Rect(10, 160, 100, 20),
            "B: " + Math.Round((double)_pickedColor.b, 4) + "\t(" + Mathf.FloorToInt(_pickedColor.b * 255) + ")");
        GUI.Label(new Rect(10, 180, 100, 20),
            "A: " + Math.Round((double)_pickedColor.a, 4) + "\t(" + Mathf.FloorToInt(_pickedColor.a * 255) + ")");
    }

    private void OnMouseDown()
    {
        _grab = true;
        // Record the mouse position to pick pixel
        s_pixelPosition = Input.mousePosition;
    }

    // OnPostRender is called after a camera has finished rendering the scene.
    // This message is sent to all scripts attached to the camera.
    // Use it to grab the screen
    // Note: grabing is a expensive operation
    private void OnPostRender()
    {
        if (_grab)
        {
            _screenRenderTexture = new Texture2D(Screen.width, Screen.height);
            _screenRenderTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            _screenRenderTexture.Apply();
            _pickedColor =
                _screenRenderTexture.GetPixel(Mathf.FloorToInt(s_pixelPosition.x), Mathf.FloorToInt(s_pixelPosition.y));
            _grab = false;
        }
    }

    // Draw the color we picked
    public static void GUIDrawRect(Rect position, Color color)
    {
        s_rectTexture ??= new Texture2D(1, 1);
        s_rectStyle ??= new GUIStyle();

        s_rectTexture.SetPixel(0, 0, color);
        s_rectTexture.Apply();

        s_rectStyle.normal.background = s_rectTexture;

        GUI.Box(position, GUIContent.none, s_rectStyle);
    }
}