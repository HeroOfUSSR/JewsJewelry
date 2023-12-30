<h1>Предметная область:</h1>

<h3>Автоматизация процесса заказа ювелирных изделий</h3>

<h1>Студент:</h1>

<h3>Иванов Алексей Анатольевич ИП-20-3</h3>

<h1>Схема взаимодействия сущностей:</h1>

![Mermaid](https://github.com/HeroOfUSSR/JewsJewelry/assets/104492239/29d4caea-1f95-462d-b49a-4838ba83dfb0)

<h1>SQL Скрипт:</h1>

  INSERT INTO Workshop (Id, Name, Address, Speciality, Workplaces, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, DeletedAt)
  VALUES ('90b8298b-2cb6-4437-9b54-6b54e877f4b5', 'Жёсткая мастерская', 'Невский 111', 'Тонкая работа', 40, 
  SYSDATETIMEOFFSET(), USER_NAME(), SYSDATETIMEOFFSET(), USER_NAME(), NULL);

  INSERT INTO Craftsmen (Id, Name, Surname, Patronymic, PhoneNumber, Age, WorkshopId , CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, DeletedAt)
  VALUES ('cd2efa81-cef5-4fb8-b816-5bac9373b332', 'Михаил', 'Петрович', 'Александрович', '88005553535', 40, '90b8298b-2cb6-4437-9b54-6b54e877f4b5', 
  SYSDATETIMEOFFSET(), USER_NAME(), SYSDATETIMEOFFSET(), USER_NAME(), NULL);

  INSERT INTO Customer (Id, Name, Surname, Patronymic, PhoneNumber, Email , CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, DeletedAt)
  VALUES ('d3912b89-012e-4f6b-aa0d-0cc77a906b98', 'Алексей', 'Иванов', 'Анатольевич', '89815556591', 'alexey@gmail.com',
  SYSDATETIMEOFFSET(), USER_NAME(), SYSDATETIMEOFFSET(), USER_NAME(), NULL);

  INSERT INTO Material (Id, Name, Color, Sample, Amount, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, DeletedAt)
  VALUES ('5152bfdd-010e-4832-884a-024e6737901c', 'Хирургическая сталь', 'Тёмно-серый', 500, 2000,
  SYSDATETIMEOFFSET(), USER_NAME(), SYSDATETIMEOFFSET(), USER_NAME(), NULL);

  INSERT INTO Jewelry (Id, MaterialId, Name, Cost, Weight, Description, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, DeletedAt)
  VALUES ('c9bd1abf-6683-48a4-a947-b5a93e4b608e', '5152bfdd-010e-4832-884a-024e6737901c', 'Крестик Доминико Торетто', 600000, 200, 
  'Крестик для жесткого типа',
  SYSDATETIMEOFFSET(), USER_NAME(), SYSDATETIMEOFFSET(), USER_NAME(), NULL);

  INSERT INTO [Order] (Id, JewelryId, CustomerId, WorkshopId, Name, Description, OrderDate, DoneDate, CreatedAt, CreatedBy, UpdatedAt, UpdatedBy, DeletedAt)
  VALUES ('e502486e-5128-4534-8370-59a4dd4a6de1', 'c9bd1abf-6683-48a4-a947-b5a93e4b608e', 'd3912b89-012e-4f6b-aa0d-0cc77a906b98',
  '90b8298b-2cb6-4437-9b54-6b54e877f4b5', 'Срочный', 'Уважаемому клиенту', SYSDATETIMEOFFSET(), DATEADD(DAY, 5, SYSDATETIMEOFFSET()),
  SYSDATETIMEOFFSET(), USER_NAME(), SYSDATETIMEOFFSET(), USER_NAME(), NULL);
