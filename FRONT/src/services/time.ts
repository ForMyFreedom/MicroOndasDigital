
export function getExibitionTime(time: number): string {
    if (time < 60) {
        let seconds = String(time)
        seconds = seconds.length == 1 ? '0' + seconds : seconds
        return `00:${seconds}`
    } else {
        const minutes = Math.floor(time/60)
        let seconds = String(time-minutes*60)
        seconds = seconds.length == 1 ? '0' + seconds : seconds
        return `${minutes}:${seconds}`
    }
}