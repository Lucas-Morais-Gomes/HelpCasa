export interface User {
    name: string;
    email: string;
    address: string;
    description: string;
    phone: string;
    rate: number
}

export interface Employee extends User {
    hour_range: {
        init_hour: string
        finish_hour: string
    }
    available_services: string[],
    experience: string
}

export interface Client extends User {
    hired_services: string[],
}