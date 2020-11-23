using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachingMaterial {
    class ASTPrinterVisitor : SimpleCalcBaseVisitor<int> {
        public override int VisitCompileUnit(CCompileUnit node) {
            return base.VisitCompileUnit(node);
        }

        public override int VisitExprNUMBER(CNUMBER node) {
            return base.VisitExprNUMBER(node);
        }

        public override int VisitExprVARIABLE(CVARIABLE node) {
            return base.VisitExprVARIABLE(node);
        }

        public override int VisitExprAssignment(CAssignment node) {
            return base.VisitExprAssignment(node);
        }

        public override int VisitExprAddition(CAddition node) {
            return base.VisitExprAddition(node);
        }

        public override int VisitExprSubtraction(CSubtraction node) {
            return base.VisitExprSubtraction(node);
        }

        public override int VisitExprMultiplication(CMultiplication node) {
            return base.VisitExprMultiplication(node);
        }

        public override int VisitExprDivision(CDivision node) {
            return base.VisitExprDivision(node);
        }
    }
}
