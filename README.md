Предметная область: Автоматизация процесса заказа ювелирных изделий

Иванов Алексей Анатольевич ИП-20-3

Схема взаимодействия сущностей:

erDiagram

    Craftsman {
        Guid Id
        string Name
        string Surname
        string Patronymic
        string Phone
        int Age
        Guid WorkshopId
    }
    
    Jewelry {
        Guid Id
        string Name
        int Cost
        int Weight
        Guid MaterialId
        string Description
    }
    
    Material {
        Guid Id
        string Name
        string Sample
        string Color
    }
    
    Customer {
        Guid Id
        string Name
        string Surname
        string Patronymic
        string Email
        string Phone
    }

    Order {
        Guid Id
        string Name
        string Description
        DateTime OrderDate
        DateTime DoneDate
        Guid JewelryId
        Guid CustomerId
        Guid WorkshopId
    }

     Workshop {
        Guid Id
        string Name
        string Address
        int Workplaces
    }

  BaseAuditEntity {
        Guid ID
        DateTimeOffset CreatedAt
        string CreatedBy
        DateTimeOffset UpdatedAt
        string UpdatedBy
        DateTimeOffset DeleteddAt
  }

    Material ||--o{ Jewelry: is
    Workshop ||--o{ Craftsman: is
    Customer ||--o{ Order: is
    Workshop ||--o{ Order: is
    Jewelry ||--o{ Order: is

