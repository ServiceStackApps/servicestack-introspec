﻿namespace ServiceStack.Documentation.AbstractApiSpec
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using Extensions;

    public abstract class ApiDtoSpec<T> : IApiResource
        where T : class, new()
    {
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public string Notes { get; protected set; }

        private readonly Dictionary<MemberInfo, IProperty> parameterLookup;

        protected ApiDtoSpec()
        {
            parameterLookup = new Dictionary<MemberInfo, IProperty>();
        }

        public IProperty GetPropertySpec(PropertyInfo pi) => parameterLookup.SafeGet(pi, (IProperty)null);

        protected IPropertyMetadata For<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            var parameter = PropertyMetadata.Create(expression);
            parameterLookup.Add(parameter.MemberInfo, parameter);
            return parameter;
        }
    }
}