import { Component, OnInit } from '@angular/core';
import {  ActivatedRoute } from '@angular/router';
import { Blog } from 'src/app/models/blog';
import { BlogService } from 'src/app/services/blog.service';

@Component({
    selector: 'app-blog',
    templateUrl: './blog.component.html',
    styleUrls: ['./blog.component.css']
})
export class BlogComponent implements OnInit {

    private blog:Blog;
    private hasError:boolean=false;

    constructor(private route:ActivatedRoute, private blogSvc:BlogService) { }

    ngOnInit() {
        var blogid=this.route.snapshot.params.id;
        this.blogSvc.getBlog(blogid)
        .subscribe(
            data=>{
                this.blog=data;
                this.hasError=false;
            },
            err=>{
                this.hasError=true;
            }
        )
    }

}
