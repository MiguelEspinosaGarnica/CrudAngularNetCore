# Proyecto Angular con ASP.NET Core

Este es un proyecto de aplicación web que utiliza Angular en el frontend y ASP.NET Core en el backend. La plantilla de Angular con ASP.NET Core permite una integración fluida entre ambas tecnologías para crear aplicaciones web modernas, eficientes y escalables.

## Características

- **Frontend**: 17.3.0 (o la versión que corresponda) con Material Design.
- **Backend**: ASP.NET Core 8 (o superior).
- **Comunicación**: API RESTful para la interacción entre el frontend y backend.
- **Enrutamiento**: Enrutamiento SPA (Single Page Application) en Angular y servidor.

## Requisitos Previos

Antes de comenzar, asegúrate de tener instalado lo siguiente:

- **[Node.js](https://nodejs.org/)**: Version 14.x o superior.
- **[Angular CLI](https://angular.io/cli)**: Para la gestión de Angular.
  ```bash
  npm install -g @angular/cli

Paquetes nuget: 
dotnet add package Newtonsoft.Json

Pasos para ejecucion:

Clonar repositorio: git clone https://github.com/usuario/nombre-repositorio.git
Acceder a la carpeta: cd nombre-repositorio
Restaurar paquetes net core: dotnet restore
Restaurar paquetes frontend (Aplica si se desea ejecutar el frontend por aparte): cd ClientApp --> npm install
Compilar proyecto back: VehiclesApp.Server
Ejecutar proyecto back (Arrancara automaticamente el frontend)


