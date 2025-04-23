import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Template } from "../models/template-model";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { JsonEditorModel } from "../models/category.model";

@Injectable({providedIn: 'root'})
export class TemplateService {
    constructor(private httpClient: HttpClient) {}

    saveTemplate(templateName: string, templateContent: JsonEditorModel[]):Observable<any> {
        const Template : Template = { name: templateName, content: templateContent}
        return this.httpClient.post(`${environment.apiUrl}template/save`, Template);
    }
    
    getTemplates():Observable<any> {        
        return this.httpClient.get(`${environment.apiUrl}template`);
    }

    deleteTemplate(templateName: string):Observable<any> {        
        return this.httpClient.delete(`${environment.apiUrl}template/delete/${templateName}`);
    }

    updateTemplate(templateName: string, templateContent: JsonEditorModel[], oldTemplateName: string):Observable<any> {
        const Template : Template = {name: templateName, content: templateContent}
        return this.httpClient.put(`${environment.apiUrl}template/update/${oldTemplateName}`, Template);
    }
}