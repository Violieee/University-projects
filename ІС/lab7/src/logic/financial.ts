import type { Financial } from './data';

export function npv(f: Financial): number {
  const cash = f.income - f.expenses;
  const rate = f.discount / 100;

  return -f.investment + Array.from(
    { length: f.duration },
    (_, i) => cash / (1 + rate) ** (i + 1)
  ).reduce((a, b) => a + b, 0);
}

export function irr(f: Financial): number | null {
  const cash = f.income - f.expenses;

  if (
    f.investment <= 0 ||
    cash <= 0 ||
    f.duration < 1 ||
    cash * f.duration <= f.investment
  ) {
    return null;
  }

  let lo = 0;
  let hi = 1;

  const calc = (r: number) =>
    -f.investment + Array.from(
      { length: f.duration },
      (_, i) => cash / (1 + r) ** (i + 1)
    ).reduce((a, b) => a + b, 0);

  while (calc(hi) > 0 && hi < 1e6) {
    hi *= 2;
  }

  for (let i = 0; i < 100; i++) {
    const mid = (lo + hi) / 2;

    if (calc(mid) > 0) {
      lo = mid;
    } else {
      hi = mid;
    }
  }

  return (lo + hi) * 50;
}

export function pi(f: Financial): number | null {
  return f.investment > 0
    ? (npv(f) + f.investment) / f.investment
    : null;
}

export function payback(f: Financial): number | null {
  const cash = f.income - f.expenses;

  return cash > 0
    ? f.investment / cash
    : null;
}
