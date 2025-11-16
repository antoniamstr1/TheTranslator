FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /TheTranslator

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /TheTranslator

ENV SUPABASE_URL="https://maccsscwpdiodplyiwxj.supabase.co"
ENV SUPABASE_KEY="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1hY2Nzc2N3cGRpb2RwbHlpd3hqIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NjMxMDczNjQsImV4cCI6MjA3ODY4MzM2NH0.JtwT4SJZx65nB0HCV2gBzJ9kwc5NTHRJ2P203nS7Z5E"
ENV VERB_API_URL="https://conjugationsapi.onrender.com"
ENV WORD_API_URL="https://freedictionaryapi.com/api/v1/entries/"


COPY --from=build /TheTranslator/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "TheTranslator.dll"]
