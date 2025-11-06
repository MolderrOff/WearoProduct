Разработан базовый backend для интернет-магазина

Написано в  CLEAN - архитектуре

Реализован REST API. Стандартный CRUD

Минимальная валидация входных данных

Проект решения .sln находится в папке WearoSkillbox. Это .Host. Нужно поменять SqlConnection в файле appsettings.json на свой.
БД сделана на базе MS SQL. Если другая введите изменения в файле InfrastructureConfigurator.cs
Сделайте миграцию и обновите базу данных введя в терминале следующие команды

// dotnet tool install --global dotnet-ef

// dotnet ef migrations add InitialCreate

// dotnet ef database update 
