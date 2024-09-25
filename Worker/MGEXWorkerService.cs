using Microsoft.JSInterop;
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
        module = await JS.InvokeAsync<IJSObjectReference>("import", "./_content/Worker/WorkerInitializer.js");
        await module.InvokeVoidAsync("initializeWorker", objRef);
        
    }

    [JSExport]
    internal static async void onWorkerMessage(string data) {
        if (debugMode) await module.InvokeVoidAsync("workerMessage", data ?? "");
        objRef.Value.onMessage(data);
    }
}
