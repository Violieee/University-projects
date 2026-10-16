import { Component } from '@angular/core';

import {
  RouterLink,
  RouterLinkActive,
  RouterOutlet
} from '@angular/router';

import { Header } from './header/header';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    Header,
    RouterLink,
    RouterLinkActive,
    RouterOutlet
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {}
