import { Component } from '@angular/core';

@Component({
  selector: 'app-lab4',
  standalone: true,
  template: `
    <iframe
      class="lab4-frame"
      src="/lab4/index.html"
      title="Лабораторна робота №4">
    </iframe>
  `,
  styles: [`
    .lab4-frame {
      display: block;
      width: 100%;
      height: 85vh;
      min-height: 650px;
      border: 0;
    }
  `]
})
export class Lab4 {}
