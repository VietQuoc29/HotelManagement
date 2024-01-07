import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './not-found.component';

const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: '/not-found' },
    {
        path: '',
        component: NotFoundComponent,
        children: [
            {
                path: 'not-found',
                loadChildren: () =>
                    import('../not-found/not-found.module').then((m) => m.NotFoundModule),
            },
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class NotFoundRoutingModule { }
