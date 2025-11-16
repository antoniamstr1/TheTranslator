# TheTranslator

A C# .NET backend application for analyzing words and conjugating verbs in spanish and italian to english. This project uses Supabase for data storage and integrates with external APIs for word analysis and verb conjugation.

---

## Features

- Retrieve texts and words for a user from Supabase.
- Analyze words: detect type, definitions, and forms.
- Conjugate verbs for Italian (`it`) and Spanish (`es`).
- Simple REST API with endpoints for texts, words, and word analysis.


---

## Technologies Used

- [.NET 8 Minimal API / ASP.NET Core](https://dotnet.microsoft.com/)
- [Supabase](https://supabase.com/) for database storage
- JSON processing
- Third-party APIs [https://freedictionaryapi.com/](https://freedictionaryapi.com/)
- Custom [Python Flask Api for verb conjugations](https://github.com/antoniamstr1/ConjugationsAPI)

---

## Getting Started

### Prerequisites

- [.NET SDK 8](https://dotnet.microsoft.com/download)


### Installation

1. Clone the repository:

```bash
git clone https://github.com/yourusername/TheTranslator.git
cd TheTranslator
dotnet restore
dotnet run
```

The API will run locall on https://localhost:5187