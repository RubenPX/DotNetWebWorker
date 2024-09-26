using Microsoft.JSInterop;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;

namespace Worker;

public partial class MGEXWorkerService {
    private static DotNetObjectReference<MGEXWorkerService> objRef;
    private static IJSObjectReference module;
    private static bool debugMode = true;

    public Action<string> onMessage = (_) => { };

    public MGEXWorkerService(IJSRuntime JS) {
        objRef = DotNetObjectReference.Create(this);
        loadApp(JS);
    }

    private async void loadApp(IJSRuntime JS) {
        module = await JS.InvokeAsync<IJSObjectReference>("import", "./_content/Worker/MGEXWorkerService.js");
        await module.InvokeVoidAsync("initializeWorker", objRef);
    }

    [JSExport]
    internal static async void onWorkerMessage(string data) {
        if (data.ToLower() == "instanced") postMessage("init");
        objRef.Value.onMessage(data);
    }

    public static async void postMessage(string data) {
        await module.InvokeVoidAsync("postMessage", data);
    }
}
