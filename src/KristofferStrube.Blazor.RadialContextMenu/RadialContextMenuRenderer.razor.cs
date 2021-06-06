using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KristofferStrube.Blazor.RadialContextMenu
{
    public partial class RadialContextMenuRenderer : ComponentBase, IAsyncDisposable
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public double InnerRadius { get; set; } = 20;

        [Parameter]
        public double OuterRadius { get; set; } = 80;

        [Parameter]
        public List<(Action OnClick, string Text)> Options { get; set; } = new();

        protected List<RadialSection> ContextMenuSections { get; set; } = new();

        public List<ElementReference> ElementSectionReferences => ContextMenuSections.Select(e => e.ElementReference).ToList();

        protected bool Opened;

        protected (double x, double y) Position { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            ContextMenuSections = Enumerable.Range(0, Options.Count).Select(i => new RadialSection() { OnClick = Options[i].OnClick, Text = Options[i].Text, InnerRadius = InnerRadius, OuterRadius = OuterRadius, Section = i, TotalSections = Options.Count }).ToList();
            await RadialContextMenuJSInterop.AddContextMenu(this);
        }

        public async Task Open()
        {
            Opened = true;
            StateHasChanged();
        }

        public async Task Close()
        {
            Opened = false;
            StateHasChanged();
        }

        public async Task OnContextClick(MouseEventArgs args)
        {
            Position = (args.ClientX, args.ClientY);
            await Open();
        }

        public async ValueTask DisposeAsync()
        {
            await RadialContextMenuJSInterop.RemoveContextMenu(this);
        }
    }
}
