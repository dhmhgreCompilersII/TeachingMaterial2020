lexer grammar SimpleCalcLexer;

/*
 * Lexer Rules
 */
SEMICOLON : ';';
ASSIGN : '=';
LP : '(';
RP : ')';
PLUS : '+';
MINUS : '-';
MULT : '*';
DIV : '/';

NUMBER : [1-9][0-9]*;
VARIABLE : [a-z][a-zA-Z0-9_]*;
WS
	:	[\r\n\t ] -> skip
	;
