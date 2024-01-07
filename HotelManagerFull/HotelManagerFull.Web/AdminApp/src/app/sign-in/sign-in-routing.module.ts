import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInComponent } from './sign-in.component';

const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: '/sign-in' },
    {
        path: '',
        component: SignInComponent,
        children: [
            {
                path: 'sign-in',
                loadChildren: () =>
                    import('../sign-in/sign-in.module').then((m) => m.SignInModule),
            },
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class SignInRoutingModule { }
