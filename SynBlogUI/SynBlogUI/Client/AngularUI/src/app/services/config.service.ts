import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Configuration } from '../models/configuration';

@Injectable({
    providedIn: 'root'
})
export class ConfigService {

    private configuration: Configuration;

    constructor(private http: HttpClient) {
        this.configuration=new Configuration();
     }

    public loadConfiguration(): Promise<Configuration> {
        const baseURI = document.baseURI.endsWith('/') ? document.baseURI : `${document.baseURI}/`;
        
        var promise = this.http.get<Configuration>(`${baseURI}api/v1/config`)
            .toPromise()
            .then(data => {
                this.configuration=data;
                return data;
            });
        return promise;

    }

    public getConfiguration():Configuration{
        return this.configuration;
    }
}
