# 🎨 Gestión de Obras de Arte

> **Sistema integral para la administración y registro de obras de arte, artistas y tipos de pintura**, desarrollado con una arquitectura modular en .NET 9.

---

## 🧱 Arquitectura del Proyecto

La captura de pantalla muestra la arquitectura de la solución del proyecto **"Gestión de Obras de Arte"** en Visual Studio.  
Se observa una estructura de solución (`.sln`) que contiene **cinco proyectos principales**, demostrando un enfoque de **separación de responsabilidades (SoC)** y una **arquitectura orientada a servicios**:

- **`GestionObrasArte.API`**  
  Proyecto **Backend (API REST)** desarrollado en **.NET 9**.  
  Actúa como el servidor central, manejando la lógica de negocio y la comunicación con la base de datos.

- **`GestionObrasArte.BlazorApp`**  
  Proyecto **Frontend (Cliente Web)** basado en **Blazor Server**, diseñado para la gestión interactiva de la entidad **Artistas**.

- **`GestionObrasArte.ConsoleApp`**  
  Proyecto **Cliente de Consola**, enfocado en las operaciones **CRUD** de la entidad **Tipos de Pintura**.

- **`GestionObrasArte.MauiApp`**  
  Proyecto **Cliente Multiplataforma (.NET MAUI)**, orientado a la gestión de **Pinturas** para **dispositivos móviles y de escritorio**.

- **`GestionObrasArte.Shared`**  
  Biblioteca de clases **compartida**, que contiene los **modelos de datos (DTOs/Entidades)** utilizados por la API y los proyectos cliente, garantizando la **consistencia de datos** en toda la solución.

---

## 🧩 Tecnologías Utilizadas

| Capa | Tecnología / Framework | Descripción |
|------|-------------------------|-------------|
| Backend | **.NET 9 Web API** | Lógica de negocio y endpoints REST |
| Frontend Web | **Blazor Server** | Interfaz interactiva para gestión de artistas |
| Frontend Móvil / Escritorio | **.NET MAUI** | Aplicación multiplataforma para gestión de pinturas |
| Cliente CLI | **.NET Console App** | Operaciones CRUD desde línea de comandos |
| Base de Datos | **PostgreSQL** | Almacenamiento principal de datos |
| Compartido | **C# DTOs / Models** | Modelos compartidos entre los proyectos |

---

## ⚙️ Estructura de Carpetas

```bash
GestionObrasArte/
│
├── GestionObrasArte.API/          # API REST (.NET 9)
├── GestionObrasArte.BlazorApp/    # Interfaz web con Blazor Server
├── GestionObrasArte.ConsoleApp/   # Cliente de consola
├── GestionObrasArte.MauiApp/      # App móvil y escritorio (MAUI)
└── GestionObrasArte.Shared/       # Entidades y DTOs compartidos
