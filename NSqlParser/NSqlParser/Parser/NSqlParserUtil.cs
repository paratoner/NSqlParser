using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSqlParser.Expression;

namespace NSqlParser.Parser
{
    class NSqlParserUtil
    {
    //    public static Statement parse(Reader statementReader) {
    //    CCJSqlParser parser = new CCJSqlParser(statementReader);
    //    try {
    //        return parser.Statement();
    //    } catch (Exception ex) {
    //        throw new JSQLParserException(ex);
    //    } 
    //}
	
    //public static Statement parse(string sql) throws JSQLParserException {
    //    CCJSqlParser parser = new CCJSqlParser(new stringReader(sql));
    //    try {
    //        return parser.Statement();
    //    } catch (Exception ex) {
    //        throw new JSQLParserException(ex);
    //    } 
    //}
	
    //public static Statement parse(InputStream is) throws JSQLParserException {
    //    CCJSqlParser parser = new CCJSqlParser(is);
    //    try {
    //        return parser.Statement();
    //    } catch (Exception ex) {
    //        throw new JSQLParserException(ex);
    //    } 
    //}
	
    //public static Statement parse(InputStream is, string encoding) throws JSQLParserException {
    //    CCJSqlParser parser = new CCJSqlParser(is,encoding);
    //    try {
    //        return parser.Statement();
    //    } catch (Exception ex) {
    //        throw new JSQLParserException(ex);
    //    } 
    //}
	
    /**
     * Parse an expression.
     * @param expression
     * @return
     * @throws JSQLParserException 
     */
    public static IExpression parseExpression(string expression) {
        //CCJSqlParser parser = new CCJSqlParser(new stringReader(expression));
        //try {
        //    return parser.SimpleExpression();
        //} catch (Exception ex) {
        //    throw new JSQLParserException(ex);
        //} 
        //todo :
        return null;
    }
    
    ///**
    // * Parse an conditional expression. This is the expression after a where clause.
    // * @param condExpr
    // * @return
    // * @throws JSQLParserException 
    // */
    //public static IExpression parseCondExpression(string condExpr) throws JSQLParserException {
    //    CCJSqlParser parser = new CCJSqlParser(new stringReader(condExpr));
    //    try {
    //        return parser.Expression();
    //    } catch (Exception ex) {
    //        throw new JSQLParserException(ex);
    //    } 
    //}
    
    ///**
    // * Parse a statement list.
    // */
    //public static Statements parseStatements(string sqls) throws JSQLParserException {
    //    CCJSqlParser parser = new CCJSqlParser(new stringReader(sqls));
    //    try {
    //        return parser.Statements();
    //    } catch (Exception ex) {
    //        throw new JSQLParserException(ex);
    //    } 
    //}
    }
}
