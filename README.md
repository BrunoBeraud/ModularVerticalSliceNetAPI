# .NET Template: ModularVerticalSliceNetAPI

This repository contains a custom .NET template that allows you to create a project with configurable parameters. The template includes options to define component names and functional domain names, with automatic transformation of domain names to lowercase.

## Features

- **ComponentName**: The name of the component you are creating.
- **FunctionalDomainNameA**: The first functional domain name (to be converted to lowercase).
- **FunctionalDomainNameB**: The second functional domain name (to be converted to lowercase).
- **FunctionalDomainNameALowerCase**: Automatically generated lowercase version of `FunctionalDomainNameA`.
- **FunctionalDomainNameBLowerCase**: Automatically generated lowercase version of `FunctionalDomainNameB`.

## Prerequisites

- .NET 6.0 or later installed on your machine.
- .NET CLI for creating projects from templates.

## Installation

To install this template locally, follow these steps:

1. **Clone this repository** or download the `.zip` of the template.
   
   Clone via Git:
   ```bash
   git clone https://github.com/your-username/your-template-repo.git
   ```

2. **Install the template** using the .NET CLI:
   
   Navigate to the root folder of the cloned repository and run the following command:
   ```bash
   dotnet new --install ./path/to/your-template-repo
   ```

## Usage

Once the template is installed, you can create a new project using the template by following these steps:

1. Open a terminal or command prompt.

2. Navigate to the directory where you want to create your new project.

3. Run the following command to create a new project from the template:
   
   ```bash
   dotnet new modular.verticalSlice.api --ComponentName <ComponentName> --FunctionalDomainNameA <FunctionalDomainA> --FunctionalDomainNameB <FunctionalDomainB>
   ```

   Replace `<ComponentName>`, `<FunctionalDomainA>`, and `<FunctionalDomainB>` with your desired values.

   Example:
   ```bash
   dotnet new modular.verticalSlice.api --ComponentName ECommerce --FunctionalDomainNameA Customers --FunctionalDomainNameB Orders
   ```
## Example

If you use the following parameters:

- **FunctionalDomainNameA** = `Customers`
- **FunctionalDomainNameB** = `Orders`

The template will generate the following:

- **FunctionalDomainNameALowerCase** = `customers`
- **FunctionalDomainNameBLowerCase** = `orders`

These values will be substituted into the appropriate places in your template.

## Customizing the Template

You can customize the template further by modifying the `.template.config/template.json` file. You can add more parameters, change default values, and configure transformations as needed.

## Troubleshooting

- If you encounter issues installing or using the template, ensure that your .NET CLI is up to date.
- Double-check the parameters you're passing to ensure they're correct.
- If you're using a custom template path, make sure you're pointing to the correct directory when installing or creating the project.

## License

This template is licensed under the MIT License. See the [LICENSE](https://github.com/BrunoBeraud/ModularVerticalSliceNetAPI/blob/main/LICENSE) file for more details.

---

Created by [Your Name](https://github.com/BrunoBeraud).
