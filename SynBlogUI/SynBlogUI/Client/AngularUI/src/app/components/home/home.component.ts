import { Component, OnInit } from '@angular/core';
import { BlogService } from '../../services/blog.service';
import { Blog } from '../../models/blog';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

    private blogs:Blog[];

    constructor(private blogSvc: BlogService) { }

    ngOnInit() {
        this.blogSvc.getBlogs()
            .subscribe(
                res => {
                    this.blogs=res;
                    //console.log(this.blogs)
                },
                err => console.log(err)
            )
    }

}
