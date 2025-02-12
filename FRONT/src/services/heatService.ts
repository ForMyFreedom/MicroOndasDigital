import { DEFAULT_POTENCY, FAST_START_POTENCY, FAST_START_TIME, IHeatRunner, MAX_POTENCY, MAX_TIME, MIN_POTENCY, MIN_TIME, TIME_ADDITION } from "../domain/heatDomain.js"
import { Setter, UncertainResponse } from "../domain/utils.js"
import { getExibitionTime } from "./time.js"

export class HeatRunner implements IHeatRunner {
    private isRunning: boolean = false
    private actualPotency: number = 0
    private remainingTime: number = 0
    private processText: string = ''

    constructor(
        private setExibitionTime: Setter<string>,
        private setErrorLabel: Setter<string>,
        private setProcessLabel: Setter<string>,
        private cleanInputs: () => void
    ){ }

    clickToHeat(time: number, potency: number = DEFAULT_POTENCY) {
        if (this.isRunning) {
            this.increaseTime()
        } else {
            if(this.remainingTime > 0) {
                this.resumeTheHeat()
            } else {
                this.startTheHeat(time, potency)
            }
        }
    }

    fastStart(): UncertainResponse<{ time: string, potency: string }> {
        if(this.isRunning) {
            return { error: true }
        }
        
        const time = FAST_START_TIME
        const potency = FAST_START_POTENCY
        this.startTheHeat(time, potency)

        return {
            error: false,
            value: { time: String(time), potency: String(potency) }
        }
    }

    clickToStop(): void {
        if (this.isRunning){
            this.pauseTheHeat()
        } else {
            this.forgetTheHeat()
        }
    }

    private pauseTheHeat() {
        this.isRunning = false
    }

    private forgetTheHeat() {
        this.setDefaultData()
        this.resetLabels()
        this.cleanInputs()
        this.setProcessLabel('')
    }

    private increaseTime() {
        this.remainingTime += TIME_ADDITION
        this.updateExibitionTime()
    }

    private resumeTheHeat() {
        this.isRunning = true

        this.heatLoop()
        const myInterval = setInterval(()=>{
            this.heatLoop(myInterval)
        }, 1000)
    }

    private startTheHeat(time: number, potency: number) {
        this.resetLabels()

        if (time < MIN_TIME || time > MAX_TIME) {
            this.setErrorLabel(`Tempo deve ficar entre ${MIN_TIME} e ${MAX_TIME}`)
            return
        }
    
        if (potency < MIN_POTENCY || potency > MAX_POTENCY) {
            this.setErrorLabel(`Potência deve ficar entre ${MIN_POTENCY} e ${MAX_POTENCY}`)
            return
        }
    
        this.isRunning = true
        this.remainingTime = time
        this.actualPotency = potency
        this.updateExibitionTime()

        this.heatLoop()
        const myInterval = setInterval(()=>{
            this.heatLoop(myInterval)
        }, 1000)
    }

    private heatLoop(theInverval?: NodeJS.Timeout){
        if (!this.isRunning) {
            clearInterval(theInverval)
            return
        }
        
        this.remainingTime -= 1
        this.processText += '.'.repeat(this.actualPotency)+' '

        if (this.remainingTime<=0) {
            this.processText += 'Aquecimento concluído'
            clearInterval(theInverval)
        }

        this.updateExibitionTime()
        this.setProcessLabel(this.processText)
    }

    private updateExibitionTime(){
        this.setExibitionTime(
            getExibitionTime(this.remainingTime)
        )
    }
    
    private setDefaultData(){
        this.isRunning = false
        this.actualPotency = 0
        this.remainingTime = 0
        this.processText = ''
    }

    private resetLabels(){
        this.setExibitionTime('')
        this.setErrorLabel('')
    }
}