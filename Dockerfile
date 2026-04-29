FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copy everything and restore in one layer for simplicity
COPY . ./
RUN dotnet restore "Practica2.csproj"

# Publish the app
RUN dotnet publish "Practica2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS runtime
WORKDIR /app

# Listen on the port provided by the environment (Render sets $PORT)
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080}
EXPOSE 8080

COPY --from=build /app/publish ./

# Use shell form so ${PORT} expansion works at runtime
ENTRYPOINT ["sh", "-c", "dotnet Practica2.dll --urls http://0.0.0.0:${PORT:-8080}"]