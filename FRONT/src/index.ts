import { DEFAULT_POTENCY, IHeatRunner } from "./domain/heatDomain.js"
import { HeatRunner } from "./services/heatService.js"

const timeInput = document.getElementById("timeInput") as HTMLInputElement
const potencyInput = document.getElementById("potencyInput") as HTMLInputElement
const heatButton = document.getElementById("heatButton") as HTMLButtonElement
const fastStartButton = document.getElementById("fastStartButton") as HTMLButtonElement
const errorLabel = document.getElementById("errorLabel") as HTMLLabelElement
const exibitionTimeLabel = document.getElementById("exibitionTimeLabel") as HTMLLabelElement

const setExibitionTime = (time: string) => {
    exibitionTimeLabel.innerText = time
}

const setErrorLabel = (message: string) => {
    errorLabel.innerText = message
}

const HeatService: IHeatRunner = new HeatRunner(
    setExibitionTime, setErrorLabel
)

heatButton.addEventListener("click", () => {
    const time = Number(timeInput.value)
    const potency = Number(potencyInput.value || DEFAULT_POTENCY)
    potencyInput.value = String(potency)

    const result = HeatService.clickToHeat(time, potency)
})

fastStartButton.addEventListener("click", () => {
    const result = HeatService.fastStart()
    potencyInput.value = result.potency
    timeInput.value = result.time
})
