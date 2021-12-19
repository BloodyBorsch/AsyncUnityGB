using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


namespace LessonEight
{
    partial class CameraRender
    {
        private readonly CommandBuffer _commandBuffer = new CommandBuffer { name = bufferName };
        private const string bufferName = "Camera Render";

        private ScriptableRenderContext _context;
        private Camera _camera;
        private CullingResults _cullingResults;

        private static readonly List<ShaderTagId> drawingShaderTagIds =
            new List<ShaderTagId>
            {
                new ShaderTagId("SRPDefaultUnlit"),
            };

        public void Render(ScriptableRenderContext context, Camera camera)
        {
            _context = context;
            _camera = camera;

            if (!Cull(out var parameters))
            {
                return;
            }

            SettingsApply(parameters);
            DrawVisible();
            DrawUnsupportedShaders();
            DrawGizmos();
            Submit();
        }

        private DrawingSettings CreateDrawingSettings(List<ShaderTagId> shaderTags,
                                                      SortingCriteria sortingCriteria,
                                                      out SortingSettings sortingSettings)
        {
            sortingSettings = new SortingSettings(_camera)
            {
                criteria = sortingCriteria,
            };

            var drawingSettings = new DrawingSettings(shaderTags[0], sortingSettings);

            for (var i = 1; i < shaderTags.Count; i++)
            {
                drawingSettings.SetShaderPassName(i, shaderTags[i]);
            }

            return drawingSettings;
        }


        private void SettingsApply(ScriptableCullingParameters parameters)
        {
            _cullingResults = _context.Cull(ref parameters);
            _context.SetupCameraProperties(_camera);
            _commandBuffer.ClearRenderTarget(true, true, Color.clear);
            _commandBuffer.BeginSample(bufferName);
            ExecuteCommandBuffer();

        }

        private void ExecuteCommandBuffer()
        {
            _context.ExecuteCommandBuffer(_commandBuffer);
            _commandBuffer.Clear();
        }

        private void DrawVisible()
        {
            var drawingSettings = CreateDrawingSettings(drawingShaderTagIds, SortingCriteria.CommonOpaque, out var sortingSettings);
            var filteringSettings = new FilteringSettings(RenderQueueRange.all);
            _context.DrawRenderers(_cullingResults, ref drawingSettings, ref filteringSettings);
            _context.DrawSkybox(_camera);

            sortingSettings.criteria = SortingCriteria.CommonTransparent;
            drawingSettings.sortingSettings = sortingSettings;
            filteringSettings.renderQueueRange = RenderQueueRange.transparent;

            _context.DrawRenderers(_cullingResults, ref drawingSettings, ref filteringSettings);
        }

        private void Submit()
        {
            _commandBuffer.EndSample(bufferName);
            ExecuteCommandBuffer();
            _context.Submit();
        }

        private bool Cull(out ScriptableCullingParameters parameters)
        {
            return _camera.TryGetCullingParameters(out parameters);
        }

        partial void DrawGizmos();
    }
}