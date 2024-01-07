import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';

export const adminRoutes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: '',
    component: AdminComponent,
    children: [
      {
        path: 'dashboard',
        loadChildren: () =>
          import('./dashboard/dashboard.module').then((m) => m.DashboardModule),
      },
      {
        path: 'provinces',
        loadChildren: () =>
          import('./provinces/provinces.module').then((m) => m.ProvincesModule),
      },
      {
        path: 'hotels',
        loadChildren: () =>
          import('./hotels/hotels.module').then((m) => m.HotelsModule),
      },
      {
        path: 'customers',
        loadChildren: () =>
          import('./customers/customers.module').then((m) => m.CustomersModule),
      },
      {
        path: 'services',
        loadChildren: () =>
          import('./services/services.module').then((m) => m.ServicesModule),
      },
      {
        path: 'room-categories',
        loadChildren: () =>
          import('./room-categories/room-categories.module').then(
            (m) => m.RoomCategoriesModule
          ),
      },
      {
        path: 'floors',
        loadChildren: () =>
          import('./floors/floors.module').then((m) => m.FloorsModule),
      },
      {
        path: 'user-profile',
        loadChildren: () =>
          import('./user-profile/user-profile.module').then((m) => m.UserProfileModule),
      },
    ],
  },
];
@NgModule({
  imports: [RouterModule.forChild(adminRoutes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
