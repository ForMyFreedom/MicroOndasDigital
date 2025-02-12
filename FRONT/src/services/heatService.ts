import { IHeatRunner, MAX_POTENCY, MAX_TIME, MIN_POTENCY, MIN_TIME } from "../domain/heatDomain.js"
import { Setter } from "../domain/utils.js"
import { getExibitionTime } from "./time.js"

export class HeatRunner implements IHeatRunner {
    constructor(
        private setExibitionTime: Setter<string>,
        private setErrorLabel: Setter<string>
    ){}

    private resetLabels(){
        this.setExibitionTime('')
        this.setErrorLabel('')
    }

    startTheHeat(time: number, potency: number = 10): boolean {
        this.resetLabels()

        if (time < MIN_TIME || time > MAX_TIME) {
            this.setErrorLabel(`Tempo deve ficar entre ${MIN_TIME} e ${MAX_TIME}`)
            return false
        }
    
        if (potency < MIN_POTENCY || potency > MAX_POTENCY) {
            this.setErrorLabel(`Potência deve ficar entre ${MIN_POTENCY} e ${MAX_POTENCY}`)
            return false
        }
    
        const exibitionTime = getExibitionTime(time)
        this.setExibitionTime(exibitionTime)
    
        return true
    }
}