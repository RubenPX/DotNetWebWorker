import { dotnet } from '/_framework/dotnet.js'
import Console from "/_content/Worker/Logger.js"

const workerInit = async () => {
    const { setModuleImports, getAssemblyExports, getConfig } = await dotnet
        .withDiagnosticTracing(false)
        .create();

    const exports = await getAssemblyExports("Worker");

    Console.info("Worker Exports", exports.Worker);

    const test = (data) => { self.postMessage(data) }
    self.onmessage = (ev) => { console.log("message", ev) }

    exports.Worker.MGEXWorker.Test(test);
}

if (typeof WorkerGlobalScope !== 'undefined' || self instanceof WorkerGlobalScope) {
    workerInit();
} else {
    Console.error("Not in web worker :(");
}