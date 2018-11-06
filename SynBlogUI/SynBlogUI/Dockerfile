FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["*.csproj", "./"]
RUN dotnet restore

#Copy remaining files
COPY . .
RUN dotnet build -c Release -o /app
RUN dotnet publish -c Release -o /app

#Create image using runtime base image
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS final
WORKDIR /app
EXPOSE 80
ENV UserServiceUrl="http://localhost:7000"
ENV BlogServiceUrl="http://localhost:7001"
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SynBlogUI.dll"]