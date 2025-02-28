import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { Category, CustomMockDataRequest } from "../models/category.model";
import { HttpClient } from "@angular/common/http";

@Injectable({  
    providedIn: 'root'
 })

 export class MockDataService {
    
    defaultLimit = 100;
    constructor(private httpClient: HttpClient) { }

    private mockJsonData = new BehaviorSubject<any>({});
    getmockJsonData$ = this.mockJsonData.asObservable();

    setData(updatedMockData: any) {
        this.mockJsonData.next(updatedMockData);
    }

    getCategories(): Observable<Category[]> {
        return this.httpClient.get<Category[]>(`${environment.apiUrl}categories`);
    }

    getCustomCategory(): Observable<Category> {
        return this.httpClient.get<Category>(`${environment.apiUrl}customMockFields`);
    }

    getMockDataForSelectedCategory(categoryAPIEndpoint: string): Observable<any> | any {        
        return this.httpClient.get<any>(`${environment.apiUrl}${categoryAPIEndpoint}/${this.defaultLimit}`);
    }

    generateMockDataForCustomJson(customMockDataRequest: CustomMockDataRequest): Observable<any> {
        return this.httpClient.post<CustomMockDataRequest>(`${environment.apiUrl}custom/${this.defaultLimit}`, customMockDataRequest);
    }
 }