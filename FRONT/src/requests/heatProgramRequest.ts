import { HeatProgram } from "../domain/heatDomain"

const END_POINT = process.env.API_ENDPOINT

export async function RequestAllHeatPrograms(): Promise<HeatProgram[]>{
    return fetch(END_POINT+'heat-program', {method:'GET'})
        .then(res=>{ return res.json() })
}
