using System.Net.Security;
using System.Collections;
using System.Collections.Generic;
using Mediapipe;
using Mediapipe.Unity;
using Mediapipe.Unity.CoordinateSystem;
using UnityEngine;

namespace Hedi.me.BoxWang
{
    public class PoseTrackingSolution : ImageSourceSolution<PoseTrackingGraph>
    {
        [SerializeField] private RectTransform _worldAnnotationArea;
        // [SerializeField] private DetectionAnnotationController _poseDetectionAnnotationController;
        // [SerializeField] private PoseLandmarkListAnnotationController _poseLandmarksAnnotationController;
        [SerializeField] private PoseWorldLandmarkListAnnotationController _poseWorldLandmarksAnnotationController;
        // [SerializeField] private MaskAnnotationController _segmentationMaskAnnotationController;
        // [SerializeField] private NormalizedRectAnnotationController _roiFromLandmarksAnnotationController;
        [SerializeField] private BodyManager bodyManager;

        [SerializeField] private bool activatePoseWorldLandmarks;

        public PoseTrackingGraph.ModelComplexity modelComplexity
        {
            get => graphRunner.modelComplexity;
            set => graphRunner.modelComplexity = value;
        }

        public bool smoothLandmarks
        {
            get => graphRunner.smoothLandmarks;
            set => graphRunner.smoothLandmarks = value;
        }

        public bool enableSegmentation
        {
            get => graphRunner.enableSegmentation;
            set => graphRunner.enableSegmentation = value;
        }

        public bool smoothSegmentation
        {
            get => graphRunner.smoothSegmentation;
            set => graphRunner.smoothSegmentation = value;
        }

        public float minDetectionConfidence
        {
            get => graphRunner.minDetectionConfidence;
            set => graphRunner.minDetectionConfidence = value;
        }

        public float minTrackingConfidence
        {
            get => graphRunner.minTrackingConfidence;
            set => graphRunner.minTrackingConfidence = value;
        }

        protected override void SetupScreen(ImageSource imageSource)
        {
            base.SetupScreen(imageSource);
            _worldAnnotationArea.localEulerAngles = imageSource.rotation.Reverse().GetEulerAngles();
        }

        protected override void OnStartRun()
        {
            if (!runningMode.IsSynchronous())
            {
                // graphRunner.OnPoseDetectionOutput += OnPoseDetectionOutput;
                // graphRunner.OnPoseLandmarksOutput += OnPoseLandmarksOutput;
                graphRunner.OnPoseWorldLandmarksOutput += OnPoseWorldLandmarksOutput;
                // graphRunner.OnSegmentationMaskOutput += OnSegmentationMaskOutput;
                // graphRunner.OnRoiFromLandmarksOutput += OnRoiFromLandmarksOutput;
                graphRunner.OnPoseWorldLandmarksOutput += OnPoseWorldLandmarksOutputDrawer;
            }

            var imageSource = ImageSourceProvider.ImageSource;
            (bool isMirrored, RotationAngle rotationAngle) = GetRotationMirroring(imageSource);
            bodyManager.OnUpdateRotationAngle(rotationAngle, isMirrored);
            // SetupAnnotationController(_poseDetectionAnnotationController, imageSource);
            // SetupAnnotationController(_poseLandmarksAnnotationController, imageSource);
            SetupAnnotationController(_poseWorldLandmarksAnnotationController, imageSource);
            // SetupAnnotationController(_segmentationMaskAnnotationController, imageSource);
            // _segmentationMaskAnnotationController.InitScreen(imageSource.textureWidth, imageSource.textureHeight);
            // SetupAnnotationController(_roiFromLandmarksAnnotationController, imageSource);
        }

        protected override void AddTextureFrameToInputStream(TextureFrame textureFrame)
        {
            graphRunner.AddTextureFrameToInputStream(textureFrame);
        }

        protected override IEnumerator WaitForNextValue()
        {
            Detection poseDetection = null;
            NormalizedLandmarkList poseLandmarks = null;
            LandmarkList poseWorldLandmarks = null;
            ImageFrame segmentationMask = null;
            NormalizedRect roiFromLandmarks = null;

            if (runningMode == RunningMode.Sync)
            {
                var _ = graphRunner.TryGetNext(out poseDetection, out poseLandmarks, out poseWorldLandmarks, out segmentationMask, out roiFromLandmarks, true);
            }
            else if (runningMode == RunningMode.NonBlockingSync)
            {
                yield return new WaitUntil(() => graphRunner.TryGetNext(out poseDetection, out poseLandmarks, out poseWorldLandmarks, out segmentationMask, out roiFromLandmarks, false));
            }

            // _poseDetectionAnnotationController.DrawNow(poseDetection);
            // _poseLandmarksAnnotationController.DrawNow(poseLandmarks);
            // _poseWorldLandmarksAnnotationController.DrawNow(poseWorldLandmarks);
            // _segmentationMaskAnnotationController.DrawNow(segmentationMask);
            // _roiFromLandmarksAnnotationController.DrawNow(roiFromLandmarks);
            bodyManager.DrawNow(poseWorldLandmarks?.Landmark);

        }

        // private void OnPoseDetectionOutput(object stream, OutputEventArgs<Detection> eventArgs)
        // {
        //   _poseDetectionAnnotationController.DrawLater(eventArgs.value);
        // }

        // private void OnPoseLandmarksOutput(object stream, OutputEventArgs<NormalizedLandmarkList> eventArgs)
        // {
        //   _poseLandmarksAnnotationController.DrawLater(eventArgs.value);
        // }

        private void OnPoseWorldLandmarksOutput(object stream, OutputEventArgs<LandmarkList> eventArgs)
        {
            if (activatePoseWorldLandmarks)
                _poseWorldLandmarksAnnotationController.DrawLater(eventArgs.value);
        }

        // private void OnSegmentationMaskOutput(object stream, OutputEventArgs<ImageFrame> eventArgs)
        // {
        //   _segmentationMaskAnnotationController.DrawLater(eventArgs.value);
        // }

        // private void OnRoiFromLandmarksOutput(object stream, OutputEventArgs<NormalizedRect> eventArgs)
        // {
        //   _roiFromLandmarksAnnotationController.DrawLater(eventArgs.value);
        // }
        private void OnPoseWorldLandmarksOutputDrawer(object stream, OutputEventArgs<LandmarkList> eventArgs)
        {
            bodyManager.OnUpdateLandmarks(eventArgs.value?.Landmark);
            // landmarks = eventArgs.value?.Landmark;
            // isStale = true;
        }

    }
}
