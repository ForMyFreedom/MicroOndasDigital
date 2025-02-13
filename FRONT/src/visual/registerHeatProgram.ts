import { HeatProgram } from "../domain/heatDomain"

export function registerHeatProgram(
    prog: HeatProgram,
    appendHeatProgram: (element: HTMLElement) => void,
    heatProgramEvent: (prog: HeatProgram) => void
){
    const input = document.createElement('input')
    input.value = prog.name
    input.type = 'button'
    input.id = `progBtn-${prog.name}`
    input.title = prog.instructions

    if (!prog.isStandart) {
        input.classList.add('italic')
    }

    input.addEventListener("click", () => heatProgramEvent(prog))
    appendHeatProgram(input)
}