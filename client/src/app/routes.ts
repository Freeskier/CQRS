import { Routes } from '@angular/router';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AnonymousGuard } from './_guards/anonymous.guard';
import { AuthGuard } from './_guards/auth.guard';
import { AccountDetailsResolver } from './_resolvers/account-details.resolver';

export const routes: Routes = [
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [AnonymousGuard],
  },
  { path: 'login', component: LoginComponent, canActivate: [AnonymousGuard] },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'profile/:id',
    component: AccountDetailsComponent,
    runGuardsAndResolvers: 'always',
    resolve: { user: AccountDetailsResolver },
    canActivate: [AuthGuard],
  },
  { path: '', component: LoginComponent, canActivate: [AnonymousGuard] },
];
