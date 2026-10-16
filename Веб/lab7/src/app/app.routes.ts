import { Routes } from '@angular/router';

import { Home } from './home/home';
import { Lab4 } from './lab4/lab4';
import { TableComponent } from '../../Class/table.component';
import { ArrayOne } from './array-one/array-one';
import { ArrayTwo } from './array-two/array-two';

export const routes: Routes = [
  {
    path: '',
    component: Home,
    pathMatch: 'full'
  },
  {
    path: 'lab4',
    component: Lab4
  },
  {
    path: 'lab6',
    component: TableComponent
  },
  {
    path: 'array-one',
    component: ArrayOne
  },
  {
    path: 'array-two',
    component: ArrayTwo
  },
  {
    path: '**',
    redirectTo: ''
  }
];
