import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    private form: FormGroup;
    private success: boolean = false;
    private submitted: boolean = false;
    constructor(private fb: FormBuilder, private svc: UserService, private router:Router) { 
        this.svc.isAunthenticated()
        .subscribe(
            data=>{
                if(data){
                    this.router.navigate(["/blogs"]);
                }
            }
        )
    }

    ngOnInit() {
        this.form = this.fb.group({
            "Email": ["", Validators.compose([Validators.required, Validators.email])],
            "Password": ["", Validators.compose([Validators.required, Validators.minLength(8)])],
        });
    }

    public login(): void {
        if (this.form.valid) {
            this.svc.loginUser(this.form.value)
                .subscribe(
                    res => {
                        localStorage.setItem("username", this.form.controls["Email"].value)
                        localStorage.setItem("token", res.token);
                        this.router.navigate(["/blogs"]);
                    },
                    err => {
                        this.success = false;
                        this.submitted = true;
                    }
                )
        }
        else {
            alert("Invalid form data");
        }
    }

}
