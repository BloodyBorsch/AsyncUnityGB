using UnityEngine;
using UnityEngine.Rendering;


namespace LessonEight
{
    public class MyPipelineRenderer : RenderPipeline
    {
        private CameraRender _cameraRenderer;

        public void GetCameraRenderer()
        {
            _cameraRenderer = new CameraRender();
        }

        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {            
            CamerasRenderer(context, cameras);
        }

        private void CamerasRenderer(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach(var camera in cameras)
            {
                _cameraRenderer.Render(context, camera);
            }
        }
    }
}
