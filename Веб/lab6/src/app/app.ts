import { Component, signal } from '@angular/core';
import { Header } from './header/header';
import { TableComponent } from '../../Class/table.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [Header, TableComponent],
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('lab6');
}
