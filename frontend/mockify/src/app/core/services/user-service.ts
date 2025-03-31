import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({providedIn: 'root'})
export class UserSerive {
    
    constructor(private httpClient: HttpClient) {
    
    }

    saveTemplate(templateName: string, templateContent: string) {
        const data = { templateName, templateContent };
        return this.httpClient.post('/api/template', data);
    }

}