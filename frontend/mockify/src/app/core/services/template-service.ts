import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Template } from "../models/template-model";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";

@Injectable({providedIn: 'root'})
export class TemplateService {
    
    constructor(private httpClient: HttpClient) {
    
    }

    saveTemplate(templateName: string, templateContent: string):Observable<any> {
        const Template : Template = {
            name: templateName,
            content: templateContent
        }
        const user = localStorage.getItem('user');
        if(!user) {  }
        const token = user ? JSON.parse(user).token : '';
        return this.httpClient.post(`${environment.apiUrl}template/save`, Template, {
            headers: {
                Authorization: token,
                'Content-Type': 'application/json'
            }
        });
    }
    

    getTemplates():Observable<any> {
        const user = localStorage.getItem('user');
        if(!user) {  }
        const token = user ? JSON.parse(user).token : '';

        return this.httpClient.get(`${environment.apiUrl}template`, {
            headers: {
                Authorization: token,
                'Content-Type': 'application/json'
            }
        });
    }

    deleteTemplate(templateName: string):Observable<any> {
        const user = localStorage.getItem('user');
        if(!user) {  }
        const token = user ? JSON.parse(user).token : '';
        return this.httpClient.delete(`${environment.apiUrl}template/${templateName}`, {
            headers: {
                Authorization: token,
                'Content-Type': 'application/json'
            }
        });
    }

    updateTemplate(templateName: string, templateContent: string):Observable<any> {
        const Template : Template = {
            name: templateName,
            content: templateContent
        }
        const user = localStorage.getItem('user');
        if(!user) {  }
        const token = user ? JSON.parse(user).token : '';
        return this.httpClient.put(`${environment.apiUrl}template/update`, Template, {
            headers: {
                Authorization: token,
                'Content-Type': 'application/json'
            }
        });
    }
}