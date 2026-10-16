import { Component } from '@angular/core';
import { Table } from './Table';
import { WritingTable } from './WritingTable';

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [],
  templateUrl: './table.component.html',
})
export class TableComponent {
  table_show: string = '';
  writingtable_show: string = '';

  calcTables(name1: string, S1: string, k1: string,
             name2: string, S2: string, material: string, k2: string) {
    try {
      let s1 = Number(S1);
      let s2 = Number(S2);
      let coeff1 = Number(k1);
      let coeff2 = Number(k2);

      if (!name1.trim() || !name2.trim() || !material.trim() ||
          !S1.trim() || !S2.trim() || !k1.trim() || !k2.trim() ||
          !Number.isFinite(s1) || !Number.isFinite(s2) ||
          !Number.isFinite(coeff1) || !Number.isFinite(coeff2) ||
          s1 <= 0 || s2 <= 0) {
        throw new Error('Помилка вхідних даних');
      }

      let table = new Table(name1, s1);
      let writingtable = new WritingTable(name2, s2, material);

      this.table_show =
        'Назва = ' + table.name +
        ' Вартість = ' + table.C(coeff1).toFixed(2);

      let fullCost = writingtable.C(coeff2);

      this.writingtable_show =
        'Назва = ' + writingtable.name +
        ' Матеріал = ' + writingtable.material +
        ' Вартість обробки = ' + writingtable.processingCost.toFixed(2) +
        ' Повна вартість = ' + fullCost.toFixed(2);

    } catch {
      this.table_show = 'Помилка вхідних даних';
      this.writingtable_show = '';
    }
  }
}
