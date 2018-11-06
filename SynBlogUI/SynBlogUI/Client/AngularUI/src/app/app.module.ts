import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { AddBlogComponent } from './components/add-blog/add-blog.component';
import { ConfigService } from './services/config.service';
import { SafeHTMLPipe } from './pipes/safe-html.pipe';
import { BlogComponent } from './components/blog/blog.component';
import { ActivatedRouteSnapshot, RouterModule } from '@angular/router';

export function loadConfig(configService: ConfigService) {
    return () => configService.loadConfiguration();
}

@NgModule({
    declarations: [
        AppComponent,
        RegisterComponent,
        LoginComponent,
        HomeComponent,
        NotFoundComponent,
        AddBlogComponent,
        SafeHTMLPipe,
        BlogComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule
    ],
    providers: [
        { provide: APP_INITIALIZER, useFactory: loadConfig, deps: [ConfigService], multi: true }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
