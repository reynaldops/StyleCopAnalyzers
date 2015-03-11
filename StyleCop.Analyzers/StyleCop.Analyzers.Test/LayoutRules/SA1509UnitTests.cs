﻿using Microsoft.CodeAnalysis.CodeFixes;

namespace StyleCop.Analyzers.Test.LayoutRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using StyleCop.Analyzers.LayoutRules;
    using TestHelper;

    [TestClass]
    public class SA1509UnitTests : CodeFixVerifier
    {
        private string DiagnosticId = SA1509OpeningCurlyBracketsMustNotBePrecededByBlankLine.DiagnosticId;

        /// <summary>
        /// Verifies that the analyzer will properly handle an empty source.
        /// </summary>
        [TestMethod]
        public async Task TestEmptySource()
        {
            var testCode = string.Empty;
            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestClassDeclarationOpeningBraceHasBlankLine()
        {
            var testCode = @"
class Foo

{

}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 4, 1)
                            }
                    }
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
class Foo
{

}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        [TestMethod]
        public async Task TestClassDeclarationOpeningBraceHasThreeBlankLine()
        {
            var testCode = @"
class Foo



{

}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 6, 1)
                            }
                    }
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
class Foo
{

}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        [TestMethod]
        public async Task TestClassDeclarationOpeningBraceDoesntHaveBlankLine()
        {
            var testCode = @"
class Foo
{

}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestClassDeclarationCommentBeforeOpeningBrace()
        {
            var testCode = @"
class Foo
//this is a comment
{

}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestClassDeclarationMultilineCommentBeforeOpeningBrace()
        {
            var testCode = @"
class Foo
/*this is a comment
that spans 2 lines */
{

}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestClassDeclarationMultilineCommentBeforeOpeningBraceButBlankLineBetweenCommentsExists()
        {
            var testCode = @"
class Foo
/*this is a comment
that spans 2 lines */

//another comment
{

}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestClassDeclarationMultilineCommentBlankLineBeforeOpeningBrace()
        {
            var testCode = @"
class Foo
/*this is a comment
that spans 2 lines */
//another comment

{

}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 7, 1)
                            }
                    }
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
class Foo
/*this is a comment
that spans 2 lines */
//another comment
{

}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        [TestMethod]
        public async Task TestStructDeclarationOpeningBraceHasBlankLine()
        {
            var testCode = @"
struct Foo

{

}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 4, 1)
                            }
                    }
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
struct Foo
{

}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        [TestMethod]
        public async Task TestStructDeclarationOpeningBraceDoesntHaveBlankLine()
        {
            var testCode = @"
struct Foo
{

}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestMethodDeclarationOpeningBraceHasBlankLine()
        {
            var testCode = @"
class Foo
{
    void Bar()

    {
    }
}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 6, 5)
                            }
                    }
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
class Foo
{
    void Bar()
    {
    }
}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
    }

        [TestMethod]
        public async Task TestMethodDeclarationOpeningBraceHasTwoBlankLine()
        {
            var testCode = @"
class Foo
{
    void Bar()


    {
    }
}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 7, 5)
                            }
                    }
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
class Foo
{
    void Bar()
    {
    }
}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        [TestMethod]
        public async Task TestMethodDeclarationOpeningBraceDoesntHaveBlankLine()
        {
            var testCode = @"
class Foo
{
    void Bar()
    {
    }
}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestNamespaceDeclarationOpeningBraceHasBlankLine()
        {
            var testCode = @"
namespace Bar

{
    class Foo
    {
    }
}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 4, 1)
                            }
                    }
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
namespace Bar
{
    class Foo
    {
    }
}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        [TestMethod]
        public async Task TestNamespaceDeclarationOpeningBraceDoesntHaveBlankLine()
        {
            var testCode = @"
namespace Bar{
    class Foo
    {
    }
}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestPropertyDeclarationOpeningBraceHasBlankLine()
        {
            var testCode = @"
class Foo
{
    string Prop

    {
        get;set;}
}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 6, 5)
                            }
                    }
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
class Foo
{
    string Prop
    {
        get;set;}
}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        [TestMethod]
        public async Task TestPropertyDeclarationOpeningBraceDoesntHaveBlankLine()
        {
            var testCode = @"
class Foo
{
    string Prop { get;set; }
}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestIfStatementOpeningBraceHasBlankLine()
        {
            var testCode = @"
class Foo
{
    void Bar()
    {
        if(1 == 1)

        {}
        else
        

        {
        }
    }
}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 8, 9)
                            }
                    },
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 12, 9)
                            }
                    }
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
class Foo
{
    void Bar()
    {
        if(1 == 1)
        {}
        else
        {
        }
    }
}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        [TestMethod]
        public async Task TestIfStatementOpeningBraceDoesntHaveBlankLine()
        {
            var testCode = @"
class Foo
{
    void Bar()
    {
        if(1 == 1)
        {}
        else
        {
        }
    }
}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestWhileStatementOpeningBraceHasBlankLine()
        {
            var testCode = @"
class Foo
{
    void Bar()
    {
        while(1 == 1)
        
        {}
    }
}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 8, 9)
                            }
                    },
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
class Foo
{
    void Bar()
    {
        while(1 == 1)
        {}
    }
}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        [TestMethod]
        public async Task TestWhileStatementOpeningBraceDoesntHaveBlankLine()
        {
            var testCode = @"
class Foo
{
    void Bar()
    {
        while(1 == 1)
        {}
    }
}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestLambdaOpeningBraceHasBlankLine()
        {
            var testCode = @"
class Foo
{
    void Bar()
    {
        Action a = () =>  

{ };
    }
}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 8, 1)
                            }
                    },
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
class Foo
{
    void Bar()
    {
        Action a = () =>  
{ };
    }
}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        [TestMethod]
        public async Task TestLambdaOpeningBraceDoesntHaveBlankLine()
        {
            var testCode = @"
class Foo
{
    void Bar()
    {
        Action a = () =>  { };
    }
}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestArrayInitializationOpeningBraceHasBlankLine()
        {
            var testCode = @"
class Foo
{
    void Bar()
    {
        var a = new[]

{1, 2, 3};
    }
}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 8, 1)
                            }
                    },
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
class Foo
{
    void Bar()
    {
        var a = new[]
{1, 2, 3};
    }
}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        [TestMethod]
        public async Task TestArrayInitializationOpeningBraceDoesntHaveBlankLine()
        {
            var testCode = @"
class Foo
{
    void Bar()
    {
        var a = new[] {1, 2, 3};
    }
}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestPropertyInitializerOpeningBraceHasBlankLine()
        {
            var testCode = @"
class Person
{
    internal string Name {get;set;}
}

class Foo
{
    void Bar()
    {
        var p = new Person()

        { Name = ""qwe""};
    }
}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 13, 9)
                            }
                    },
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
class Person
{
    internal string Name {get;set;}
}

class Foo
{
    void Bar()
    {
        var p = new Person()
        { Name = ""qwe""};
    }
}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        [TestMethod]
        public async Task TestPropertyInitializerOpeningBraceDoesntHaveBlankLine()
        {
            var testCode = @"
class Person
{
    internal string Name {get;set;}
}

class Foo
{
    void Bar()
    {
        var p = new Person()
        { 
            Name = ""qwe""
        };
    }
}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestAnonymousTypeOpeningBraceHasBlankLine()
        {
            var testCode = @"
class Foo
{
    void Bar()
    {
        var p = new 

        { Name = ""qwe""};
    }
}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 8, 9)
                            }
                    },
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"
class Foo
{
    void Bar()
    {
        var p = new 
        { Name = ""qwe""};
    }
}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        [TestMethod]
        public async Task TestAnonymousTypeOpeningBraceDoesntHaveBlankLine()
        {
            var testCode = @"
class Person
{
    internal string Name {get;set;}
}

class Foo
{
    void Bar()
    {
        var p = new { Name = ""qwe""};
    }
}";

            await this.VerifyCSharpDiagnosticAsync(testCode, EmptyDiagnosticResults, CancellationToken.None);
        }

        [TestMethod]
        public async Task TestComplex1()
        {
            var testCode = @"namespace Test

    {
    class Person
    
    {
        internal string Name {get;set;}
    }

    class Foo  
    {
        void Bar()
        

        {
            var a = new
//this is a comment 

{Age = 5};

            var b = new {Week = 5};
        }
    }
}";

            var expected = new[]
                {
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 3, 5)
                            }
                    },
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 6, 5)
                            }
                    },
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 15, 9)
                            }
                    },
                    new DiagnosticResult
                    {
                        Id = this.DiagnosticId,
                        Message = "Opening curly brackets must not be preceded by blank line.",
                        Severity = DiagnosticSeverity.Warning,
                        Locations =
                            new[]
                            {
                                new DiagnosticResultLocation("Test0.cs", 19, 1)
                            }
                    },
                };

            await this.VerifyCSharpDiagnosticAsync(testCode, expected, CancellationToken.None);

            var fixedCode = @"namespace Test
    {
    class Person
    {
        internal string Name {get;set;}
    }

    class Foo  
    {
        void Bar()
        {
            var a = new
//this is a comment 
{Age = 5};

            var b = new {Week = 5};
        }
    }
}";
            await this.VerifyCSharpFixAsync(testCode, fixedCode);
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new SA1509OpeningCurlyBracketsMustNotBePrecededByBlankLine();
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new SA1509CodeFixProvider();
        }
    }
}