using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Tree;

namespace TeachingMaterial {
    public class STPrinter :SimpleCalcParserBaseVisitor<int> {
        StreamWriter m_STSpecFile = new StreamWriter("test.dot");
        Stack<string> m_parentsLabel = new Stack<string>();
        private static int ms_serialCounter = 0;

        public override int VisitExprAssignment(SimpleCalcParser.ExprAssignmentContext context) {
            string label = "Assignment" + "_" + ms_serialCounter++;
            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
            m_parentsLabel.Push(label);
            base.VisitExprAssignment(context);
            m_parentsLabel.Pop();
            return 0;
        }

        public override int VisitExprParenthesis(SimpleCalcParser.ExprParenthesisContext context) {
            return base.VisitExprParenthesis(context);
        }

        public override int VisitExprMulDiv(SimpleCalcParser.ExprMulDivContext context) {
            string label = "";
            switch (context.op.Type) {
                case SimpleCalcLexer.MULT:
                    label = "Multiplication" + "_" + ms_serialCounter++;
                    break;
                case SimpleCalcLexer.DIV:
                    label = "Division" + "_" + ms_serialCounter++;
                    break;
            }
            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
            m_parentsLabel.Push(label);
            base.VisitExprMulDiv(context);
            m_parentsLabel.Pop();
            return 0;
        }

        public override int VisitExprAddSub(SimpleCalcParser.ExprAddSubContext context) {
            string label = "";
            switch (context.op.Type) {
                case SimpleCalcLexer.PLUS:
                    label = "Addition" + "_" + ms_serialCounter++;
                    break;
                case SimpleCalcLexer.MINUS:
                    label = "Subtraction" + "_" + ms_serialCounter++;
                    break;
            }
            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
            m_parentsLabel.Push(label);
            base.VisitExprAddSub(context);
            m_parentsLabel.Pop();
            return 0;
        }

        public override int VisitCompileUnit(SimpleCalcParser.CompileUnitContext context) {
            string label = "CompileUnit" + "_" + ms_serialCounter++;
            m_STSpecFile.WriteLine("digraph G{");
            m_parentsLabel.Push(label);
            base.VisitCompileUnit(context);
            m_parentsLabel.Pop();
            m_STSpecFile.WriteLine("}");
            m_STSpecFile.Close();

            // Prepare the process dot to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-Tgif " +
                              Path.GetFileName("test.dot") + " -o " +
                              Path.GetFileNameWithoutExtension("test") + ".gif";
            // Enter the executable to run, including the complete path
            start.FileName = "dot";
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start)) {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }

            return 0;
        }

        public override int VisitTerminal(ITerminalNode node) {
            string label;
            switch (node.Symbol.Type) {
                case SimpleCalcLexer.NUMBER:
                label = "NUMBER" + "_" +node.Symbol.Text +"_" + ms_serialCounter++;
                m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
                break;
                case SimpleCalcLexer.VARIABLE:
                label = "VARIABLE" + "_" + node.Symbol.Text + "_" + ms_serialCounter++;
                m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
                break;
            }
            return base.VisitTerminal(node);
        }
    }
}
