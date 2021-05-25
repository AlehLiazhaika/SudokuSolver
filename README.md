# SudokuSolver
SudokuSolver is a Windows application for solving classic sudoku 9x9.

## Table of contents
* [Getting Started](#getting-started)
* [Contributing](#contributing)
* [Author](#author)

## Getting Started
There are two ways to run application:

### Download app
* Download needed archive from the App folder.
* Extract content.
* Run SodukuSolver.Desktop.exe.

### Build app

#### Prerequisites
If you want to build and run SudokuSolver from the sources you need to have [.NET5 SDK and Runtime(Desktop)](https://dotnet.microsoft.com/download/dotnet/5.0) on your computer.

#### Build (using Visual Studio)
* Open Solutions\\SudokuSolver.sln
* Choose target Configuration and Platform
* Build > Build Solution
* Run SudokuSolverBuilds\\{Configuration}\\{Platform}\\SudokuSolver.exe

#### Build (using MSBuild.exe)
In the command line:
```condole
cd <path to MSBuild.exe dir>
MSBuild.exe <path to SudokuSolver.sln> /p:Configuration=<Release/Debug> /p:Platform=<"x64"/"x86"/"Any CPU"> /p:OutputPath=<directory for build results (as default build will putted into ..\..\SudokuSolverBuild\{Configuration}\{Platform}\ related to sln file)>
```

## Contributing
Feedback and issues are welcome.

## Author
[Aleh Liazhaika](https://github.com/AlehLiazhaika)