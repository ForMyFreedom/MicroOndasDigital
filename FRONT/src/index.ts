import { errorLabel, exibitionTimeLabel, fastStartButton, heatButton, heatProgramsDiv, potencyInput, processLabel, stopButton, timeInput } from "./config"
import { DEFAULT_POTENCY, HeatProgram, IHeatRunner } from "./domain/heatDomain"
import { HeatRunner } from "./services/heatService"
import { RequestAllHeatPrograms } from "./requests/heatProgramRequest"
import { registerHeatProgram } from "./visual/registerHeatProgram"

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
    timeInput.readOnly = false
    potencyInput.readOnly = false
}

const appendHeatProgram = (element: HTMLElement) => {
    heatProgramsDiv.append(element)
}

const heatProgramEvent = (prog: HeatProgram) => {
    const response = HeatService.startHeatProgram(prog)
    if(!response.error){
        potencyInput.value = response.value.potency
        timeInput.value = response.value.time
        timeInput.readOnly = true
        potencyInput.readOnly = true
    }
}

RequestAllHeatPrograms()
    .then(res => {
        res.response.forEach(prog=>registerHeatProgram(
            prog, appendHeatProgram, heatProgramEvent
        ))
    })

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