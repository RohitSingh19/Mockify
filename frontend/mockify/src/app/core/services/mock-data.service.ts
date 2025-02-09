import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable({  
    providedIn: 'root'
 })

 export class MockDataService {
    
    constructor() { }

    private mockJsonData = new BehaviorSubject<any>({});
    getmockJsonData$ = this.mockJsonData.asObservable();

    setData(updatedMockData: any) {
        this.mockJsonData.next(updatedMockData);
    }

 }