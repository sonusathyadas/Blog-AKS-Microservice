import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Bloguser } from '../models/bloguser';
import { LoginUser } from '../models/loginuser';
import { ConfigService } from './config.service';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    private authServiceUrl: string;

    constructor(private http: HttpClient,private configSvc:ConfigService) { 
        let config=this.configSvc.getConfiguration();
        this.authServiceUrl=config.userServiceUrl ;
        //console.log(config);

    }
    public registerUser(blogUser: Bloguser): Observable<Bloguser> {
        console.log(`${this.authServiceUrl}/api/v1/users/register`);
        return this.http.post<Bloguser>(
            `${this.authServiceUrl}/api/v1/users/register`,
            blogUser,
            {
                headers: {
                    "content-Type": "application/json"
                }
            }
        );
    }

    public loginUser(user: LoginUser): Observable<any> {
        return this.http.post<any>(`${this.authServiceUrl}/api/v1/users/token`,
            user, {
                headers: {
                    "content-Type": "application/json"
                }
            });
    }

    public isAunthenticated(): Observable<boolean> {
        if (this.getUser())
            return of(true);
        else
            return of(false);
    }
    public getUser(): string {
        return localStorage.getItem("username");
    }

    public getToken(): string {
        return localStorage.getItem("token");
    }

    public logout(): void {
        localStorage.removeItem("username");
        localStorage.removeItem("token");
    }
}
