using Unity.PolySpatial.InputDevices;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PolySpatial.Samples
{
    public class GalleryInputManager : MonoBehaviour
    {
        [SerializeField] Transform m_InputAxisTransform;

        private void OnEnable()
        {
            // enable enhanced touch support to use active touches for properly pooling input phases
            EnhancedTouchSupport.Enable();
        }

        private void Update()
        {
            ReadOnlyArray<Touch> activeTouches = Touch.activeTouches;

            if (activeTouches.Count == 0)
            {
                return;
            }

            SpatialPointerState primaryTouchData = EnhancedSpatialPointerSupport.GetPointerState(activeTouches[0]);
            if (activeTouches[0].phase == TouchPhase.Began)
            {
                // allow balloons to be popped with a poke or indirect pinch
                if (primaryTouchData.Kind == SpatialPointerKind.IndirectPinch || primaryTouchData.Kind == SpatialPointerKind.Touch)
                {
                    GameObject balloonObject = primaryTouchData.targetObject;
                    if (balloonObject != null)
                    {
                        if (balloonObject.TryGetComponent(out BalloonBehavior balloon))
                        {
                            balloon.Pop();
                        }
                    }
                }

                // update input gizmo
                m_InputAxisTransform.SetPositionAndRotation(primaryTouchData.interactionPosition, primaryTouchData.inputDeviceRotation);
            }

            // visualize input gizmo while input is maintained
            if (activeTouches[0].phase == TouchPhase.Moved)
            {
                m_InputAxisTransform.SetPositionAndRotation(primaryTouchData.interactionPosition, primaryTouchData.inputDeviceRotation);
            }
        }
    }
}