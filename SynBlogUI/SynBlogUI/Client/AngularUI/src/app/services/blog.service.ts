import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Blog } from '../models/blog';
import { Observable } from 'rxjs';
import { UserService } from './user.service';
import { ConfigService } from './config.service';

@Injectable({
    providedIn: 'root'
})
export class BlogService {

    private blogServiceUrl: string;

    constructor(private http: HttpClient, private userSvc: UserService, private configSvc:ConfigService) { 
        let config=this.configSvc.getConfiguration();
        console.log(config)
        this.blogServiceUrl=config.blogServiceUrl+ "/api/v1/blogs";
    }

    public getBlogs(): Observable<Blog[]> {
        let token = this.userSvc.getToken();
        return this.http.get<Blog[]>(this.blogServiceUrl, {
            headers: {
                "Authorization": `Bearer ${token}`,
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        });
    }

    public addBlog(blog: Blog): Observable<Blog> {
        let token = this.userSvc.getToken();
        blog.postedDate=new Date();
        blog.author=localStorage.getItem("username");
        return this.http.post<Blog>(this.blogServiceUrl,
            blog,
            {
                headers: {
                    "Authorization": `Bearer ${token}`,
                    "Accept": "application/json",
                    "Content-Type": "application/json"
                }
            });
    }

    public getBlog(id:number): Observable<Blog> {
        let token = this.userSvc.getToken();
        return this.http.get<Blog>(`${this.blogServiceUrl}/${id}`, {
            headers: {
                "Authorization": `Bearer ${token}`,
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        });
    }
}
