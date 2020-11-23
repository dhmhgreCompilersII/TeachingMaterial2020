using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Tree;

namespace TeachingMaterial {
    public class ASTGenerator : SimpleCalcParserBaseVisitor<int> {
        private CCompileUnit m_root;
        Stack<ValueTuple<SimpleCalcASTElement,int>> m_parents = 
            new Stack<(SimpleCalcASTElement, int)>();

        public CCompileUnit M_Root {
            get => m_root;
        }

        public override int VisitExprAssignment(SimpleCalcParser.ExprAssignmentContext context) {
            CAssignment newNode = new CAssignment();
            ValueTuple<SimpleCalcASTElement,int> parent= m_parents.Peek();
            parent.Item1.AddChild(newNode,parent.Item2);

            m_parents.Push((newNode,CAssignment.CT_LEFT));
            Visit(context.VARIABLE());
            m_parents.Pop();

            m_parents.Push((newNode, CAssignment.CT_RIGHT));
            Visit(context.expr());
            m_parents.Pop();

            return 0;
        }

        public override int VisitExprMulDiv(SimpleCalcParser.ExprMulDivContext context) {

            switch (context.op.Type) {
                case SimpleCalcLexer.MULT:
                    CMultiplication newNode = new CMultiplication();
                    ValueTuple<SimpleCalcASTElement, int> parent = m_parents.Peek();
                    parent.Item1.AddChild(newNode, parent.Item2);

                    m_parents.Push((newNode, CMultiplication.CT_LEFT));
                    Visit(context.expr(0));
                    m_parents.Pop();

                    m_parents.Push((newNode, CMultiplication.CT_RIGHT));
                    Visit(context.expr(1));
                    m_parents.Pop();
                    break;
                case SimpleCalcLexer.DIV:
                    CDivision newNode1 = new CDivision();
                    ValueTuple<SimpleCalcASTElement, int> parent1 = m_parents.Peek();
                    parent1.Item1.AddChild(newNode1, parent1.Item2);

                    m_parents.Push((newNode1, CDivision.CT_LEFT));
                    Visit(context.expr(0));
                    m_parents.Pop();

                    m_parents.Push((newNode1, CDivision.CT_RIGHT));
                    Visit(context.expr(1));
                    m_parents.Pop();
                    break;
            }
            return 0;
        }

        public override int VisitExprAddSub(SimpleCalcParser.ExprAddSubContext context) {

            switch (context.op.Type) {
                case SimpleCalcLexer.PLUS:
                    CAddition newNode = new CAddition();
                    ValueTuple<SimpleCalcASTElement, int> parent = m_parents.Peek();
                    parent.Item1.AddChild(newNode, parent.Item2);

                    m_parents.Push((newNode, CAddition.CT_LEFT));
                    Visit(context.expr(0));
                    m_parents.Pop();

                    m_parents.Push((newNode, CAddition.CT_RIGHT));
                    Visit(context.expr(1));
                    m_parents.Pop();
                    break;
                case SimpleCalcLexer.MINUS:
                    CSubtraction newNode1 = new CSubtraction();
                    ValueTuple<SimpleCalcASTElement, int> parent1 = m_parents.Peek();
                    parent1.Item1.AddChild(newNode1, parent1.Item2);

                    m_parents.Push((newNode1, CSubtraction.CT_LEFT));
                    Visit(context.expr(0));
                    m_parents.Pop();

                    m_parents.Push((newNode1, CSubtraction.CT_RIGHT));
                    Visit(context.expr(1));
                    m_parents.Pop();
                    break;
            }
            return 0;
        }

        public override int VisitCompileUnit(SimpleCalcParser.CompileUnitContext context) {
            CCompileUnit newNode = new CCompileUnit();
            m_root = newNode;

            m_parents.Push((newNode,CCompileUnit.CT_BODY));
            base.VisitCompileUnit(context);
            m_parents.Pop();

            return 0;
        }

        public override int VisitTerminal(ITerminalNode node) {
            switch (node.Symbol.Type) {
                case SimpleCalcLexer.NUMBER:
                    CNUMBER newNumber= new CNUMBER();
                    ValueTuple<SimpleCalcASTElement, int> parent = m_parents.Peek();
                    parent.Item1.AddChild(newNumber, parent.Item2);
                    break;
                case SimpleCalcLexer.VARIABLE:
                    CVARIABLE newVar = new CVARIABLE();
                    ValueTuple<SimpleCalcASTElement, int> parent1 = m_parents.Peek();
                    parent1.Item1.AddChild(newVar, parent1.Item2);
                    break;
            }
            return 0;
        }
    }
}
