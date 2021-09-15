# Noodle Classroom API

> Work in progress. (No releases yet)

## On this file
- [About the Project](#about-the-project)
- [Repo Structure](#repo-structure)
- [Software Requirements](#software-requirements)
- [API Dependencies](#api-dependencies)
- [Other Information](#other-information)

## About the Project
The Noodle Classroom App is intended to be a very simplified mimic of Google Classroom, allowing both professors and students to create, join and follow courses through groups. Each course has a collection of publications and a collection of participants. This repository contains the web API, which serves the resources to the application client through REST endpoints.

The main features of this project are:
- Join or create classes.
- View course publications, assignments and announcements.
- Receive feedback for each assignment turned in.

## Repo Structure
This project contains two main directories: `NoodleApi/`, which contains the files for the web API and `Tests/`, the directory for the api unit tests.

```
noodle_classroom_api/
├── README.md
├── .gitignore
├── noodle_classroom_api.sln
└── NoodleApi/
	├── NoodleApi.csproj
	├── appsettings.json
	├── appsettings.Development.json
	├── Program.cs
	├── Startup.cs
	└── Controllers/
		└── CourseController.cs
	└── Data/
		├── CourseContext.cs
		├── CourseRepository.cs
		└── ICourseService.cs
	└── Docs/
		├── controller_docs.xml
		├── data_docs.xml
		└── model_docs.xml
	└── Models/
		├── Course.cs
		├── CourseDTO.cs
		├── CourseSimplifiedDTO.cs
		├── Extensions.cs
		├── Publication.cs
		└── PublicationDTO.cs
	└── Properties/
		└── launchSettings.json
└── Tests/
	├── Tests.csproj
	├── Utils.cs
	├── CourseControllerTests.cs
	└── docs/
		├── tests_doc.xml
		└── tests_util_doc.xml
```

## Software requirements
- .NET SDK (5.0.303)
- Git

## API Dependencies
- [Microsoft.EntityFrameworkCore.Sqlite (5.0.3)](https://www.nuget.org/packages/Microsoft.Data.Sqlite.Core/5.0.3)
- [Microsoft.EntityFrameworkCore.SqlServer (5.0.3)](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/5.0.3)
- [Microsoft.EntityFrameworkCore.Tools (5.0.3)](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/5.0.3)
- [Microsoft.Extensions.Logging.AzureAppServices (5.0.3)](https://www.nuget.org/packages/Microsoft.Extensions.Logging.AzureAppServices/5.0.3)
- [Swashbuckle.AspNetCore (5.6.3)](https://www.nuget.org/packages/Swashbuckle.AspNetCore/5.6.3)

### Test Dependencies
- [Microsoft.NET.Test.Sdk (16.9.4)](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk/16.9.4)
- [Moq (4.16.1)](https://www.nuget.org/packages/Moq/4.16.1)
- [xunit (2.4.1)](https://www.nuget.org/packages/xunit/2.4.1)
- [xunit.runner.visualstudio (2.4.3)](https://www.nuget.org/packages/xunit.runner.visualstudio/2.4.3)
- [coverlet.collector (3.0.2)](https://www.nuget.org/packages/coverlet.collector/3.0.2)

## Other information
This project is still work in progress.