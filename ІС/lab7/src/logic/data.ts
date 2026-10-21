export type Status = 'draft' | 'pending' | 'active' | 'completed';

export type Approval = 'pending' | 'approved' | 'revision';

export interface Stage {
  name: string;
  start: string;
  end: string;
  status: 'planned' | 'active' | 'completed';
}

export interface Financial {
  investment: number;
  income: number;
  expenses: number;
  discount: number;
  duration: number;
}

export interface Risk {
  type: string;
  level: string;
  description: string;
  actions: string;
}

export interface Scenario {
  id: string;
  name: string;
  type: string;
  income: number;
  expenses: number;
  investment: number;
  duration: number;
}

export interface Project {
  id: string;
  name: string;
  description: string;
  budget: number;
  start: string;
  end: string;
  manager: string;
  status: Status;
  progress: number;
  approvalStatus: Approval;
  approvalComment: string;
  stages: Stage[];
  documents: string[];
  financial?: Financial;
  risk?: Risk;
  risks?: Risk[];
  owner?: string;
  scenarios: Scenario[];
  actualExpenses?: number;
}

export interface Database {
  projects: Project[];
}

const binId = import.meta.env.VITE_JSONBIN_BIN_ID?.trim() || '';
const baseUrl = `https://api.jsonbin.io/v3/b/${encodeURIComponent(binId)}`;

let queue: Promise<unknown> = Promise.resolve();

export class DataError extends Error {}

function checkConfig(): void {
  if (!binId) {
    throw new DataError('Налаштуйте VITE_JSONBIN_BIN_ID у файлі .env.');
  }
}

function normalize(raw: unknown): Database {
  if (
    !raw ||
    typeof raw !== 'object' ||
    !Array.isArray((raw as Database).projects)
  ) {
    throw new DataError(
      'JSONBin повинен містити об’єкт із масивом projects.'
    );
  }

  return raw as Database;
}

async function request(url: string, init: RequestInit): Promise<unknown> {
  checkConfig();

  let response: Response;

  try {
    response = await fetch(url, {
      ...init,
      headers: {
        ...(init.headers || {})
      }
    });
  } catch {
    throw new DataError(
      'Не вдається зв’язатися з JSONBin. Перевірте інтернет та налаштування.'
    );
  }

  if (!response.ok) {
    throw new DataError(
      `JSONBin: помилка ${response.status}. Перевірте Bin ID, ключ та права доступу.`
    );
  }

  return response.json();
}

export async function load(): Promise<Database> {
  const data = await request(`${baseUrl}/latest`, {
    method: 'GET'
  }) as { record?: unknown };

  return normalize(data.record);
}

async function save(db: Database): Promise<void> {
  await request(baseUrl, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(db)
  });
}

export function update<T>(
  fn: (db: Database) => T | Promise<T>
): Promise<T> {
  const job = queue.then(async () => {
    const db = await load();
    const result = await fn(db);

    await save(db);

    return result;
  });

  queue = job.catch(() => undefined);

  return job;
}

export function currentRole(): 'employee' | 'manager' {
  return sessionStorage.getItem('projectRole') === 'employee'
    ? 'employee'
    : 'manager';
}

export function isEmployeeProject(p: Project): boolean {
  return (
    p.owner === 'employee' ||
    (!p.owner && ['project-2', 'project-4', 'project-5'].includes(p.id))
  );
}

export function returnToList(): string {
  return currentRole() === 'employee'
    ? '/employee.html'
    : '/';
}

export function generateId(): string {
  return crypto.randomUUID();
}

export function getId(): string | null {
  return new URLSearchParams(location.search).get('id');
}

export function url(page: string, id: string): string {
  return `/${page}.html?id=${encodeURIComponent(id)}`;
}

export function formatDate(value: string): string {
  if (!value) {
    return '—';
  }

  const [y, m, d] = value.split('-');

  return `${d}.${m}.${y}`;
}

export function formatMoney(value: number): string {
  return `${new Intl.NumberFormat('uk-UA').format(value)} грн`;
}

export function displayBudget(value: number): string {
  return value >= 1_000_000
    ? `${new Intl.NumberFormat('uk-UA', {
        maximumFractionDigits: 2
      }).format(value / 1_000_000)} млн грн`
    : formatMoney(value);
}

export function report(error: unknown): void {
  window.alert(
    error instanceof Error
      ? error.message
      : 'Не вдалося виконати операцію.'
  );
}

export function selected(db: Database): Project | undefined {
  return (
    db.projects.find(p => p.id === getId()) ||
    (!getId() ? db.projects[0] : undefined)
  );
}

export function number(value: string): number {
  return Number(value) || 0;
}

export function validDates(start: string, end: string): boolean {
  return Boolean(start && end && start <= end);
}
