import {
    load, update, generateId, getId, url, formatDate, formatMoney,
    displayBudget, report, selected, number, validDates,
    currentRole, isEmployeeProject, returnToList,
    type Risk, type Project, type Stage, type Financial, type Scenario
} from './data';
import { npv, irr, pi, payback } from './financial';
const $ = <T extends HTMLElement = HTMLElement>(selector: string): T | null => document.querySelector<T>(selector);
const field = (id: string): HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement | null => document.getElementById(id) as HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement | null;
const value = (id: string): string => field(id)?.value.trim() || '';
const set = (id: string, val: string | number | undefined): void => { const el = field(id); if (el)
    el.value = String(val ?? ''); };
const text = (selector: string, val: string): void => { const el = $(selector); if (el)
    el.textContent = val; };
const showError = (error: unknown): void => report(error);
function rowCell(tr: HTMLTableRowElement, content: string): HTMLTableCellElement { const td = tr.insertCell(); td.textContent = content; return td; }
function projectRow(project: Project, employee: boolean): HTMLTableRowElement {
    const tr = document.createElement('tr');
    tr.dataset.status = project.status;
    const td = tr.insertCell();
    const a = document.createElement('a');
    a.href = url('project', project.id);
    a.textContent = project.name;
    td.append(a);
    const prog = tr.insertCell();
    const outer = document.createElement('div');
    outer.className = 'progress';
    const bar = document.createElement('div');
    bar.className = 'progress__bar';
    const fill = document.createElement('div');
    fill.className = 'progress__fill';
    fill.style.width = `${Math.min(100, Math.max(0, project.progress))}%`;
    bar.append(fill);
    const pct = document.createElement('span');
    pct.textContent = `${project.progress}%`;
    outer.append(bar, pct);
    prog.append(outer);
    rowCell(tr, displayBudget(project.budget));
    rowCell(tr, formatDate(project.start));
    rowCell(tr, formatDate(project.end));
    if (employee) {
        const td = tr.insertCell();
        const status = document.createElement('span');
        status.className = `status-badge status-badge--${project.approvalStatus === 'approved' ? 'approved' : 'pending'}`;
        status.textContent = project.approvalStatus === 'approved' ? 'Погоджено' : project.approvalStatus === 'revision' ? 'На доопрацюванні' : 'Очікує погодження';
        td.append(status);
    }
    if (!employee && project.approvalStatus !== 'approved') {
        const cell = tr.insertCell();
        const approve = document.createElement('button');
        approve.type = 'button';
        approve.className = 'button';
        approve.textContent = 'Погодити';
        approve.addEventListener('click', () => void update(db => { const item = db.projects.find(p => p.id === project.id); if (item) {
            item.approvalStatus = 'approved';
            if (item.status === 'pending')
                item.status = 'active';
        } }).then(() => location.reload()).catch(showError));
        cell.append(approve);
    }
    else if (!employee)
        tr.insertCell();
    return tr;
}
async function listing(employee: boolean): Promise<void> {
    const db = await load();
    const tbody = $('.project-table tbody');
    if (!tbody)
        return;
    const search = field('project-search');
    const tabs = document.querySelectorAll<HTMLButtonElement>('.tab[data-filter]');
    let active = 'all';
    const redraw = () => {
        let projects = employee ? db.projects.filter(isEmployeeProject) : db.projects;
        if (active === 'pending')
            projects = projects.filter(p => p.status === 'pending' || p.approvalStatus === 'pending');
        projects = projects.filter(p => p.name.toLocaleLowerCase('uk').includes((search?.value || '').toLocaleLowerCase('uk')));
        tbody.replaceChildren(...projects.map(p => projectRow(p, employee)));
    };
    if (!employee) {
        const heading = document.querySelector('.project-table thead tr');
        if (heading && !heading.querySelector('.approval-heading')) {
            const th = document.createElement('th');
            th.className = 'approval-heading';
            th.textContent = 'Дії';
            heading.append(th);
        }
        const counts = document.querySelectorAll<HTMLElement>('.tab__count');
        if (counts[0])
            counts[0].textContent = String(db.projects.length);
        if (counts[1])
            counts[1].textContent = String(db.projects.filter(p => p.status === 'pending' || p.approvalStatus === 'pending').length);
    }
    else {
        const count = $('.tab__count');
        if (count)
            count.textContent = String(db.projects.filter(isEmployeeProject).length);
    }
    tabs.forEach(tab => tab.addEventListener('click', () => { active = tab.dataset.filter || 'all'; tabs.forEach(t => { t.classList.toggle('tab--active', t === tab); t.setAttribute('aria-pressed', String(t === tab)); }); redraw(); }));
    search?.addEventListener('input', redraw);
    redraw();
}
function stageRows(stages: Stage[]): void {
    const tbody = $('.project-form__table tbody');
    if (!tbody)
        return;
    tbody.replaceChildren();
    if (!stages.length) {
        const tr = document.createElement('tr');
        const td = rowCell(tr, 'Етапи ще не додано');
        td.colSpan = 4;
        tbody.append(tr);
        return;
    }
    for (const s of stages) {
        const tr = document.createElement('tr');
        [s.name, formatDate(s.start), formatDate(s.end), ({ planned: 'Заплановано', active: 'У процесі', completed: 'Завершено' }[s.status])].forEach(v => rowCell(tr, v));
        tbody.append(tr);
    }
}
function readStage(): Stage | null {
    const name = value('stage-name'), start = value('stage-start'), end = value('stage-end');
    if (!name || !validDates(start, end)) {
        alert('Вкажіть назву етапу та коректні дати.');
        return null;
    }
    return { name, start, end, status: value('stage-status') as Stage['status'] };
}
async function formPage(edit: boolean): Promise<void> {
    const stages: Stage[] = [];
    let docs: string[] = [];
    let old: Project | undefined;
    if (edit) {
        const db = await load();
        old = selected(db);
        if (!old || (currentRole() === 'employee' && !isEmployeeProject(old)))
            throw Error('Проєкт не знайдено серед Ваших проєктів.');
        set('project-name', old.name);
        set('project-description', old.description);
        set('project-budget', old.budget);
        set('project-manager', old.manager);
        set('project-start', old.start);
        set('project-end', old.end);
        set('project-status', old.status);
        set('project-progress', old.progress);
        set('approval-status', old.approvalStatus);
        set('approval-comment', old.approvalComment);
        text('.project-form__heading p', old.name);
        stages.push(...(old.stages || []));
        docs = [...(old.documents || [])];
    }
    stageRows(stages);
    $('.project-form__secondary')?.addEventListener('click', () => { const s = readStage(); if (s) {
        stages.push(s);
        stageRows(stages);
        ['stage-name', 'stage-start', 'stage-end'].forEach(id => set(id, ''));
    } });
    const form = $<HTMLFormElement>('form.project-form__content');
    const save = async () => {
        if (!form?.reportValidity())
            return;
        if (!validDates(value('project-start'), value('project-end'))) {
            alert('Дата завершення не може передувати даті початку.');
            return;
        }
        const fileInput = field('project-files') as HTMLInputElement | null;
        const fileNames = Array.from(fileInput?.files || []).map(f => f.name);
        const status = (value('project-status') || 'draft') as Project['status'];
        const approval = (currentRole() === 'employee' ? (old?.approvalStatus || 'pending') : (value('approval-status') || old?.approvalStatus || 'pending')) as Project['approvalStatus'];
        const newProject: Project = {
            id: old?.id || generateId(),
            name: value('project-name'),
            description: value('project-description'),
            budget: number(value('project-budget')),
            manager: value('project-manager'),
            start: value('project-start'),
            end: value('project-end'), status,
            progress: Math.min(100, Math.max(0, number(value('project-progress') || String(old?.progress || 0)))),
            approvalStatus: approval,
            approvalComment: value('approval-comment') || old?.approvalComment || '',
            stages: [...stages],
            documents: [...docs, ...fileNames],
            scenarios: old?.scenarios || [],
            financial: old?.financial,
            risk: old?.risk,
            risks: old?.risks,
            owner: old?.owner || (currentRole() === 'employee' ? 'employee' : 'manager'),
            actualExpenses: old?.actualExpenses
        };
        await update(db => { const index = db.projects.findIndex(p => p.id === newProject.id); if (index === -1)
            db.projects.push(newProject);
        else
            db.projects[index] = newProject; });
        location.href = url('project', newProject.id);
    };
    $('.project-form__submit')?.addEventListener('click', () => void save().catch(showError));
}
function projectDetails(p: Project): void {
    text('.project-details__top h1', p.name);
    const cards = document.querySelectorAll('.project-details__card');
    const setCard = (ci: number, values: string[]) => { const nodes = cards[ci]?.querySelectorAll('strong'); nodes?.forEach((node, i) => { if (values[i] !== undefined)
        node.textContent = values[i]; }); };
    setCard(0, [p.name, ({ draft: 'Чернетка', pending: 'На затвердження', active: 'У процесі', completed: 'Завершено' }[p.status]), `${p.progress}%`]);
    setCard(1, [formatMoney(p.budget), formatMoney(p.actualExpenses || 0)]);
    setCard(2, [formatDate(p.start), formatDate(p.end)]);
    document.querySelectorAll<HTMLAnchorElement>('.project-details__actions a').forEach(a => { const name = a.getAttribute('href')?.replace('/', '').replace('.html', ''); if (name)
        a.href = url(name, p.id); });
}
function readFinancial(): Financial { return { investment: number(value('investment')), income: number(value('income')), expenses: number(value('expenses')), discount: number(value('discount')), duration: Math.floor(number(value('duration'))) }; }
function showMetrics(f: Financial): void {
    const nodes = document.querySelectorAll<HTMLElement>('.evaluation__result strong');
    const values = [`${formatMoney(npv(f))}`, irr(f) === null ? 'Не визначено' : `${irr(f)!.toFixed(2)} %`, pi(f) === null ? 'Не визначено' : pi(f)!.toFixed(2), payback(f) === null ? 'Не визначено' : `${payback(f)!.toFixed(2)} років`];
    nodes.forEach((n, i) => n.textContent = values[i]);
}
async function evaluation(): Promise<void> {
    const p = selected(await load());
    if (!p || (currentRole() === 'employee' && !isEmployeeProject(p)))
        throw Error('Проєкт не знайдено.');
    text('.evaluation__heading p', p.name);
    if (p.financial)
        Object.entries(p.financial).forEach(([k, v]) => set(k, v));
    else
        set('investment', p.budget);
    const risks: Risk[] = [...(p.risks || (p.risk ? [p.risk] : []))];
    const renderRisks = () => {
        const container = $('#risk-list');
        if (!container)
            return;
        container.replaceChildren();
        risks.forEach((r, i) => {
            const entry = document.createElement('div');
            entry.style.cssText = 'border:1px solid #d7dce4;border-radius:8px;padding:12px;margin-bottom:10px';
            const title = document.createElement('strong');
            title.textContent = `Ризик ${i + 1}: ${r.description || r.type} (${r.level})`;
            const detail = document.createElement('p');
            detail.textContent = `Заходи: ${r.actions || '—'}`;
            const remove = document.createElement('button');
            remove.type = 'button';
            remove.className = 'action-button';
            remove.textContent = 'Видалити ризик';
            remove.addEventListener('click', () => { risks.splice(i, 1); renderRisks(); });
            entry.append(title, detail, remove);
            container.append(entry);
        });
    };
    $('#add-risk')?.addEventListener('click', () => { const description = value('risk-description'), actions = value('risk-actions'); if (!description) {
        alert('Вкажіть опис ризику.');
        return;
    } risks.push({ type: value('risk-type'), level: value('risk-level'), description, actions }); set('risk-description', ''); set('risk-actions', ''); renderRisks(); });
    renderRisks();
    const refresh = () => { const f = readFinancial(); if (f.duration > 0 && f.investment > 0)
        showMetrics(f); };
    document.querySelectorAll<HTMLInputElement>('.evaluation__form input').forEach(i => i.addEventListener('input', refresh));
    refresh();
    $('.evaluation__submit')?.addEventListener('click', () => void (async () => {
        const f = readFinancial();
        if (f.duration < 1 || f.investment <= 0 || f.income < 0 || f.expenses < 0 || f.discount < 0) {
            alert('Перевірте фінансові показники: інвестиції й тривалість мають бути додатними.');
            return;
        }
        const remainingDescription = value('risk-description');
        if (remainingDescription)
            risks.push({ type: value('risk-type'), level: value('risk-level'), description: remainingDescription, actions: value('risk-actions') });
        await update(db => { const proj = db.projects.find(v => v.id === p.id); if (!proj)
            throw Error('Проєкт не знайдено.'); proj.financial = f; proj.risks = [...risks]; delete proj.risk; });
        location.href = url('project', p.id);
    })().catch(showError));
}
function scenarioTable(scenarios: Scenario[]): void {
    const tbody = $('.scenarios__table tbody');
    const thead = $('.scenarios__table thead tr');
    if (!tbody || !thead)
        return;
    thead.replaceChildren();
    ['Показник', ...scenarios.map(s => s.name)].forEach(v => { const th = document.createElement('th'); th.textContent = v; thead.append(th); });
    tbody.replaceChildren();
    const fmt = (s: Scenario): Financial => ({ investment: s.investment, income: s.income, expenses: s.expenses, discount: 0, duration: s.duration });
    const matrix: [
        string,
        (s: Scenario) => string
    ][] = [['Початкові інвестиції', s => formatMoney(s.investment)], ['Річний дохід', s => formatMoney(s.income)], ['Річні витрати', s => formatMoney(s.expenses)], ['Тривалість', s => `${s.duration} років`], ['NPV (без дисконтування)', s => formatMoney(npv(fmt(s)))], ['IRR', s => irr(fmt(s)) === null ? 'Не визначено' : `${irr(fmt(s))!.toFixed(2)} %`]];
    for (const [label, transform] of matrix) {
        const tr = document.createElement('tr');
        rowCell(tr, label);
        scenarios.forEach(s => rowCell(tr, transform(s)));
        tbody.append(tr);
    }
    text('.scenarios__note', 'NPV сценаріїв розраховано за нульової ставки дисконтування (у формі сценарію ставку не передбачено).');
}
async function scenariosPage(): Promise<void> {
    const p = selected(await load());
    if (!p || (currentRole() === 'employee' && !isEmployeeProject(p)))
        throw Error('Проєкт не знайдено.');
    text('.scenarios__heading p', p.name);
    const scenarios = [...(p.scenarios || [])];
    const redrawScenarios = () => { scenarioTable(scenarios); const container = $('#scenario-delete-list'); if (!container)
        return; container.replaceChildren(); scenarios.forEach(s => { const button = document.createElement('button'); button.type = 'button'; button.className = 'action-button'; button.style.margin = '4px'; button.textContent = `Видалити: ${s.name}`; button.addEventListener('click', () => { if (!confirm(`Видалити сценарій «${s.name}»?`))
        return; void update(db => { const project = db.projects.find(q => q.id === p.id); if (!project)
        throw Error('Проєкт не знайдено.'); project.scenarios = (project.scenarios || []).filter(q => q.id !== s.id); }).then(() => { scenarios.splice(scenarios.findIndex(q => q.id === s.id), 1); redrawScenarios(); }).catch(showError); }); container.append(button); }); };
    redrawScenarios();
    $('.scenarios__button')?.addEventListener('click', () => void (async () => {
        const s: Scenario = { id: generateId(), name: value('scenario-name'), type: value('scenario-type'), investment: number(value('scenario-investment')), income: number(value('scenario-income')), expenses: number(value('scenario-expenses')), duration: Math.floor(number(value('scenario-duration'))) };
        if (!s.name || s.duration < 1 || s.investment <= 0 || s.income < 0 || s.expenses < 0) {
            alert('Вкажіть назву, додатні інвестиції й тривалість сценарію.');
            return;
        }
        await update(db => { const proj = db.projects.find(q => q.id === p.id); if (!proj)
            throw Error('Проєкт не знайдено.'); (proj.scenarios ||= []).push(s); });
        scenarios.push(s);
        redrawScenarios();
        set('scenario-name', '');
    })().catch(showError));
}
export async function initialize(): Promise<void> {
    const page = location.pathname.split('/').pop() || 'index.html';
    if (page === 'index.html')
        return listing(false);
    if (page === 'employee.html')
        return listing(true);
    if (page === 'create.html')
        return formPage(false);
    if (page === 'edit.html')
        return formPage(true);
    if (page === 'evaluation.html')
        return evaluation();
    if (page === 'scenarios.html')
        return scenariosPage();
    if (page === 'project.html') {
        const p = selected(await load());
        if (!p || (currentRole() === 'employee' && !isEmployeeProject(p)))
            throw Error('Проєкт не знайдено.');
        projectDetails(p);
        $('#delete-project')?.addEventListener('click', () => { if (!confirm(`Видалити проєкт «${p.name}»? Цю дію неможливо скасувати.`))
            return; void update(db => { db.projects = db.projects.filter(q => q.id !== p.id); }).then(() => location.href = returnToList()).catch(showError); });
    }
}
export function setupNavigation(): void {
    const id = getId();
    if (!id)
        return;
    document.querySelectorAll<HTMLAnchorElement>('a[href="/project.html"]').forEach(a => a.href = url('project', id));
}
export function start(): void {
    document.querySelectorAll<HTMLAnchorElement>('.role-switch__link').forEach(a => a.addEventListener('click', () => sessionStorage.setItem('projectRole', a.href.endsWith('/employee.html') ? 'employee' : 'manager')));
    if (location.pathname.endsWith('/employee.html'))
        sessionStorage.setItem('projectRole', 'employee');
    if (location.pathname === '/' || location.pathname.endsWith('/index.html'))
        sessionStorage.setItem('projectRole', 'manager');
    const role = currentRole();
    document.querySelectorAll<HTMLAnchorElement>('.role-switch__link').forEach(a => { const active = role === 'employee' ? a.href.endsWith('/employee.html') : !a.href.endsWith('/employee.html'); a.classList.toggle('role-switch__link--active', active); if (active)
        a.setAttribute('aria-current', 'page');
    else
        a.removeAttribute('aria-current'); });
    const back = document.querySelector<HTMLAnchorElement>('.project-details__back');
    if (back)
        back.href = returnToList();
    document.querySelectorAll<HTMLAnchorElement>('.brand').forEach(a => a.href = returnToList());
    if (role === 'employee') {
        document.querySelector('.profile__details strong')!.textContent = 'Петренко П. П.';
        document.querySelector('.profile__details small')!.textContent = 'Працівник';
        const avatar = document.querySelector('.profile__avatar');
        if (avatar)
            avatar.textContent = 'ПП';
        const approvalFields = document.getElementById('approval-status')?.closest('.project-form__group');
        if (approvalFields instanceof HTMLElement)
            approvalFields.style.display = 'none';
        const approvalComment = document.getElementById('approval-comment')?.closest('.project-form__group');
        if (approvalComment instanceof HTMLElement)
            approvalComment.style.display = 'none';
    }
    initialize().then(setupNavigation).catch(showError);
}
