import { Component } from '@angular/core';

@Component({
  selector: 'app-array-two',
  standalone: true,
  templateUrl: './array-two.html'
})
export class ArrayTwo {
  matrix: number[][] = [];
  error = '';

  private readNumbers(
    text: string,
    count: number
  ): number[] | null {

    const parts = text.trim().split(/[,;\s]+/);

    const realNumber =
      /^[+-]?(?:\d+(?:\.\d*)?|\.\d+)(?:[eE][+-]?\d+)?$/;

    if (
      parts.length !== count ||
      !parts.every(x => realNumber.test(x))
    ) {
      return null;
    }

    const values = parts.map(Number);

    return values.every(Number.isFinite)
      ? values
      : null;
  }

  calculate(aText: string, bText: string): void {
    this.error = '';
    this.matrix = [];

    const a = this.readNumbers(aText, 10);
    const b = this.readNumbers(bText, 20);

    if (!a || !b) {
      this.error =
        'Введіть 10 чисел a і 20 чисел b. ' +
        'Для дробових чисел використовуйте крапку.';
      return;
    }

    this.matrix = b.map(
      bi => a.map(
        aj => aj / (1 + Math.abs(bi))
      )
    );
  }
}
