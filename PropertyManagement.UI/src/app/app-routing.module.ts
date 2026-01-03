import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { authGuard } from './core/guards/auth.guard';
import { UserFormComponent } from './user/user-form/user-form.component';
import { publicGuard } from './core/guards/public.guard';
import { PropertyListComponent } from './property/property-list/property-list.component';
import { PropertyFormComponent } from './property/property-form/property-form.component';
import { PropertyDetailsComponent } from './property/property-details/property-details.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [publicGuard],
  },
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [publicGuard],
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [authGuard],
  },
  {
    path: 'dashboard/users/create',
    component: UserFormComponent,
    canActivate: [authGuard],
  },
  {
    path: 'dashboard/users/:userId/properties',
    component: PropertyListComponent,
    canActivate: [authGuard],
  },
  {
    path: 'dashboard/users/:userId/properties/create',
    component: PropertyFormComponent,
    canActivate: [authGuard],
  },
  {
    path: 'dashboard/users/:userId/properties/:propertyId',
    component: PropertyDetailsComponent,
    canActivate: [authGuard],
  },
  {
    path: 'dashboard/users/:userId/properties/:propertyId/edit',
    component: PropertyFormComponent,
    canActivate: [authGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
