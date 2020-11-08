parser grammar SimpleCalcParser;

options {
    tokenVocab = SimpleCalcLexer;
}

/*
 * Parser Rules
 */

compileUnit
	:	( expr SEMICOLON )+
	;

expr : NUMBER								#ExprNUMBER
	 | VARIABLE								#ExprVARIABLE
	 | VARIABLE ASSIGN<assoc=right> expr	#ExprAssignment
	 | LP expr RP							#ExprParenthesis
	 | expr op=(MULT|DIV) expr				#ExprMulDiv
	 | expr op=(PLUS|MINUS) expr			#ExprAddSub
	;
