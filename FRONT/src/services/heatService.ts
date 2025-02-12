import { DEFAULT_POTENCY, FAST_START_POTENCY, FAST_START_TIME, IHeatRunner, MAX_POTENCY, MAX_TIME, MIN_POTENCY, MIN_TIME, TIME_ADDITION } from "../domain/heatDomain.js"
import { Setter } from "../domain/utils.js"
import { getExibitionTime } from "./time.js"

export class HeatRunner implements IHeatRunner {
    private isRunning: boolean = false
    private remainingTime: number = 0

    constructor(
        private setExibitionTime: Setter<string>,
        private setErrorLabel: Setter<string>
    ){}

    private resetLabels(){
        this.setExibitionTime('')
        this.setErrorLabel('')
    }

    clickToHeat(time: number, potency: number = DEFAULT_POTENCY): boolean {
        if (this.isRunning) {
            return this.increaseTime()
        } else {
            return this.startTheHeat(time, potency)
        }
    }

    private increaseTime(): boolean{
        this.remainingTime += TIME_ADDITION
        this.updateExibitionTime()
        return true
    }

    private startTheHeat(time: number, potency: number): boolean{
        this.resetLabels()

        if (time < MIN_TIME || time > MAX_TIME) {
            this.setErrorLabel(`Tempo deve ficar entre ${MIN_TIME} e ${MAX_TIME}`)
            return false
        }
    
        if (potency < MIN_POTENCY || potency > MAX_POTENCY) {
            this.setErrorLabel(`Potência deve ficar entre ${MIN_POTENCY} e ${MAX_POTENCY}`)
            return false
        }
    
        this.isRunning = true
        this.remainingTime = time
        this.updateExibitionTime()
        return true
    }

    fastStart(): { time: string, potency: string } {
        const time = FAST_START_TIME
        const potency = FAST_START_POTENCY
        this.startTheHeat(time, potency)
        return { time: String(time), potency: String(potency) }
    }

    private updateExibitionTime(){
        this.setExibitionTime(
            getExibitionTime(this.remainingTime)
        )
    }
}