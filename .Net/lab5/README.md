# lab5 — список вкладників банку (WPF)

Проєкт лабораторної роботи за **варіантом 5**. Назва головного WPF-проєкту, збірки та рішення: **lab5**.

## Як відкрити

1. На **Windows** встановити Visual Studio 2022 із компонентом **«Розробка класичних програм .NET»** (.NET Desktop Development) та **.NET 8 SDK**.
2. Відкрити `lab5.sln`.
3. Обрати стартовим проєкт `lab5` (за потреби через меню **Set as Startup Project**).
4. Натиснути **F5** або **Ctrl+F5** для запуску WPF-вікна.
5. Вибрати **Test → Run All Tests** для запуску Unit-тестів MSTest.

Альтернативно в терміналі Windows із каталогу рішення:

```powershell
dotnet restore lab5.sln
dotnet build lab5.sln
dotnet test lab5.Tests/lab5.Tests.csproj
dotnet run --project lab5/lab5.csproj
```

## Робота програми

- Внести **ПІБ**, **№ рахунку**, **суму вкладу** та **рік відкриття**; натиснути «Додати вкладника».
- Для видалення вибрати рядок і натиснути «Видалити».
- Внизу автоматично проставлено **поточний рік**. Натиснути «Фільтр», щоб показати вкладників цього року **відсортованих за сумою вкладу**. Можна вказати інший рік; щоб повернути **всі записи**, очистити поле року і натиснути «Фільтр».
- «Сортувати за сумою» відображає рядки за зростанням суми. Сортування діє і на відфільтровані дані.
- «Зберегти» і «Завантажити» асинхронно працюють із JSON-файлом `Документи/lab5/deposits.json` (системна папка **Documents** користувача Windows). Зберігаються **всі вкладники**, а не тільки видимі після фільтра.

Для швидкого заповнення можна скопіювати `Examples/deposits.sample.json` у `Документи/lab5/deposits.json` і натиснути «Завантажити».

## Відповідність діаграмі класів

- `Depositor` — `FullName`, `AccountNumber`, `Amount`, `YearOpened`; `ToString()`, `CompareTo()` (`IComparable<Depositor>`), перевірка даних.
- `AmountComparer` — `IComparer<Depositor>`; сортування за сумою.
- `CurrentYearDepositorFilter` — `Filter(IEnumerable<Depositor>, int)`.
- `Bank` — додавання, видалення за номером, отримання всіх вкладників / за поточний рік, сортування, збереження й завантаження; `ObservableCollection<Depositor>` для таблиці WPF.
- `IFileSaver`, `IFileLoader`, `JsonFileWriter`, `JsonFileReader` — асинхронна робота з JSON.
- `lab5.Tests` — Unit-тести класів і інтеграційні тести файлів (із тимчасовими файлами та очищенням `finally`).

## Організація рішення

```text
lab5.sln
lab5/          — застосунок WPF (MainWindow.xaml, MainWindow.xaml.cs)
lab5.Core/     — класи предметної області, фільтр, порівняння, JSON
lab5.Tests/    — тести MSTest
Examples/      — приклад файлу JSON
```

> WPF працює на Windows. Для цього проєкту потрібна відповідна Windows-конфігурація .NET Desktop; на Linux чи macOS графічний застосунок WPF не запускається.
