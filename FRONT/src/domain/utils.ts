export type Setter<T> = (_: T) => void

export type UncertainResponse<T> = Sucess<T> | Failure

export type Sucess<T> = {
    error: false
    value: T
}

export type Failure = {
    error: true
    message?: string
}