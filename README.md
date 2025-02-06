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
  - **Unit Tests:** Focus on unhappy paths (e.g., validating failure scenarios in services and use cases) and domain model business rules validation.
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

# Going Further with the Template Implementation

This template is intentionally designed to be simple and non-opinionated, allowing you to choose your preferred technologies and libraries (e.g., Entity Framework, Mass Transit, databases, message brokers, etc.). The basic use cases demonstrated here leverage an `InMemoryRepository` for this reason. However, as your application grows in complexity, you'll need to adopt more sophisticated patterns and strategies. This section outlines key recommendations for taking your implementation further.

## 1. Grouping Ports by Feature

As the number of use cases and ports in your application increases, it can become difficult to maintain a clear structure. To manage this complexity:

- **Group ports by feature**: When your application starts having many ports/interfaces, organizing them by feature will keep your codebase maintainable and coherent. All ports used by a feature will be regroup into this folder.

## 2. Using Integration Events for Asynchronous Communication

As your application grows and different modules (*bounded context*) need to communicate, synchronous communication can lead to tight coupling. Consider switching to **asynchronous communication** using integration events.

- **Define integration events**: Use integration events to enable asynchronous communication between modules, ensuring that they remain decoupled. For example, after a change occurs in one module, you can publish an integration event to notify other modules.

### Publishing Integration Events on Save Changes

To ensure that integration events are consistently published whenever there are changes to your domain, you can take advantage of **EF Interceptors** combined with **Mass Transit** for event publishing.

- **EF Interceptors**: These can be used to hook into the `SaveChanges` process of Entity Framework, ensuring that integration events are published when changes to your root aggregate are saved. This provides an automated way of broadcasting changes to other modules in your system.
- **Mass Transit**: Use Mass Transit to handle the messaging aspect of these integration events. With Mass Transit, you can publish events to a message broker (e.g., RabbitMQ, Kafka) for delivery to other services in the system.

### Using a Root Aggregate for Event Creation

When you are working with complex domain models, it is beneficial to have a **root aggregate** that manages the creation of domain events, including integration events.

- **Root aggregate**: The root aggregate is responsible for ensuring that domain logic is enforced and that events are published in a consistent and reliable manner. By centralizing event creation within the root aggregate, you can ensure that your system behaves as expected and that events are only triggered when necessary.

### 3 Exposing Public Use Case Interfaces for Synchronous Communication

While asynchronous communication is essential for decoupling, certain use cases may still require **synchronous communication** for efficiency or simplicity.

- **Public use case interfaces**: Expose public ports for those use cases that need synchronous communication. For example, querying for specific data. These interfaces should be designed as
if you were exposing this feature through HTTP REST Api endpoint.

By following these guidelines, you can evolve your application from simple to complex while maintaining flexibility and scalability.

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
   git clone https://github.com/BrunoBeraud/ModularVerticalSliceNetAPI.git
   ```

2. **Install the template** using the .NET CLI:
   
   Navigate to the root folder of the cloned repository and run the following command:
   ```bash
   dotnet new install .
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