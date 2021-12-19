using UnityEngine.Rendering;
using UnityEngine;


namespace LessonEight
{
    [CreateAssetMenu(fileName = nameof(MyPipelineRendererAsset), menuName = "Rendering/MyPipelineRenderAsset")]
    public class MyPipelineRendererAsset : RenderPipelineAsset
    {
        protected override RenderPipeline CreatePipeline()
        {
            var renderer = new MyPipelineRenderer();
            renderer.GetCameraRenderer();
            return renderer;
        }
    }
}
