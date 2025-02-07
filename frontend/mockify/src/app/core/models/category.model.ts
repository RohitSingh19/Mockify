export interface Category {
    category: string;
    properties: Property[];
    endPoint: string;
}

export interface Property {
    name: string;
    type: string;
}