# TheTranslator_BACKEND

A C# .NET backend application for analyzing words and conjugating verbs in spanish and italian to english. This project uses Supabase for data storage and integrates with external APIs for word analysis and verb conjugation.

---

## Features

- Retrieve texts and words for a user from Supabase.
- Analyze words: detect type, definitions, and forms.
- Conjugate verbs for Italian (`it`) and Spanish (`es`).
- Simple REST API with endpoints for texts, words, and word analysis.


---

## Technologies Used

- [.NET 10 API / ASP.NET Core](https://dotnet.microsoft.com/)
- [Supabase](https://supabase.com/) for database storage
- JSON processing
- Third-party APIs [https://freedictionaryapi.com/](https://freedictionaryapi.com/)
- Custom [Python Flask Api for verb conjugations](https://github.com/antoniamstr1/ConjugationsAPI)

---

## Getting Started

### Prerequisites

- [.NET SDK 10](https://dotnet.microsoft.com/download)


### Installation

1. Clone the repository:

```bash
git clone https://github.com/antoniamstr1/TheTranslator.git
cd TheTranslator
dotnet restore
dotnet run
```

The API will run locall on https://localhost:5187


# TheTranslator_FRONTEND

A React-based frontend application that allows users to input any text and receive detailed linguistic analysis for each word. The app integrates with a .NET backend API and an additional Python (Flask) microservice.

---

## Features
- Interactive Text Input
- Automatic Word Extraction
- Word-by-Word Analysis
- Translation and Verb conjugation 
- Expandable Word Details
- Language Selection
- API Integration

---

## Technologies Used

- [React](https://react.dev/)
- [Vite](https://vitejs.dev/)
- [MUI Components](https://mui.com/)
- [TypeScript](https://www.typescriptlang.org/)

## Getting Started

### Prerequisites

- [Node.js (LTS)](https://nodejs.org/)
- [npm](https://www.npmjs.com/) or [yarn](https://yarnpkg.com/)  

### Installation

1. Clone the repository:

```bash
git clone https://github.com/antoniamstr1/TheTranslator.git
cd TheTranslator
npm install
npm run dev

The API will run locall on https://localhost:5173
