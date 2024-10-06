# CSV Processing Console Application

## Overview
This is a C# console application developed for a job application task. The application reads a CSV file, processes the data, and writes the results to a new file. The data from the CSV is displayed in the console with specific formatting requirements and then converted to hexadecimal format. The processed data is also saved into a new CSV file under a different name.

The project was built with a focus on **code readability** and **expandability**, aiming to showcase a clear understanding of file processing logic without relying on external libraries like **CsvHelper**.

## Features
- Reads a CSV file with columns: `Ime`, `Prezime`, and `DatumRodjenja`.
- Allows the user to input the file path and ensures the file exists.
- Displays the data in the console with:
  - Birth date format as `DD-MM-YYYY`
  - Uppercase name and surname
- Converts the displayed data to hexadecimal and outputs it to the console.
- Saves the modified data into a new CSV file.
- Configurable via an external JSON configuration file (`appsettings.json`), supporting:
  - CSV separator character
  - Whether the target file contains a header
  - Date format for birth dates
  - Appendix for the modified file name

## Technologies Used
- **C#**
- **.NET 6.0**
- **Microsoft.Extensions.Configuration** for loading configurations from a JSON file.

## How to Run
### 1. Clone the repository:

```bash
git clone https://github.com/lukamrko/AKD-selekcijski-zadatak.git
```

### 2. Navigate to the Project Directory
Once the repository is cloned, navigate into the project directory:

```bash
cd AKD-selekcijski-zadatak
```

### 3. Ensure appsettings.json is Correctly Configured
The appsettings.json file contains important configurations for the application. Ensure the file is present in the project root and contains the following structure:

```json
{
    "CsvSeparator": ",",
    "TargetFileContainsHeader": true,
    "BirthDateFormat": "dd-MM-yyyy",
    "ModifiedFileAppendix": "_modified"
}
```

### 4. Build the Application
Run the following command to build the application:

```bash
dotnet build
```

### 5. Run the Application
After successfully building, run the application using the following command:

```bash
dotnet run
```

### 6. Provide the CSV File Path
The application will prompt you to input the path to a CSV file. Make sure the file you provide adheres to the following structure:

```
Ime,Prezime,DatumRodjenja
John,Doe,15/03/1990
Jane,Smith,22/08/1985
```

The CSV file should contain the columns: Ime, Prezime, and DatumRodjenja.

### 7. Check the Output
The application will display the processed data in the console. The names and surnames will be converted to uppercase, and birth dates will be formatted as DD-MM-YYYY. The data will also be converted to hexadecimal format and displayed alongside the regular data.

### 8. Check the Modified CSV File
The application will save the processed data into a new CSV file, appending the suffix _modified to the original file name (or as specified in appsettings.json). You will see the file saved at the specified location.
