import { dotnet } from '/_framework/dotnet.js'
import { infoLog } from "/_content/Worker/WorkerInitializer.js"

const workerInit = async () => {
    const { setModuleImports, getAssemblyExports, getConfig } = await dotnet
        .withDiagnosticTracing(false)
        .create();

    const exports = await getAssemblyExports("Worker");

    infoLog("Worker Exports", exports.Worker);

    const test = (data) => { self.postMessage(data) }
    self.onmessage = (ev) => { console.log("message", ev) }

    exports.Worker.MGEXWorker.Test(test);
}

if (typeof WorkerGlobalScope !== 'undefined' || self instanceof WorkerGlobalScope) {
    workerInit();
} else {
    console.log("Not in web worker :(");
}