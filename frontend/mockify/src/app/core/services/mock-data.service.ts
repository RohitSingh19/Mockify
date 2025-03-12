import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { Category, CustomMockDataRequest } from "../models/category.model";
import { HttpClient } from "@angular/common/http";
import { ApiResponse } from "../models/api-response.model";



@Injectable({  
    providedIn: 'root'
 })

 export class MockDataService {
    
    defaultLimit = 100;
    constructor(private httpClient: HttpClient) {
     }

    private mockJsonData = new BehaviorSubject<any>({});
    getmockJsonData$ = this.mockJsonData.asObservable();

    setData(updatedMockData: any) {
        this.mockJsonData.next(updatedMockData);
    }

    getCategories(): Observable<ApiResponse<Category[]>> {
        return this.httpClient.get<ApiResponse<Category[]>>(`${environment.apiUrl}categories`);
    }

    getCustomCategory(): Observable<ApiResponse<Category>> {
        return this.httpClient.get<ApiResponse<Category>>(`${environment.apiUrl}customMockFields`);
    }

    getMockDataForSelectedCategory(categoryAPIEndpoint: string): Observable<ApiResponse<any>> | any {        
        return this.httpClient.get<ApiResponse<any>>(`${environment.apiUrl}${categoryAPIEndpoint}/${this.defaultLimit}`);
    }

    generateMockDataForCustomJson(customMockDataRequest: CustomMockDataRequest): Observable<ApiResponse<any>> {
        return this.httpClient.post<ApiResponse<CustomMockDataRequest>>(`${environment.apiUrl}custom/${this.defaultLimit}`, customMockDataRequest);
    }

   
 }