using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityUtils.VisualElements
{
    [UxmlElement]
    public partial class PieChart : VisualElement
    {
        float m_Value = 40.0f;

        public float value {
            get { return m_Value; }
            set { m_Value = value; MarkDirtyRepaint(); }
        }

        public PieChart()
        {
            generateVisualContent += DrawCanvas;
        }

        void DrawCanvas(MeshGenerationContext ctx)
        {
            var painter = ctx.painter2D;
            painter.strokeColor = Color.white;
            painter.fillColor = Color.white;

            float2 size = ctx.visualElement.contentRect.size;
            float diameter = math.min(size.x, size.y);
            float radius = diameter / 2;

            var percentage = m_Value;

            var percentages = new float[] {
                percentage, 100 - percentage
            };
            var colors = new Color32[] {
                new Color32(182,235,122,255),
                new Color32(251,120,19,255)
            };
            float angle = 0.0f;
            float anglePct = 0.0f;
            int k = 0;
            foreach (var pct in percentages)
            {
                anglePct += 360.0f * (pct / 100);

                painter.fillColor = colors[k++];
                painter.BeginPath();
                painter.MoveTo(new Vector2(radius, radius));
                painter.Arc(new Vector2(radius, radius), radius, angle, anglePct);
                painter.Fill();

                angle = anglePct;
            }
        }
    }
}
