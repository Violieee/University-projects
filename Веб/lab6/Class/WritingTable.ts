import { Table } from './Table';

export class WritingTable extends Table {
    material: string;
    processingCost: number;

    constructor(name1: string, S1: number, material1: string) {
        super(name1, S1);
        this.material = material1;
        this.processingCost = 0;
    }

    override C(k: number) {
        let cost = super.C(k);
        this.processingCost = cost * 0.1;
        return cost + this.processingCost;
    }
}