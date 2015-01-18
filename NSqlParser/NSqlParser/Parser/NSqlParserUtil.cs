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
    //    CCNSqlParser parser = new CCNSqlParser(statementReader);
    //    try {
    //        return parser.Statement();
    //    } catch (Exception ex) {
    //        throw new NSqlParserException(ex);
    //    } 
    //}
	
    //public static Statement parse(string sql) throws NSqlParserException {
    //    CCNSqlParser parser = new CCNSqlParser(new stringReader(sql));
    //    try {
    //        return parser.Statement();
    //    } catch (Exception ex) {
    //        throw new NSqlParserException(ex);
    //    } 
    //}
	
    //public static Statement parse(InputStream is) throws NSqlParserException {
    //    CCNSqlParser parser = new CCNSqlParser(is);
    //    try {
    //        return parser.Statement();
    //    } catch (Exception ex) {
    //        throw new NSqlParserException(ex);
    //    } 
    //}
	
    //public static Statement parse(InputStream is, string encoding) throws NSqlParserException {
    //    CCNSqlParser parser = new CCNSqlParser(is,encoding);
    //    try {
    //        return parser.Statement();
    //    } catch (Exception ex) {
    //        throw new NSqlParserException(ex);
    //    } 
    //}
	
    /**
     * Parse an expression.
     * @param expression
     * @return
     * @throws NSqlParserException 
     */
    public static IExpression parseExpression(string expression) {
        //CCNSqlParser parser = new CCNSqlParser(new stringReader(expression));
        //try {
        //    return parser.SimpleExpression();
        //} catch (Exception ex) {
        //    throw new NSqlParserException(ex);
        //} 
        //todo :
        return null;
    }
    
    ///**
    // * Parse an conditional expression. This is the expression after a where clause.
    // * @param condExpr
    // * @return
    // * @throws NSqlParserException 
    // */
    //public static IExpression parseCondExpression(string condExpr) throws NSqlParserException {
    //    CCNSqlParser parser = new CCNSqlParser(new stringReader(condExpr));
    //    try {
    //        return parser.Expression();
    //    } catch (Exception ex) {
    //        throw new NSqlParserException(ex);
    //    } 
    //}
    
    ///**
    // * Parse a statement list.
    // */
    //public static Statements parseStatements(string sqls) throws NSqlParserException {
    //    CCNSqlParser parser = new CCNSqlParser(new stringReader(sqls));
    //    try {
    //        return parser.Statements();
    //    } catch (Exception ex) {
    //        throw new NSqlParserException(ex);
    //    } 
    //}
    }
}
