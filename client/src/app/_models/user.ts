export interface User {
    username: string;
    token: string;
}

let data: number | string = "42"


data = 42


interface Car {
    color: string,
    model: string,
    topspeed?: number
}

const car1: Car = {
    color: "blue",
    model: "BMW",
}

const car2: Car = {
    color: "123",
    model: "Mercedes"
}


function run(x, y):void {
    x * y
}

const run1 = (x: number, y: number): void =>{
    x * y
}


