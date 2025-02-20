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
    value: string;
    isVisible: boolean;
}

export interface JsonEditorModel {
    name: string;
    value: any;
    label: string;
}

export interface CustomMockDataRequest {
    items: CustomCategoryRequestItem[];
}

export interface CustomCategoryRequestItem {
    filedName: string;
    customValue: string | null;
}