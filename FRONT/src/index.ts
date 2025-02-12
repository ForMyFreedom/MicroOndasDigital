import { errorLabel, exibitionTimeLabel, fastStartButton, heatButton, potencyInput, processLabel, stopButton, timeInput } from "./config.js"
import { DEFAULT_POTENCY, IHeatRunner } from "./domain/heatDomain.js"
import { HeatRunner } from "./services/heatService.js"

const setExibitionTime = (time: string) => {
    exibitionTimeLabel.innerText = time
}

const setErrorLabel = (message: string) => {
    errorLabel.innerText = message
}

const setProcessLabel = (process: string) => {
    processLabel.innerText = process
}

const cleanInputs = () => {
    timeInput.value = ''
    potencyInput.value = ''
}

const HeatService: IHeatRunner = new HeatRunner(
    setExibitionTime, setErrorLabel, setProcessLabel, cleanInputs
)

heatButton.addEventListener("click", () => {
    const time = Number(timeInput.value)
    const potency = Number(potencyInput.value || DEFAULT_POTENCY)
    potencyInput.value = String(potency)

    HeatService.clickToHeat(time, potency)
})

fastStartButton.addEventListener("click", () => {
    const response = HeatService.fastStart()
    if(!response.error){
        potencyInput.value = response.value.potency
        timeInput.value = response.value.time
    }
})

stopButton.addEventListener("click", () => {
    HeatService.clickToStop()
})