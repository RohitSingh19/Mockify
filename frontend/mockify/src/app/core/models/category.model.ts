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
    isRandomData: boolean;
    isVisible: boolean;
    CustomDataValue: string;
}

export interface CustomMockDataRequest {
    items: CustomCategoryRequestItem[];
}

export interface CustomCategoryRequestItem {
    filedName: string;
    customValue: string | null;
}