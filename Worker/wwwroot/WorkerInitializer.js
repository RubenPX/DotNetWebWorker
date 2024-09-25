import Console from "/_content/Worker/Logger.js"

export async function initializeWorker() {
    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
    const { MGEXWorkerService } = (await getAssemblyExports("Worker.dll")).Worker;

    const worker = new Worker("/_content/Worker/Worker.js", { type: "module" })

    worker.onmessage = (ev) => {
        MGEXWorkerService.onWorkerMessage(ev.data);
    };

    worker.onerror = (ev) => {
        Console.error("DotNet Worker | Error", ev, ev.message)
    }
}

export function workerMessage(data) {
    Console.success("DotNet Worker ", data);
}

