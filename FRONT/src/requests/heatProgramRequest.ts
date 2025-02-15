import { HeatProgram } from "../domain/heatDomain"

const END_POINT = process.env.API_ENDPOINT
const BearToken = fetch(END_POINT+'login', {
    method:'POST', headers: {
        'Content-Type': 'application/json'
    }, body:JSON.stringify({
        username: process.env.LOGIN,
        password: process.env.PASSWORD,
    })
}).then(res=>{ return res.json() })

export async function RequestAllHeatPrograms(): Promise<{response: HeatProgram[]}> {
    const token = await BearToken;
    return fetch(END_POINT + 'heat-program', {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token.response}`
        }
    }).then(res => res.json());
}
