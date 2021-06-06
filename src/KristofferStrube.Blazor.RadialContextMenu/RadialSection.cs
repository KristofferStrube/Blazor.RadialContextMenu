using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KristofferStrube.Blazor.RadialContextMenu
{
    public class RadialSection
    {
        public double InnerRadius { get; set; }

        public double OuterRadius { get; set; }

        public int Section { get; set; }

        public int TotalSections { get; set; }

        public Action OnClick { get; set; }

        public string Text { get; set; }

        public ElementReference ElementReference { get; set; }

        public string D => $"m {(Math.Sin(Math.PI * 2 * (Section + 1) / TotalSections) * InnerRadius + OuterRadius).AsString()} {(Math.Cos(Math.PI * 2 * (Section + 1) / TotalSections) * InnerRadius + OuterRadius).AsString()}" +
            $"A {InnerRadius} {InnerRadius} 0 0 1 {(Math.Sin(Math.PI * 2 * (Section) / TotalSections) * InnerRadius + OuterRadius).AsString()} {(Math.Cos(Math.PI * 2 * (Section) / TotalSections) * InnerRadius + OuterRadius).AsString()}" +
            $"l {(Math.Sin(Math.PI * 2 * Section / TotalSections) * (OuterRadius - InnerRadius)).AsString()} {(Math.Cos(Math.PI * 2 * Section / TotalSections) * (OuterRadius - InnerRadius)).AsString()}" +
            $"A {OuterRadius} {OuterRadius} 0 0 0 {(Math.Sin(Math.PI * 2 * (Section + 1) / TotalSections) * OuterRadius + OuterRadius).AsString()} {(Math.Cos(Math.PI * 2 * (Section + 1) / TotalSections) * OuterRadius + OuterRadius).AsString()}" +
            $"z";

        public (double x, double y) TextPosition => (Math.Sin(Math.PI * 2 * (Section + 0.5) / TotalSections) * (OuterRadius + InnerRadius) / 2 + OuterRadius, Math.Cos(Math.PI * 2 * (Section + 0.5) / TotalSections) * (OuterRadius + InnerRadius) / 2 + OuterRadius);
    }
}
