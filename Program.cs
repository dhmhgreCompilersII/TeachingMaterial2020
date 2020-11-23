using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace TeachingMaterial {
    class Program {
        static void Main(string[] args) {

            StreamReader aStream = new StreamReader(args[0]);
            AntlrInputStream antlrInputStream = new AntlrInputStream(aStream);
            SimpleCalcLexer lexer = new SimpleCalcLexer(antlrInputStream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            SimpleCalcParser parser = new SimpleCalcParser(tokens);
            IParseTree tree = parser.compileUnit();
            Console.WriteLine(tree.ToStringTree());

            STPrinter stPrinter = new STPrinter();
            stPrinter.Visit(tree);

            ASTGenerator astgen =new ASTGenerator();
            astgen.Visit(tree);

            ASTPrinterVisitor astPrinter = new ASTPrinterVisitor();
            astPrinter.Visit(astgen.M_Root);
        }
    }
}
