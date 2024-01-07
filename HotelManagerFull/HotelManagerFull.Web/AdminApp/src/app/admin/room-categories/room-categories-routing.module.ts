import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoomCategoriesComponent } from './room-categories.component';

const routes: Routes = [
  {
    path: '',
    component: RoomCategoriesComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RoomCategoriesRoutingModule {}
