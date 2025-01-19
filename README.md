# .NET Template: ModularVerticalSliceNetAPI

## Why Use This Template

This .NET architecture template is specifically designed to address critical software development and maintenance challenges. It provides a structured, modernized approach to streamline development, testing, and compliance with architectural best practices. Below are the key benefits:

### 1. Facilitate the Effort Required to Meet Customer Needs
- **Vertical Slicing Pattern:**  
  This template organizes code by features (end-to-end functional requirements) rather than technical layers. This approach:  
  - Maximizes cohesion by grouping code that fulfills the same functional requirement.  
  - Minimizes coupling, reducing dependencies between unrelated code.  
- **Simplified Code Navigation:**  
  Developers can quickly locate and understand code, which reduces onboarding time and increases productivity.  
- **Reduced Risk of Regression:**  
  Modifying one functional requirement is less likely to introduce bugs in other areas.  
- **Lower Need for Abstraction:**  
  With minimized coupling and maximized cohesion, fewer abstractions are needed, as dependencies are reduced to only those related to specific functional requirements.

### 2. Facilitate Future Application Maintenance
- **Domain-Driven Division:**  
  The application is divided into functional domains, and each domain is further divided into features. This logical organization is independent of underlying technologies, ensuring long-term maintainability.  
- **Resilience to Team Turnover:**  
  Even in cases where teams lose application history, reverse engineering efforts are significantly reduced due to the natural and intuitive architecture.

### 3. Make It Easier to Decide on Distributed System Granularity
- **Modular Monolith Pattern:**  
  This template combines the simplicity of monolithic architectures with the flexibility of microservices:  
  - Functional domains (modules) can be easily separated or combined into distributed components.  
  - This segmentation provides a clean and maintainable transition path for future scaling decisions.  

### 4. Facilitate Automated Testing
- **Clear Testing Guidance:**  
  The template simplifies decisions on what and how to test by providing clear guidelines for:  
  - **Functional Tests:** Cover happy paths for all application entry points (e.g., HTTP, AMQP).  
  - **Unit Tests:** Focus on unhappy paths (e.g., validating failure scenarios in services and use cases).  
Use tools like `TestContainer` and `.NET WebApplicationFactory` to verify end-to-end behavior.  

### 5. Facilitate Compliance with Architecture Rules
- **Automated Architecture Testing:**  
  The template provides tools to verify compliance with best practices and standards, including:  
  - Dependency management.  
  - Functional isolation of domains.  
  - Proper naming conventions.  
  - Immutability of DTOs.  
  - ...

---

This architecture template simplifies development, enhances maintainability, and ensures long-term scalability while keeping a focus on business needs and compliance. It’s a robust starting point for building clean, maintainable, and testable .NET applications.

## How use it

### Features

- **ComponentName**: The name of the component you are creating.
- **FunctionalDomainNameA**: The first functional domain name (to be converted to lowercase).
- **FunctionalDomainNameB**: The second functional domain name (to be converted to lowercase).
- **FunctionalDomainNameALowerCase**: Automatically generated lowercase version of `FunctionalDomainNameA`.
- **FunctionalDomainNameBLowerCase**: Automatically generated lowercase version of `FunctionalDomainNameB`.

### Prerequisites

- .NET 6.0 or later installed on your machine.
- .NET CLI for creating projects from templates.

### Installation

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

### Usage

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
### Example

If you use the following parameters:

- **FunctionalDomainNameA** = `Customers`
- **FunctionalDomainNameB** = `Orders`

The template will generate the following:

- **FunctionalDomainNameALowerCase** = `customers`
- **FunctionalDomainNameBLowerCase** = `orders`

These values will be substituted into the appropriate places in your template.

### Customizing the Template

You can customize the template further by modifying the `.template.config/template.json` file. You can add more parameters, change default values, and configure transformations as needed.

### Troubleshooting

- If you encounter issues installing or using the template, ensure that your .NET CLI is up to date.
- Double-check the parameters you're passing to ensure they're correct.
- If you're using a custom template path, make sure you're pointing to the correct directory when installing or creating the project.

## License

This template is licensed under the MIT License. See the [LICENSE](https://github.com/BrunoBeraud/ModularVerticalSliceNetAPI/blob/main/LICENSE) file for more details.