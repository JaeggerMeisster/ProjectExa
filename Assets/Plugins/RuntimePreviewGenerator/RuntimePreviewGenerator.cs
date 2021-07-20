//#define DEBUG_BOUNDS

using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class RuntimePreviewGenerator {
    // Source: https://github.com/MattRix/UnityDecompiled/blob/master/UnityEngine/UnityEngine/Plane.cs
    private struct ProjectionPlane {
        private readonly Vector3 m_Normal;
        private readonly float m_Distance;

        public ProjectionPlane(Vector3 inNormal, Vector3 inPoint) {
            m_Normal = Vector3.Normalize(inNormal);
            m_Distance = -Vector3.Dot(inNormal, inPoint);
        }

        public Vector3 ClosestPointOnPlane(Vector3 point) {
            var d = Vector3.Dot(m_Normal, point) + m_Distance;

            return point - m_Normal * d;
        }

        public float GetDistanceToPoint(Vector3 point) {
            var signedDistance = Vector3.Dot(m_Normal, point) + m_Distance;

            if (signedDistance < 0f) {
                signedDistance = -signedDistance;
            }

            return signedDistance;
        }
    }

    private class CameraSetup {
        private float aspect;

        private Color backgroundColor;
        private CameraClearFlags clearFlags;
        private float farClipPlane;
        private float nearClipPlane;
        private bool orthographic;
        private float orthographicSize;
        private Vector3 position;
        private Quaternion rotation;

        private RenderTexture targetTexture;

        public void GetSetup(Camera camera) {
            position = camera.transform.position;
            rotation = camera.transform.rotation;

            targetTexture = camera.targetTexture;

            backgroundColor = camera.backgroundColor;
            orthographic = camera.orthographic;
            orthographicSize = camera.orthographicSize;
            nearClipPlane = camera.nearClipPlane;
            farClipPlane = camera.farClipPlane;
            aspect = camera.aspect;
            clearFlags = camera.clearFlags;
        }

        public void ApplySetup(Camera camera) {
            camera.transform.position = position;
            camera.transform.rotation = rotation;

            camera.targetTexture = targetTexture;

            camera.backgroundColor = backgroundColor;
            camera.orthographic = orthographic;
            camera.orthographicSize = orthographicSize;
            camera.nearClipPlane = nearClipPlane;
            camera.farClipPlane = farClipPlane;
            camera.aspect = aspect;
            camera.clearFlags = clearFlags;

            targetTexture = null;
        }
    }

    private const int PREVIEW_LAYER = 22;
    private static readonly Vector3 PREVIEW_POSITION = new Vector3(-9245f, 9899f, -9356f);

    private static Camera renderCamera;
    private static readonly CameraSetup cameraSetup = new CameraSetup();

    private static readonly List<Renderer> renderersList = new List<Renderer>();
    private static readonly List<int> layersList = new List<int>();

    private static float aspect;
    private static float minX, maxX, minY, maxY;
    private static float maxDistance;

    private static Vector3 boundsCenter;
    private static ProjectionPlane projectionPlaneHorizontal, projectionPlaneVertical;

#if DEBUG_BOUNDS
	private static List<Transform> boundsDebugCubes = new List<Transform>( 8 );
	private static Material boundsMaterial;
#endif

    private static Camera m_internalCamera;

    private static Camera InternalCamera {
        get {
            if (m_internalCamera == null) {
                m_internalCamera = new GameObject("ModelPreviewGeneratorCamera").AddComponent<Camera>();
                m_internalCamera.enabled = false;
                m_internalCamera.nearClipPlane = 0.01f;
                m_internalCamera.cullingMask = 1 << PREVIEW_LAYER;
                m_internalCamera.gameObject.hideFlags = HideFlags.HideAndDontSave;
            }

            return m_internalCamera;
        }
    }

    public static Camera PreviewRenderCamera { get; set; }

    private static Vector3 m_previewDirection;

    public static Vector3 PreviewDirection {
        get => m_previewDirection;
        set => m_previewDirection = value.normalized;
    }

    private static float m_padding;

    public static float Padding {
        get => m_padding;
        set => m_padding = Mathf.Clamp(value, -0.25f, 0.25f);
    }

    private static Color m_backgroundColor;

    public static Color BackgroundColor {
        get => m_backgroundColor;
        set => m_backgroundColor = value;
    }

    public static bool OrthographicMode { get; set; }

    public static bool MarkTextureNonReadable { get; set; }

    static RuntimePreviewGenerator() {
        PreviewRenderCamera = null;
        PreviewDirection = new Vector3(-1f, -1f, -1f);
        Padding = 0f;
        BackgroundColor = new Color(0.3f, 0.3f, 0.3f, 1f);
        OrthographicMode = false;
        MarkTextureNonReadable = true;

    #if DEBUG_BOUNDS
		boundsMaterial = new Material( Shader.Find( "Legacy Shaders/Diffuse" ) )
		{
			hideFlags = HideFlags.HideAndDontSave,
			color = new Color( 0f, 1f, 1f, 1f )
		};
    #endif
    }

    public static Texture2D GenerateMaterialPreview(Material material, PrimitiveType previewObject, int width = 64, int height = 64) {
        return GenerateMaterialPreviewWithShader(
            material,
            previewObject,
            null,
            null,
            width,
            height
        );
    }

    public static Texture2D GenerateMaterialPreviewWithShader(Material material, PrimitiveType previewPrimitive, Shader shader, string replacementTag, int width = 64, int height = 64) {
        var previewModel = GameObject.CreatePrimitive(previewPrimitive);
        previewModel.gameObject.hideFlags = HideFlags.HideAndDontSave;
        previewModel.GetComponent<Renderer>().sharedMaterial = material;

        try {
            return GenerateModelPreviewWithShader(
                previewModel.transform,
                shader,
                replacementTag,
                width,
                height
            );
        } catch (Exception e) {
            Debug.LogException(e);
        } finally {
            Object.DestroyImmediate(previewModel);
        }

        return null;
    }

    public static Texture2D GenerateModelPreview(Transform model, int width = 64, int height = 64, bool shouldCloneModel = false) {
        return GenerateModelPreviewWithShader(
            model,
            null,
            null,
            width,
            height,
            shouldCloneModel
        );
    }

    public static Texture2D GenerateModelPreviewWithShader(Transform model, Shader shader, string replacementTag, int width = 64, int height = 64, bool shouldCloneModel = false) {
        if (model == null || model.Equals(null)) {
            return null;
        }

        Texture2D result = null;

        if (!model.gameObject.scene.IsValid() || !model.gameObject.scene.isLoaded) {
            shouldCloneModel = true;
        }

        Transform previewObject;

        if (shouldCloneModel) {
            previewObject = Object.Instantiate(model, null, false);
            previewObject.gameObject.hideFlags = HideFlags.HideAndDontSave;
        } else {
            previewObject = model;

            layersList.Clear();
            GetLayerRecursively(previewObject);
        }

        var isStatic = IsStatic(model);
        var wasActive = previewObject.gameObject.activeSelf;
        var prevPos = previewObject.position;
        var prevRot = previewObject.rotation;

        try {
            SetupCamera();
            SetLayerRecursively(previewObject);

            if (!isStatic) {
                previewObject.position = PREVIEW_POSITION;
                previewObject.rotation = Quaternion.identity;
            }

            if (!wasActive) {
                previewObject.gameObject.SetActive(true);
            }

            var previewDir = previewObject.rotation * m_previewDirection;

            renderersList.Clear();
            previewObject.GetComponentsInChildren(renderersList);

            var previewBounds = new Bounds();
            var init = false;

            for (var i = 0; i < renderersList.Count; i++) {
                if (!renderersList[i].enabled) {
                    continue;
                }

                if (!init) {
                    previewBounds = renderersList[i].bounds;
                    init = true;
                } else {
                    previewBounds.Encapsulate(renderersList[i].bounds);
                }
            }

            if (!init) {
                throw new Exception("No enabled renderers attached to model");
            }

            boundsCenter = previewBounds.center;
            var boundsExtents = previewBounds.extents;
            var boundsSize = 2f * boundsExtents;

            aspect = (float) width / height;
            renderCamera.aspect = aspect;
            renderCamera.transform.rotation = Quaternion.LookRotation(previewDir, previewObject.up);

        #if DEBUG_BOUNDS
			boundsDebugCubes.Clear();
        #endif

            float distance;

            if (OrthographicMode) {
                renderCamera.transform.position = boundsCenter;

                minX = minY = Mathf.Infinity;
                maxX = maxY = Mathf.NegativeInfinity;

                var point = boundsCenter + boundsExtents;
                ProjectBoundingBoxMinMax(point);
                point.x -= boundsSize.x;
                ProjectBoundingBoxMinMax(point);
                point.y -= boundsSize.y;
                ProjectBoundingBoxMinMax(point);
                point.x += boundsSize.x;
                ProjectBoundingBoxMinMax(point);
                point.z -= boundsSize.z;
                ProjectBoundingBoxMinMax(point);
                point.x -= boundsSize.x;
                ProjectBoundingBoxMinMax(point);
                point.y += boundsSize.y;
                ProjectBoundingBoxMinMax(point);
                point.x += boundsSize.x;
                ProjectBoundingBoxMinMax(point);

                distance = boundsExtents.magnitude + 1f;
                renderCamera.orthographicSize = (1f + m_padding * 2f) * Mathf.Max(maxY - minY, (maxX - minX) / aspect) * 0.5f;
            } else {
                projectionPlaneHorizontal = new ProjectionPlane(renderCamera.transform.up, boundsCenter);
                projectionPlaneVertical = new ProjectionPlane(renderCamera.transform.right, boundsCenter);

                maxDistance = Mathf.NegativeInfinity;

                var point = boundsCenter + boundsExtents;
                CalculateMaxDistance(point);
                point.x -= boundsSize.x;
                CalculateMaxDistance(point);
                point.y -= boundsSize.y;
                CalculateMaxDistance(point);
                point.x += boundsSize.x;
                CalculateMaxDistance(point);
                point.z -= boundsSize.z;
                CalculateMaxDistance(point);
                point.x -= boundsSize.x;
                CalculateMaxDistance(point);
                point.y += boundsSize.y;
                CalculateMaxDistance(point);
                point.x += boundsSize.x;
                CalculateMaxDistance(point);

                distance = (1f + m_padding * 2f) * Mathf.Sqrt(maxDistance);
            }

            renderCamera.transform.position = boundsCenter - previewDir * distance;
            renderCamera.farClipPlane = distance * 4f;

            var temp = RenderTexture.active;
            var renderTex = RenderTexture.GetTemporary(width, height, 16);
            RenderTexture.active = renderTex;

            if (m_backgroundColor.a < 1f) {
                GL.Clear(false, true, m_backgroundColor);
            }

            renderCamera.targetTexture = renderTex;

            if (shader == null) {
                renderCamera.Render();
            } else {
                renderCamera.RenderWithShader(shader, replacementTag ?? string.Empty);
            }

            renderCamera.targetTexture = null;

            result = new Texture2D(width, height, m_backgroundColor.a < 1f ? TextureFormat.RGBA32 : TextureFormat.RGB24, false);
            result.ReadPixels(new Rect(0, 0, width, height), 0, 0, false);
            result.Apply(false, MarkTextureNonReadable);

            RenderTexture.active = temp;
            RenderTexture.ReleaseTemporary(renderTex);
        } catch (Exception e) {
            Debug.LogException(e);
        } finally {
        #if DEBUG_BOUNDS
			for( int i = 0; i < boundsDebugCubes.Count; i++ )
				Object.DestroyImmediate( boundsDebugCubes[i].gameObject );

			boundsDebugCubes.Clear();
        #endif

            if (shouldCloneModel) {
                Object.DestroyImmediate(previewObject.gameObject);
            } else {
                if (!wasActive) {
                    previewObject.gameObject.SetActive(false);
                }

                if (!isStatic) {
                    previewObject.position = prevPos;
                    previewObject.rotation = prevRot;
                }

                var index = 0;
                SetLayerRecursively(previewObject, ref index);
            }

            if (renderCamera == PreviewRenderCamera) {
                cameraSetup.ApplySetup(renderCamera);
            }
        }

        return result;
    }

    private static void SetupCamera() {
        if (PreviewRenderCamera != null && !PreviewRenderCamera.Equals(null)) {
            cameraSetup.GetSetup(PreviewRenderCamera);

            renderCamera = PreviewRenderCamera;
            renderCamera.nearClipPlane = 0.01f;
        } else {
            renderCamera = InternalCamera;
        }

        renderCamera.backgroundColor = m_backgroundColor;
        renderCamera.orthographic = OrthographicMode;
        renderCamera.clearFlags = m_backgroundColor.a < 1f ? CameraClearFlags.Depth : CameraClearFlags.Color;
    }

    private static void ProjectBoundingBoxMinMax(Vector3 point) {
    #if DEBUG_BOUNDS
		CreateDebugCube( point, Vector3.zero, new Vector3( 0.5f, 0.5f, 0.5f ) );
    #endif

        var localPoint = renderCamera.transform.InverseTransformPoint(point);

        if (localPoint.x < minX) {
            minX = localPoint.x;
        }

        if (localPoint.x > maxX) {
            maxX = localPoint.x;
        }

        if (localPoint.y < minY) {
            minY = localPoint.y;
        }

        if (localPoint.y > maxY) {
            maxY = localPoint.y;
        }
    }

    private static void CalculateMaxDistance(Vector3 point) {
    #if DEBUG_BOUNDS
		CreateDebugCube( point, Vector3.zero, new Vector3( 0.5f, 0.5f, 0.5f ) );
    #endif

        var intersectionPoint = projectionPlaneHorizontal.ClosestPointOnPlane(point);

        var horizontalDistance = projectionPlaneHorizontal.GetDistanceToPoint(point);
        var verticalDistance = projectionPlaneVertical.GetDistanceToPoint(point);

        // Credit: https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
        var halfFrustumHeight = Mathf.Max(verticalDistance, horizontalDistance / aspect);
        var distance = halfFrustumHeight / Mathf.Tan(renderCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);

        var distanceToCenter = (intersectionPoint - m_previewDirection * distance - boundsCenter).sqrMagnitude;

        if (distanceToCenter > maxDistance) {
            maxDistance = distanceToCenter;
        }
    }

    private static bool IsStatic(Transform obj) {
        if (obj.gameObject.isStatic) {
            return true;
        }

        for (var i = 0; i < obj.childCount; i++) {
            if (IsStatic(obj.GetChild(i))) {
                return true;
            }
        }

        return false;
    }

    private static void SetLayerRecursively(Transform obj) {
        obj.gameObject.layer = PREVIEW_LAYER;

        for (var i = 0; i < obj.childCount; i++) {
            SetLayerRecursively(obj.GetChild(i));
        }
    }

    private static void GetLayerRecursively(Transform obj) {
        layersList.Add(obj.gameObject.layer);

        for (var i = 0; i < obj.childCount; i++) {
            GetLayerRecursively(obj.GetChild(i));
        }
    }

    private static void SetLayerRecursively(Transform obj, ref int index) {
        obj.gameObject.layer = layersList[index++];

        for (var i = 0; i < obj.childCount; i++) {
            SetLayerRecursively(obj.GetChild(i), ref index);
        }
    }

#if DEBUG_BOUNDS
	private static void CreateDebugCube( Vector3 position, Vector3 rotation, Vector3 scale )
	{
		Transform cube = GameObject.CreatePrimitive( PrimitiveType.Cube ).transform;
		cube.localPosition = position;
		cube.localEulerAngles = rotation;
		cube.localScale = scale;
		cube.gameObject.layer = PREVIEW_LAYER;
		cube.gameObject.hideFlags = HideFlags.HideAndDontSave;

		cube.GetComponent<Renderer>().sharedMaterial = boundsMaterial;

		boundsDebugCubes.Add( cube );
	}
#endif
}