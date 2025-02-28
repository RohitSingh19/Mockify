export interface Category {
    category: string;
    properties: Property[];
    endpointToGetMockData: string;
    icon: string;
}

export interface Property {
    name: string;
    type: string;
    description: string;
    label: string;
    value: string;
    isVisible: boolean;
}

export interface JsonEditorModel {
    name: string;
    value: any;
}

export interface CustomMockDataRequest {
    items: CustomCategoryRequestItem[];
}

export interface CustomCategoryRequestItem {
    fieldName: string;
    customValue: string | null;
}