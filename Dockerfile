FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /src

# Copy the .csproj file first to restore dependencies
COPY chatApi.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

EXPOSE 80
EXPOSE 443
# You don't need to run dotnet watch here. It's done via docker-compose.
# CMD ["dotnet", "run", "--urls=http://+:80"]