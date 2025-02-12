export const MIN_TIME = 1
export const MAX_TIME = 120

export const MIN_POTENCY = 1
export const MAX_POTENCY = 10
export const DEFAULT_POTENCY = 10

export const FAST_START_TIME = 30
export const FAST_START_POTENCY = 10

export const TIME_ADDITION = 30

export interface IHeatRunner {
    clickToHeat(time: number, potency: number): boolean
    fastStart(): { time: string, potency: string }
    clickToStop(): void
}
