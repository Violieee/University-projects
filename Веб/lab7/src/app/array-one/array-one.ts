import { Component } from '@angular/core';

@Component({
  selector: 'app-array-one',
  standalone: true,
  templateUrl: './array-one.html'
})
export class ArrayOne {
  numbers: number[] = [];
  sum: number | null = null;
  error = '';

  calculate(text: string): void {
    this.error = '';
    this.sum = null;
    this.numbers = [];

    const parts = text.trim().split(/[,;\s]+/);

    if (
      parts.length !== 10 ||
      !parts.every(x => /^[+-]?\d+$/.test(x))
    ) {
      this.error =
        'Введіть рівно 10 цілих чисел через пробіл або кому.';
      return;
    }

    const values = parts.map(Number);

    if (!values.every(Number.isSafeInteger)) {
      this.error = 'Введено недопустимі значення.';
      return;
    }

    this.numbers = values;

    this.sum = values
      .filter(x => x > 0)
      .reduce((s, x) => s + x, 0);
  }
}
