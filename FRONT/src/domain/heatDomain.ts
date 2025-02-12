export const MIN_TIME = 1
export const MAX_TIME = 120

export const MIN_POTENCY = 1
export const MAX_POTENCY = 10
export const DEFAULT_POTENCY = 10

/*
export type SucessHeatResponse = {
    status: 'sucess',
}

export type ErrorHeatResponse = {
    status: 'error',
    message: string,
}

export type HeatResponse = SucessHeatResponse | ErrorHeatResponse
*/

export interface IHeatRunner {
    startTheHeat(time: number, potency: number): boolean // HeatResponse
}
