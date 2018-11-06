import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

    private form: FormGroup;
    private success:boolean=false;
    private submitted:boolean=false;
    constructor(private fb: FormBuilder, private svc:UserService) { }

    ngOnInit() {
        this.form = this.fb.group({
            "FirstName": ["", Validators.required],
            "LastName": ["", Validators.required],
            "Email": ["", Validators.compose([Validators.required, Validators.email])],
            "Password": ["", Validators.compose([Validators.required, Validators.minLength(8)])],
        });
    }

    public register(): void {
        if(this.form.valid){
        this.svc.registerUser(this.form.value)
        .subscribe(
            res=>{
                this.success=true;
                this.submitted=true;
            },
            err=>{
                this.success=false;
                this.submitted=true;
            }
        )
        }
        else{
            alert("Invalid form data");
        }
    }

}
