using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtils.VisualElements {
    
    [UxmlElement]
    public partial class LineChart : VisualElement {
        
        private float _margin = 5f;
        public float margin {
            get { return _margin; }
            set {
                _margin = value;
                MarkDirtyRepaint();
            }
        }
        
        private float _lineThickness = 2f;
        public float lineThickness {
            get { return _lineThickness; }
            set {
                _lineThickness = value;
                MarkDirtyRepaint();
            }
        }
        
        private float2[] _data = Array.Empty<float2>();
        public float2[] data {
            get { return _data; }
            set {
                _data = value;
                MarkDirtyRepaint();
            }
        }
        
        public LineChart()
        {
            generateVisualContent += DrawCanvas;
        }

        void DrawCanvas(MeshGenerationContext ctx) {
            var painter = ctx.painter2D;
            painter.strokeColor = Color.white;
            painter.lineWidth = lineThickness;

            float2 size = ctx.visualElement.contentRect.size;
            float2 minPt = new float2(0, size.y) + new float2(margin, -margin);
            float2 maxPt = new float2(size.x, 0) + new float2(-margin, margin);

            for (int i = 1; i < data.Length; i++) {
                float2 leftPt = math.lerp(minPt, maxPt, data[i - 1]);
                float2 rightPt = math.lerp(minPt, maxPt, data[i]);
                
                painter.BeginPath();
                painter.MoveTo(leftPt);
                painter.LineTo(rightPt);
                painter.Stroke();
            }
        }
    }
}
