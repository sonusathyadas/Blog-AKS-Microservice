import { Component } from '@angular/core';
import { UserService } from './services/user.service';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ConfigService } from './services/config.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'SynBlog';
    username:string=null;

    constructor(private userSvc:UserService, 
        private router:Router,
        private configSvc:ConfigService) {
            console.log(this.configSvc.getConfiguration());
        
    }

    public logout(){
        this.userSvc.logout();
        this.router.navigate(['/']);
    }

    public isAuthenticated():Observable<boolean>{
        this.username=this.userSvc.getUser();
        return this.userSvc.isAunthenticated()
    }
}
