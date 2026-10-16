export class Table {
    name: string;
    S: number;

    constructor(name1: string, S1: number) {
        if (name1.length > 0 && S1 > 0) {
            this.name = name1;
            this.S = S1;
        } else {
            this.name = "Стіл";
            this.S = 100;
        }
    }

    C(k: number) {
        return this.S ** 2 / 3 + k;
    }
}
