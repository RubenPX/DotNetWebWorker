using System.Runtime.InteropServices.JavaScript;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Worker;

internal partial class MGEXWorker {
    private static WorkerInstance? _instance = null;
    private static event Action<string> onMsg = (_) => { };

    [JSExport]
    internal static void Init([JSMarshalAs<Function<JSType.String>>] Action<string> postMessage) {
        if (_instance != null) return;
        _instance = new WorkerInstance(postMessage);
        postMessage("instanced");
    }

    [JSExport]
    internal static void onMessage(string data) {
        if (_instance == null) return;
        _instance.onMessage(data);
    }
}
