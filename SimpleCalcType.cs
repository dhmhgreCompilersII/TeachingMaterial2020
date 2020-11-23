using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachingMaterial {
    public class SimpleCalcType : CNodeType<SimpleCalcType.NodeType> {
        public enum NodeType {
            NT_NA,
            NT_COMPILEUNIT,
            NT_ASSIGNMENT,
            NT_ADDITION,
            NT_SUBTRACTION,
            NT_MULTIPLICATION,
            NT_DIVISION,
            NT_NUMBER,
            NT_VARIABLE
        }

        public SimpleCalcType(NodeType nodeType) : base(nodeType) {
        }

        public override NodeType Default() {
            return NodeType.NT_NA;
        }

        public override NodeType NA() {
            return NodeType.NT_NA;
        }

        public override NodeType Map(int type) {
            return (NodeType) type;
        }

        public override int Map(NodeType type) {
            return (int) type;
        }
    }

    public abstract class SimpleCalcASTElement : ASTElement {
        private SimpleCalcType m_nodeType;

        protected SimpleCalcASTElement(int context, SimpleCalcType.NodeType Type) : base(context) {
            m_nodeType = new SimpleCalcType(Type);
        }
    }
    
    public class CCompileUnit : SimpleCalcASTElement {
        public const int CT_BODY = 0;
        public CCompileUnit( ) : base(1, SimpleCalcType.NodeType.NT_COMPILEUNIT) {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor) {
            SimpleCalcBaseVisitor<T> visitor_ = visitor as SimpleCalcBaseVisitor<T>;
            if (visitor_ != null) {
              return visitor_.VisitCompileUnit(this);
            }
            return default(T);
        }
    }

    public class CAssignment : SimpleCalcASTElement {
        public const int CT_LEFT = 0, CT_RIGHT=1;
        public CAssignment() : base(2, SimpleCalcType.NodeType.NT_ASSIGNMENT) {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor) {
            SimpleCalcBaseVisitor<T> visitor_ = visitor as SimpleCalcBaseVisitor<T>;
            if (visitor_ != null) {
                return visitor_.VisitExprAssignment(this);
            }
            return default(T);
        }
    }

    public class CAddition : SimpleCalcASTElement {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CAddition() : base(2, SimpleCalcType.NodeType.NT_ADDITION) {
        }
        public override T Accept<T>(ASTBaseVisitor<T> visitor) {
            SimpleCalcBaseVisitor<T> visitor_ = visitor as SimpleCalcBaseVisitor<T>;
            if (visitor_ != null) {
                return visitor_.VisitExprAddition(this);
            }
            return default(T);
        }
    }

    public class CSubtraction : SimpleCalcASTElement {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CSubtraction() : base(2, SimpleCalcType.NodeType.NT_SUBTRACTION) {
        }
        public override T Accept<T>(ASTBaseVisitor<T> visitor) {
            SimpleCalcBaseVisitor<T> visitor_ = visitor as SimpleCalcBaseVisitor<T>;
            if (visitor_ != null) {
                return visitor_.VisitExprSubtraction(this);
            }
            return default(T);
        }
    }

    public class CMultiplication : SimpleCalcASTElement {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CMultiplication() : base(2, SimpleCalcType.NodeType.NT_MULTIPLICATION) {
        }
        public override T Accept<T>(ASTBaseVisitor<T> visitor) {
            SimpleCalcBaseVisitor<T> visitor_ = visitor as SimpleCalcBaseVisitor<T>;
            if (visitor_ != null) {
                return visitor_.VisitExprMultiplication(this);
            }
            return default(T);
        }
    }

    public class CDivision : SimpleCalcASTElement {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CDivision() : base(2, SimpleCalcType.NodeType.NT_DIVISION) {
        }
        public override T Accept<T>(ASTBaseVisitor<T> visitor) {
            SimpleCalcBaseVisitor<T> visitor_ = visitor as SimpleCalcBaseVisitor<T>;
            if (visitor_ != null) {
                return visitor_.VisitExprDivision(this);
            }
            return default(T);
        }
    }

    public class CNUMBER : SimpleCalcASTElement {
        public CNUMBER() : base(0, SimpleCalcType.NodeType.NT_NUMBER) {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor) {
            SimpleCalcBaseVisitor<T> visitor_ = visitor as SimpleCalcBaseVisitor<T>;
            if (visitor_ != null) {
                return visitor_.VisitExprNUMBER(this);
            }
            return default(T);
        }
    }

    public class CVARIABLE : SimpleCalcASTElement {
        public CVARIABLE() : base(0, SimpleCalcType.NodeType.NT_VARIABLE) {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor) {
            SimpleCalcBaseVisitor<T> visitor_ = visitor as SimpleCalcBaseVisitor<T>;
            if (visitor_ != null) {
                return visitor_.VisitExprVARIABLE(this);
            }
            return default(T);
        }
    }

}
