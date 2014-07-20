using System;
using System.Linq;
using System.Linq.Expressions;
using CompanyABC.Domain.Entities;
using CompanyABC.Domain.Repositories;

namespace CompanyABC.Domain.Search
{
    public class ProductSearchService : IProductSearchService
    {
        private readonly IProductRepository _productRepository;

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
            
            //expr1 = exprTree;
            //expr2 = BuildDateStringContainsExpression(productParamExpr, searchQuery, typeof(DateTime), "DateCreated");
            //exprTree = Expression.OrElse(expr1, expr2);

            //expr1 = exprTree;
            //expr2 = BuildDateStringContainsExpression(productParamExpr, searchQuery, typeof(DateTime?), "DateReceived");
            //exprTree = Expression.OrElse(expr1, expr2);

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
            string[] dateFormats = new string[] { "d", "f", "m", "o", "y" };

            Expression productPropExpr = Expression.Property(productParamExpr, typeof(Product).GetProperty(propertyName));

            if (dateType == typeof(DateTime?))
            {
                productPropExpr = Expression.Call(productPropExpr, typeof(DateTime?).GetMethod("GetValueOrDefault", Type.EmptyTypes));
            }

            Expression dateToStringFormatPropExpr = Expression.Call(productPropExpr, typeof(DateTime).GetMethod("ToString", new Type[] { typeof(string) }), Expression.Constant(dateFormats[0]));
            Expression containsProductPropExpr = Expression.Call(dateToStringFormatPropExpr, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), Expression.Constant(searchQuery));
            Expression expr1 = containsProductPropExpr;

            dateToStringFormatPropExpr = Expression.Call(productPropExpr, typeof(DateTime).GetMethod("ToString", new Type[] { typeof(string) }), Expression.Constant(dateFormats[1]));
            containsProductPropExpr = Expression.Call(dateToStringFormatPropExpr, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), Expression.Constant(searchQuery));
            Expression expr2 = containsProductPropExpr;

            Expression exprTree = Expression.OrElse(expr1, expr2);

            foreach (string dateFormat in dateFormats.Skip(2))
            {
                expr1 = exprTree;

                dateToStringFormatPropExpr = Expression.Call(productPropExpr, typeof(DateTime).GetMethod("ToString", new Type[] { typeof(string) }), Expression.Constant(dateFormat));
                expr2 = Expression.Call(dateToStringFormatPropExpr, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), Expression.Constant(searchQuery));

                exprTree = Expression.OrElse(expr1, expr2);
            }

            return exprTree;
        }
    }
}
