FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7126
ENV ASPNETCORE_URLS=http://+:7126

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .

FROM build AS publish
RUN dotnet publish "TenisKlub/TenisKlub.csproj" -c Release -o /app
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY TenisKlub/ImageBase64Files /app/ImageBase64Files
ENTRYPOINT ["dotnet", "TenisKlub.dll"]