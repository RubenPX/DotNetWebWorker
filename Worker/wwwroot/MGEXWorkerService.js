import Console from "/_content/Worker/Logger.js"

var worker;

export async function initializeWorker() {
    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
    const { MGEXWorkerService } = (await getAssemblyExports("Worker.dll")).Worker;

    worker = new Worker("/_content/Worker/MGEXWorker.js", { type: "module" })

    worker.onmessage = (ev) => {
        if (Console.reciveDebug) Console.success("Service <", ev.data);
        MGEXWorkerService.onWorkerMessage(ev.data)
    }

    worker.onerror = (ev) => {
        Console.error("Worker Service | Error", ev, ev.message)
    }
}

export function postMessage(data) {
    if (Console.sendDebug) Console.success("Service >", data);
    worker.postMessage(data);
}

