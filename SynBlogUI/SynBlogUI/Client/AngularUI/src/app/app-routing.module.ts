import { NgModule } from '@angular/core';
import { Routes, RouterModule, ActivatedRouteSnapshot } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { AddBlogComponent } from './components/add-blog/add-blog.component';
import { AuthGuard } from './guards/auth.guard';
import { BlogComponent } from './components/blog/blog.component';

const routes: Routes = [
    {
        path:"",
        component:LoginComponent,
        pathMatch:"full"
    },
    {
        path:"blogs",
        component:HomeComponent,
        canActivate:[AuthGuard]        
    },
    {
        path:"blogs/:id",
        component:BlogComponent,
        canActivate:[AuthGuard]  
    },
    {
        path:"register",
        component:RegisterComponent
    },
    {
        path:"addblog",
        component:AddBlogComponent,
        canActivate:[AuthGuard]
    },
    {
        path:"**",
        component:NotFoundComponent
    }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
