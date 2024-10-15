import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApplicationsComponent } from './applications/applications.component';
import { TemplatesComponent } from './templates/templates.component';
import { UsersComponent} from './users/users.component'
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    TemplatesComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    ReactiveFormsModule, 
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'applications', component: ApplicationsComponent },
      { path: 'templates', component: TemplatesComponent },
      { path: 'users', component: UsersComponent }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }