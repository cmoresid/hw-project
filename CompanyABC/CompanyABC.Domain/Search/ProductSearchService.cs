using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using CompanyABC.Domain.Entities;
using CompanyABC.Domain.Repositories;

namespace CompanyABC.Domain.Search
{
    public class ProductSearchService : IProductSearchService
    {
        private readonly IProductRepository _productRepository;
        private static readonly IDictionary<string, int> monthNameMappings = new Dictionary<string, int>()
        {
            { "january", 1 },
            { "february", 2},
            { "march", 3},
            { "april", 4},
            { "may", 5},
            { "june", 6},
            { "july", 7},
            { "august", 8},
            { "september", 9},
            { "october", 10},
            { "november", 11},
            { "december", 12}
        };

        public ProductSearchService(IProductRepository productRepo)
        {
            _productRepository = productRepo;
        }

        public IQueryable<Product> Search(string searchQuery)
        {
            searchQuery = searchQuery.ToLower();

            ParameterExpression productParamExpr = Expression.Parameter(typeof(Product), "product");

            Expression expr1 = BuildNonStringContainsExpression(productParamExpr, searchQuery, typeof(Guid), "ABCID");
            Expression expr2 = BuildStringContainsExpression(productParamExpr, searchQuery, "Title");

            Expression exprTree = Expression.OrElse(expr1, expr2);

            expr1 = exprTree;
            expr2 = BuildStringContainsExpression(productParamExpr, searchQuery, "Description");
            exprTree = Expression.OrElse(expr1, expr2);

            expr1 = exprTree;
            expr2 = BuildStringContainsExpression(productParamExpr, searchQuery, "Vendor");
            exprTree = Expression.OrElse(expr1, expr2);

            expr1 = exprTree;
            expr2 = BuildNonStringContainsExpression(productParamExpr, searchQuery, typeof(decimal), "ListPrice");
            exprTree = Expression.OrElse(expr1, expr2);

            expr1 = exprTree;
            expr2 = BuildNonStringContainsExpression(productParamExpr, searchQuery, typeof(decimal), "Cost");
            exprTree = Expression.OrElse(expr1, expr2);

            expr1 = exprTree;
            expr2 = BuildStringContainsExpression(productParamExpr, searchQuery, "Status");
            exprTree = Expression.OrElse(expr1, expr2);

            expr1 = exprTree;
            expr2 = BuildStringContainsExpression(productParamExpr, searchQuery, "Location");
            exprTree = Expression.OrElse(expr1, expr2);

            expr1 = exprTree;
            expr2 = BuildDateStringContainsExpression(productParamExpr, searchQuery, typeof(DateTime), "DateCreated");

            // Only add to the expression tree if valid date search
            if (expr2 != null)
                exprTree = Expression.OrElse(expr1, expr2);

            expr1 = exprTree;
            expr2 = BuildDateStringContainsExpression(productParamExpr, searchQuery, typeof(DateTime?), "DateReceived");

            // Ditto
            if (expr2 != null)
                exprTree = Expression.OrElse(expr1, expr2);

            MethodCallExpression filterCallExpression = Expression.Call(typeof(Queryable),
                "Where",
                new Type[] { _productRepository.Products.ElementType },
                _productRepository.Products.Expression,
                Expression.Lambda<Func<Product, bool>>(exprTree, new ParameterExpression[] { productParamExpr }));

            return _productRepository.Products.Provider.CreateQuery<Product>(filterCallExpression);
        }

        private Expression BuildNonStringContainsExpression(Expression productParamExpr, string searchQuery, Type propType, string propertyName)
        {
            Expression productPropExpr = Expression.Property(productParamExpr, typeof(Product).GetProperty(propertyName));
            Expression stringProductPropExpr = Expression.Call(productPropExpr, propType.GetMethod("ToString", Type.EmptyTypes));
            Expression normalizedProductPropertyExpression = Expression.Call(stringProductPropExpr, typeof(string).GetMethod("ToLower", Type.EmptyTypes));
            Expression expr = Expression.Call(normalizedProductPropertyExpression, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), Expression.Constant(searchQuery));

            return expr;
        }

        private Expression BuildStringContainsExpression(Expression productParamExpr, string searchQuery, string propertyName)
        {
            Expression productPropExpr = Expression.Property(productParamExpr, typeof(Product).GetProperty(propertyName));
            Expression normalizedProductPropertyExpression = Expression.Call(productPropExpr, typeof(string).GetMethod("ToLower", Type.EmptyTypes));
            Expression expr = Expression.Call(normalizedProductPropertyExpression, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), Expression.Constant(searchQuery));

            return expr;
        }

        private Expression BuildDateStringContainsExpression(Expression productParamExpr, string searchQuery, Type dateType, string propertyName)
        {
            Expression productPropExpr = Expression.Property(productParamExpr, typeof(Product).GetProperty(propertyName));

            if (dateType == typeof(DateTime?))
            {
                productPropExpr = Expression.Property(productPropExpr, "Value");
            }

            int number = -1;

            Expression numberConstant;
            if (!int.TryParse(searchQuery, out number))
            {
                number = MatchStringToMonthNumber(searchQuery);

                if (number != -1)
                {
                    numberConstant = Expression.Constant(number);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                numberConstant = Expression.Constant(number);
            }

            Expression yearPropExpr = Expression.Property(productPropExpr, "Year");
            Expression expr1 = Expression.Equal(yearPropExpr, numberConstant);

            Expression dayPropExpr = Expression.Property(productPropExpr, "Day");
            Expression expr2 = Expression.Equal(dayPropExpr, numberConstant);

            Expression exprTree = Expression.OrElse(expr1, expr2);

            expr1 = exprTree;
            Expression monthPropExpr = Expression.Property(productPropExpr, "Month");
            expr2 = Expression.Equal(monthPropExpr, numberConstant);

            exprTree = Expression.OrElse(expr1, expr2);

            return exprTree;
        }

        private int MatchStringToMonthNumber(string input)
        {
            foreach (var monthPair in monthNameMappings)
            {
                if (monthPair.Key.Contains(input.ToLower()))
                    return monthPair.Value;
            }

            return -1;
        }
    }
}
