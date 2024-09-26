const padding = 'padding-left: 5px; padding-right: 5px; padding-top: 2px;';

const badgeInfo = 'background-color: #38f; border-radius: 100px; color: #000;';
const badgeWarn = 'background-color: #f53; border-radius: 100px; color: #000;';
const badgeSucc = 'background-color: #492; border-radius: 100px; color: #000;';
const badgeErro = 'background-color: #f33; border-radius: 100px; color: #000;';
const badgeYellow = 'background-color: #fa0; border-radius: 100px; color: #000;';

const debug = true;


export default class Console {
    static sendDebug = debug;
    static reciveDebug = debug && false;

    static info(context, data) {
        if (context instanceof Array) return console.log('%c' + context.join(' | '), `${padding} ${badgeInfo}`, data)
        console.log('%c' + context, `${padding} ${badgeInfo}`, data)
    }

    static warn(context, data) {
        if (context instanceof Array) return console.log('%c' + context.join(' | '), `${padding} ${badgeWarn}`, data)
        console.log('%c' + context, `${padding} ${badgeWarn}`, data)
    }

    static error(context, data) {
        if (context instanceof Array) return console.log('%c' + context.join(' | '), `${padding} ${badgeErro}`, data)
        console.log('%c' + context, `${padding} ${badgeErro}`, data)
    }

    static success(context, data) {
        if (context instanceof Array) return console.log('%c' + context.join(' | '), `${padding} ${badgeSucc}`, data)
        console.log('%c' + context, `${padding} ${badgeSucc}`, data)
    }

    static yellow(context, data) {
        if (context instanceof Array) return console.log('%c' + context.join(' | '), `${padding} ${badgeYellow}`, data)
        console.log('%c' + context, `${padding} ${badgeYellow}`, data)
    }
}