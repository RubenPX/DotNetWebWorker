import { dotnet } from '/_framework/dotnet.js'
import Console from "/_content/Worker/Logger.js"

const workerInit = async () => {
    const { setModuleImports, getAssemblyExports, getConfig } = await dotnet
        .withDiagnosticTracing(false)
        .create();

    const worker = (await getAssemblyExports("Worker")).Worker;
    Console.info("Worker Exports", worker);

    self.onmessage = (ev) => {
        if (Console.reciveDebug) Console.success("Worker <", ev.data);
        worker.MGEXWorker.onMessage(ev.data)
    }

    worker.MGEXWorker.Init((data) => {
        if (Console.sendDebug) Console.success("Worker >", data);
        self.postMessage(data);
    });
}

if (typeof WorkerGlobalScope !== 'undefined' || self instanceof WorkerGlobalScope) {
    workerInit();
} else {
    Console.error("Not in web worker :(");
}