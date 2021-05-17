import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';
import {LoginComponent} from './login/login.component';
import {AuthGuardService} from './auth-guard.service';
import {AuctionComponent} from './auction/auction.component';
import {AuthInterceptor} from './auth-interceptor';
import {EditAuctionComponent} from './edit-auction/edit-auction.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FetchDataComponent,
    LoginComponent,
    AuctionComponent,
    EditAuctionComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: AuctionComponent, canActivate: [AuthGuardService]},
      {path: 'login', component: LoginComponent},
      {path: 'edit/:id', component: EditAuctionComponent, canActivate: [AuthGuardService]},
      {path: 'add', component: EditAuctionComponent, canActivate: [AuthGuardService]},
      {path: '**', redirectTo: ''}
    ])
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
