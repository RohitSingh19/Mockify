export interface Category {
    category: string;
    properties: Property[];
    endPoint: string;
    icon: string;
}

export interface Property {
    name: string;
    type: string;
    description: string;
    label: string;
}