export async function initializeWorker() {
    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
    const { MGEXWorkerService } = (await getAssemblyExports("Worker.dll")).Worker;

    const worker = new Worker("/_content/Worker/Worker.js", { type: "module" })

    worker.onmessage = (ev) => {
        MGEXWorkerService.onWorkerMessage(ev.data);
    };
}

const padding = 'padding-left: 5px; padding-right: 5px; padding-top: 2px;';
const badgeInfo = 'background-color: #38f; border-radius: 100px; color: #000;';
const badgeSucc = 'background-color: #492; border-radius: 100px; color: #000;';
const badgeYellow = 'background-color: #fa0; border-radius: 100px; color: #000;';
const badgeErro = 'background-color: #f33; border-radius: 100px; color: #000;';
const badgeWarn = 'background-color: #f53; border-radius: 100px; color: #000;';

export function workerMessage(data) {
    successLog("DotNet Worker ", data);
}

export function successLog(context, data) {
    console.log(`%c${context}`, `${padding} ${badgeSucc}`, data);
}

export function infoLog(context, data) {
    console.log(`%c${context}`, `${padding} ${badgeInfo}`, data);
}