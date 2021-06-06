using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.RadialContextMenu
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class RadialContextMenuJSInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> ModuleTask;

        private IJSObjectReference JSObjectReference { get; set; }

        private DotNetObjectReference<RadialContextMenuJSInterop> ObjRef { get; init; }

        private List<RadialContextMenuRenderer> ContextMenues { get; set; } = new();

        public RadialContextMenuJSInterop(IJSRuntime jsRuntime)
        {
            ModuleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/KristofferStrube.Blazor.RadialContextMenu/KristofferStrube.Blazor.RadialContextMenu.js").AsTask());
            ObjRef = DotNetObjectReference.Create(this);
        }

        protected async Task Init(RadialContextMenuRenderer radialContextMenuRenderer)
        {
            JSObjectReference = await ModuleTask.Value;
            await JSObjectReference.InvokeVoidAsync("Init", ObjRef);
        }

        public async Task AddContextMenu(RadialContextMenuRenderer radialContextMenuRenderer)
        {
            if (JSObjectReference is null)
                await Init(radialContextMenuRenderer);
            await JSObjectReference.InvokeVoidAsync("AddMenuElements", radialContextMenuRenderer.ElementSectionReferences);
            ContextMenues.Add(radialContextMenuRenderer);
        }

        public async Task RemoveContextMenu(RadialContextMenuRenderer radialContextMenuRenderer)
        {
            await JSObjectReference.InvokeVoidAsync("RemoveMenuElements", radialContextMenuRenderer.ElementSectionReferences);
            ContextMenues.Remove(radialContextMenuRenderer);
        }

        [JSInvokable("Close")]
        public void Close()
        {
            Task.WaitAll(ContextMenues.Select(e => e.Close()).ToArray());
        }


        public async ValueTask DisposeAsync()
        {
            if (ModuleTask.IsValueCreated)
            {
                var module = await ModuleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
