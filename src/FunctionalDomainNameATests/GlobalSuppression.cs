using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    category: "Compiler",
    checkId: "CS0051:Inconsistent accessibility: parameter type is less accessible than method",
    Justification = "Test projects use the internal classes of functional domains, but XUinit test methods are public by design. The error is therefore irrelevant"
)]
