import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { BlogService } from '../../services/blog.service';

@Component({
    selector: 'app-add-blog',
    templateUrl: './add-blog.component.html',
    styleUrls: ['./add-blog.component.css']
})
export class AddBlogComponent implements OnInit {

    private form: FormGroup;
    private success: boolean = false;
    private submitted: boolean = false;
    constructor(private fb: FormBuilder, private blogSvc: BlogService) { }

    ngOnInit() {
        this.form = this.fb.group({
            "title": ["", Validators.required],
            "content": ["", Validators.required],
            "keywords":new FormControl()        
        });
    }

    public async addBlog(){
                
        this.blogSvc.addBlog(this.form.value)
        .subscribe(
            res=>{
                this.submitted=true;
                this.success=true,
                this.form.reset();

            },
            err=>{
                this.success=false;
                this.submitted=true;
            }
        )
    }

}
