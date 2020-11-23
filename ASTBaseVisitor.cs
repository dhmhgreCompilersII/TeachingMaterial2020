using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachingMaterial {
    public abstract class ASTBaseVisitor<T> {
        public virtual T Visit(ASTElement node) {
            return node.Accept(this);
        }
        public virtual T VisitChildren(ASTElement node) {
            T netResult = default(T);
            foreach (ASTElement child in node.GetChildren()) {
                netResult = AggregateResult(netResult, child.Accept(this));
            }

            return netResult;
        }
        public virtual T AggregateResult(T oldResult, T value) {
            return value;
        }
    }

    public abstract class SimpleCalcBaseVisitor<T> : ASTBaseVisitor<T> {

        public virtual T VisitCompileUnit(CCompileUnit node) {
            return VisitChildren(node);
        }

        public virtual T VisitExprNUMBER(CNUMBER node) {
            return default(T);
        }

        public virtual T VisitExprVARIABLE(CVARIABLE node) {
            return default(T);
        }

        public virtual T VisitExprAssignment(CAssignment node) {
            return VisitChildren(node);
        }

        public virtual T VisitExprAddition(CAddition node) {
            return VisitChildren(node);
        }

        public virtual T VisitExprSubtraction(CSubtraction node) {
            return VisitChildren(node);
        }

        public virtual T VisitExprMultiplication(CMultiplication node) {
            return VisitChildren(node);
        }

        public virtual T VisitExprDivision(CDivision node) {
            return VisitChildren(node);
        }
    }
}
