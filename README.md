# Курсовой проект
- Проект позволяет редактировать, удалять и просматривать таблицы на тему мотоциклов
- ER-Диаграмма доступна в файлах проекта под названием `diagram.png`
- Для создания был использован фреймворк `(WPF-UI)[https://wpfui.lepo.co/]` а также библиотеки:
	- `Microsoft.Extensions.Configuration`
	- `Microsoft.Extensions.DependencyInjection`
	- `Microsoft.EntityFrameworkCore`
	- `CommunityToolkit.Mvvm`

# Запуск
Скачайте сборку в разделе релизов,
или соберите проект в Visual Studio или командой `dotnet run` в терминале

# Подключение
Проект работает через **SQL Server**, загрузите в базу данных бэкап `Motorcycles.bak` и в файле `config.json` пропишите строку подключения
