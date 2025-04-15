
# 📚 Laboration 3 – EF Core (ASP.NET Web API)

Detta är mitt projekt för **Laboration 3** i kursen *Fördjupning Programmering*, där jag bygger ett ASP.NET Web API med Entity Framework Core och SQL Server LocalDB.

Projektstruktur

- `PubAPI` – API-projektet med alla endpoints
- `PublisherData` – Datakontext och EF-migrationer
- `PublisherDomain` – Domänmodeller och validering

Funktionalitet

- CRUD-endpoints för `/api/book`
- Asynkrona metoder med `async/await`
- Relationshantering mellan `Book` och `Author`
- `Include()` används för att returnera relaterade objekt
- `AsNoTracking()` används i GET för bättre prestanda
- Validering av inkommande data
- Felhantering med rätt HTTP-status
- Swagger-dokumentation med XML-kommentarer
- Cykliska referenser hanteras med `ReferenceHandler.IgnoreCycles`

### Extra funktionalitet
POST-endpointen för `/api/book` returnerar direkt det skapade `Book`-objektet inklusive dess `Author`, för ett smidigare klientflöde.

## Teknologier

- ASP.NET Core 8
- Entity Framework Core
- SQL Server LocalDB
- Swagger / OpenAPI
- .NET CLI (`dotnet`)



## 📝 Övrigt

Frågorna till laborationen finns i filen `LAB 3 - FRÅGOR.txt`.
